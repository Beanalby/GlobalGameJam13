using UnityEngine;
using System.Collections.Generic;

public class TypeMaster : MonoBehaviour {

    public GameObject targetTemplate;
    private bool _isRunning = false;
    public int numTargets;
    public AudioClip keySound;

    private PulseController pc;
    private List<TypeTarget> targets;
    private float targetOffset;
    private string[] startList = { "type", "these", "words" };
    private string[] wordList = { "beat", "blood", "flow", "flutter", "gush", "heart", "live", "pound", "pulse", "spout", "spurt", "surge", "throb", "thump"};

    public bool isRunning
    {
        get { return _isRunning; }
        set
        {
            if (!_isRunning && value)
            {
                InitWords();
            }
            _isRunning = value;
        }
    }
	void Start () {
        pc = GameObject.Find("PulseController").GetComponent<PulseController>();
        targetOffset = targetTemplate.GetComponent<TextMesh>().fontSize / 10;
        targets = new List<TypeTarget>();
        for(int i=0;i<startList.Length;i++)
            targets.Add(CreateTarget(i, startList[i]));
	}

    // Update is called once per frame
    void Update()
    {
        if (_isRunning)
        {
            HandleInput(Input.inputString);
            HandleFinished();
        }
    }

    private void HandleFinished()
    {
        foreach (TypeTarget target in targets)
        {
            if (target.IsFinished)
            {
                TypeTarget oldTarget = target;
                targets[oldTarget.index] = CreateTarget(oldTarget.index);
                Destroy(oldTarget.gameObject);
                pc.DidWord();
            }
        }
    }

    private TypeTarget CreateTarget(int index, string forceWord=null)
    {
        TypeTarget tt = ((GameObject)Instantiate(targetTemplate)).GetComponent<TypeTarget>();
        tt.index = index;
        tt.transform.parent = transform;
        tt.transform.localPosition = new Vector3(0, -(index * targetOffset), 0);
        if (forceWord != null)
            tt.text = forceWord;
        else
            tt.text = GetNewText();
        return tt;
    }
    private void HandleInput(string input)
    {
        InputResult result = InputResult.None;
        foreach (char c in input)
        {
            foreach (TypeTarget target in targets)
            {
                switch (target.HandleInput(c))
                {
                    case InputResult.Match:
                        result = InputResult.Match;
                        break;
                    case InputResult.Advance:
                        if (result != InputResult.Match)
                            result = InputResult.Advance;
                        break;
                    case InputResult.Reset:
                        // reset only overrides none
                        if (result == InputResult.None)
                            result = InputResult.Reset;
                        break;
                }
            }
        }
        if (input != "")
        {
            PlaySound(result);
        }
	}
    private void InitWords()
    {
        if (targets != null)
        {
            foreach(TypeTarget target in targets)
                Destroy(target.gameObject);
            targets.Clear();
        }
        else
            targets = new List<TypeTarget>();

        for (int i = 0; i < numTargets; i++)
            targets.Add(CreateTarget(i));
    }
    public bool IsTextUsed(string text)
    {
        foreach(TypeTarget target in targets)
            if(target.text == text)
                return true;
        return false;
    }
    public string GetNewText()
    {
        string text;
        do
        {
            text = wordList[Random.Range(0, wordList.Length-1)];
        } while(IsTextUsed(text));
        return text;
    }
    public void OnDrawGizmos()
    {
        targetOffset = targetTemplate.GetComponent<TextMesh>().fontSize / 10;
        Gizmos.color = targetTemplate.GetComponent<TypeTarget>().matchColor;
        Vector3 pos = transform.position;
        for (int i = 0; i < numTargets; i++)
        {
            Gizmos.DrawLine(pos,
                new Vector3(pos.x + targetOffset * 2, pos.y - .9f * targetOffset, pos.z));
            Gizmos.DrawLine(new Vector3(pos.x, pos.y - .9f * targetOffset, pos.z),
                new Vector3(pos.x + targetOffset * 2, pos.y, pos.z));
            pos.y -= targetOffset;
        }
    }
    public void PlaySound(InputResult result)
    {
        if(result != InputResult.Match)
            AudioSource.PlayClipAtPoint(keySound, Camera.main.transform.position);
    }
}
