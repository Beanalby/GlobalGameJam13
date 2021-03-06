using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PulseController : MonoBehaviour {

    public GameObject beatTemplate;
    public GameObject feedbackTemplate;

    public float beatDelay;
    private bool _isRunning = false;
    private bool _makeNewBeats = true;
    public Color healthyColor = Color.green;
    public Color badColor = Color.red;
    public float distanceGood = 5;
    public float distanceFair = 10;

    public AudioClip beatSound;
    public AudioClip earlySound;
    public AudioClip missSound;
    public AudioClip flatlineSound;

    private List<GameObject> beats;
    private float lastBeat = -100f;
    private float startOffset = 80f;
    private float missPosition = 6f;
    private float markPosition;

    private GameObject heart;
    private Vector3 heartStartPos=Vector3.zero;
    private GameState gameState;
    private GameDriver gameDriver;
    private Material bgMat;
    private TypeMaster tm;

    private float maxHealth = 100f;
    private float health;
    private float healthModGood = 2.5f;
    private float healthModFair = .5f;
    private float healthModBad = -10f;

    public bool isRunning
    {
        get { return _isRunning; }
        set
        {
            if (!_isRunning && value)
            {
                lastBeat = Time.time;
                AudioSource audio = GetComponent<AudioSource>();
                if (gameState.gameConfig != null)
                {
                    audio.clip = gameState.gameConfig.music;
                    audio.Play();
                }
            }
            _isRunning = value;
        }
    }
    public bool makeNewBeats
    {
        get { return _makeNewBeats; }
        set { _makeNewBeats = value; }
    }
	// Use this for initialization
	void Start () {
        health = maxHealth;
        beats = new List<GameObject>();
        markPosition = transform.Find("PulseMark").transform.localPosition.x;
        bgMat = transform.Find("PulseBackground").GetComponent<LineRenderer>().material;
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        gameDriver = GameObject.Find("GameDriver").GetComponent<GameDriver>();
        tm = GameObject.Find("TypeMaster").GetComponent<TypeMaster>();
        heart = GameObject.Find("Heart");
        if (gameState.gameConfig != null)
            beatDelay = 60f / gameState.gameConfig.rate;
	}
	
	// Update is called once per frame
	void Update () {
        if (_isRunning)
        {
            MakeBeat();
            MoveBeats();
        }
    }

    public void ApplyBeat(GameObject beat, bool didType)
    {
        float healthChange = GetHealthChange(beat);
        ApplyHealthChange(healthChange);
        PlaySound(beat, didType);
        FeedbackController feedback = ((GameObject)Instantiate(feedbackTemplate)).GetComponent<FeedbackController>();
        Vector3 pos = feedback.transform.position;
        feedback.transform.parent = transform;
        feedback.transform.localPosition = pos;
        if (beat != null)
            feedback.beatPos = beat.transform.localPosition.x;
        else
            feedback.beatPos = startOffset;

        RemoveBeat(beat);
    }
    public void ApplyHealthChange(float delta)
    {
        health += delta;
        if (health <= 0)
        {
            Debug.Log("Game Over.");
            GetComponent<AudioSource>().Stop();
            gameDriver.GameOver();
            _isRunning = false;
            tm.isRunning = false;
            AudioSource.PlayClipAtPoint(flatlineSound, Camera.main.transform.position);
        }
        health = Mathf.Min(health, maxHealth);
        Color newColor = GetCurrentColor();
        bgMat.SetColor("_EmisColor", newColor);
        foreach (GameObject beat in beats)
        {
            beat.GetComponent<Renderer>().material.SetColor("_EmisColor", newColor);
        }
    }
    public Color GetCurrentColor()
    {
        Color newColor = Color.Lerp(badColor, healthyColor, health / 100);
        // scale it up to make it brighter
        if (newColor.r > newColor.g)
        {
            newColor.g *= 1 / newColor.r;
            newColor.r = 1;
        }
        else
        {
            newColor.r *= 1 / newColor.g;
            newColor.g = 1;
        }
        return newColor;
    }
    public float GetHealthChange(GameObject beat)
    {
        if (beat == null)
        {
            return healthModBad;
        }
        float distance = GetBeatDistance(beat);
        if (distance < distanceGood)
            return healthModGood;
        if (distance < distanceFair)
            return healthModFair;
        return healthModBad;
    }
    public GameObject GetNearestBeat()
    {
        GameObject nearestBeat = null;
        float nearestDistance = 1000000f;
        foreach (GameObject beat in beats)
        {
            float distance = GetBeatDistance(beat);
            if (distance < nearestDistance)
            {
                nearestBeat = beat;
                nearestDistance = distance;
            }
        }
        return nearestBeat;
    }
    public void DidWord()
    {
        // find the nearest pulse, remove it, and see how good it was
        ApplyBeat(GetNearestBeat(), true);
    }
    public float GetBeatDistance(float posx)
    {
        return Mathf.Abs(posx - markPosition);
    }
    public float GetBeatDistance(GameObject beat)
    {
        if (beat != null)
            return Mathf.Abs(beat.transform.localPosition.x - markPosition);
        else
            return startOffset;
    }
    public bool HasBeats()
    {
        return beats.Count != 0;
    }
    private void MakeBeat()
    {
        if (lastBeat + beatDelay < Time.time)
        {
            if (!_makeNewBeats)
                return;
            lastBeat = Time.time;
            GameObject newBeat = (GameObject)Instantiate(beatTemplate);
            Vector3 pos = newBeat.transform.position;
            pos.x += startOffset;
            newBeat.transform.parent = transform;
            newBeat.transform.localPosition = new Vector3(startOffset, pos.y, pos.z);
            newBeat.GetComponent<Renderer>().material.SetColor("_EmisColor", GetCurrentColor());
            beats.Add(newBeat);
        }
    }
    private void MoveBeats()
    {
        GameObject beatToRemove = null;
        foreach (GameObject beat in beats)
        {
            Vector3 pos = beat.transform.localPosition;
            pos.x -= (20 / beatDelay) * Time.deltaTime;
            beat.transform.localPosition = pos;
            if (pos.x <= missPosition)
            {
                beatToRemove = beat;
            }
        }
        if (beatToRemove != null)
        {
            ApplyBeat(beatToRemove, false);
        }
        if (heart)
        {
            if(heartStartPos == Vector3.zero)
                heartStartPos = heart.transform.position;
            if (!_makeNewBeats)
            {
                heart.transform.position = heartStartPos;
            }
            else
            {
                float offset = (Time.time - lastBeat) / beatDelay;
                Vector3 pos = heartStartPos;
                if (offset > .5f)
                    pos.y = Mathf.Lerp(heartStartPos.y+2f, heartStartPos.y, 2f*(offset-.5f));
                else
                    pos.y = Mathf.Lerp(heartStartPos.y, heartStartPos.y+2f, (2f*offset));
                heart.transform.position = pos;
            }
        }
    }
    private void PlaySound(GameObject beat, bool didType)
    {
        AudioClip clip;
        if (!didType)
            clip = missSound;
        else
            if (GetBeatDistance(beat) > distanceFair)
                clip = earlySound;
            else
                clip = beatSound;

AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
    private void RemoveBeat(GameObject beat)
    {
        if (beat == null)
            return;
        beats.Remove(beat);
        Destroy(beat);
    }
}
