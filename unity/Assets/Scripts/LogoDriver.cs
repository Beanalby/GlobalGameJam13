using UnityEngine;
using System.Collections;

public class LogoDriver : MonoBehaviour {

    public GUISkin skin;
    public float beatDelay = 1f;
    public float lastBeat = 0f;
    public AudioClip beat;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel("menu");
        }
        if (lastBeat + beatDelay < Time.time)
        {
            GetComponent<CameraJuice>().BeatGood();
            AudioSource.PlayClipAtPoint(beat, Camera.main.transform.position);
            lastBeat = Time.time;
        }
    }
    void OnGUI()
    {
        if (skin)
            GUI.skin = skin;
        GUI.Label(new Rect(0, Screen.height * (3f/5f), Screen.width, Screen.height * .2f), "Press [Space] to Start");
    }
}
