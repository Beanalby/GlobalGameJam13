using UnityEngine;
using System.Collections;

public enum GameDriverState { Start, Run, Success, Fail };
public class GameDriver : MonoBehaviour {

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
                }
                break;
        }
	}
}
