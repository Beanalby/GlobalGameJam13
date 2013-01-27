using UnityEngine;
using System.Collections;

public enum GameDriverState { Start, Run, Success, Fail };
public class GameDriver : MonoBehaviour {

    public GUISkin skin;

    private float gameDuration = 60f;
    private float gameStart;

    private GameObject helpText;
    private GameDriverState state;
    private PulseController pc;
    private TypeMaster tm;

	// Use this for initialization
	void Start () {
        helpText = GameObject.Find("HelpText");
        state = GameDriverState.Start;
        pc = GameObject.Find("PulseController").GetComponent<PulseController>();
        tm = GameObject.Find("TypeMaster").GetComponent<TypeMaster>();
	}
	
	// Update is called once per frame
	void Update () {
        switch(state)
        {
            case GameDriverState.Start:
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    state = GameDriverState.Run;
                    Destroy(helpText);
                    pc.isRunning = true;
                    tm.isRunning = true;
                    gameStart = Time.time;
                }
                break;
            case GameDriverState.Run:
                if (Time.time > gameStart + gameDuration)
                {
                    Debug.Log("No more new beats");
                    pc.makeNewBeats = false;
                    if (!pc.HasBeats())
                    {
                        Debug.Log("It's over!");
                        GameWon();
                    }
                }
                break;
        }
	}
    public void OnGUI()
    {
        if (skin)
            GUI.skin = skin;
        string labelText = null, buttonText = null;
        switch (state)
        {
            case GameDriverState.Fail:
                labelText = "Yooou Looose";
                buttonText = "Try Again";
                break;
            case GameDriverState.Success:
                labelText = "You Live!";
                buttonText = "Play Again";
                break;
        }
        if (labelText != null && buttonText != null)
        {
            GUI.Label(new Rect(Screen.width * (1f / 5f), Screen.height * .75f, Screen.width * (3f / 5f), Screen.height * .1f), labelText);
            if (GUI.Button(new Rect(Screen.width * (1f / 5f), Screen.height * .85f, Screen.width * (3f / 5f), Screen.height * .1f), buttonText))
            {
                Application.LoadLevel("menu");
            }
        }
    }

    public void GameOver()
    {
        state = GameDriverState.Fail;
    }
    public void GameWon()
    {
        state = GameDriverState.Success;
    }
}
