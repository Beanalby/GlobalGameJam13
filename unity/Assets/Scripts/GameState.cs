using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    private static GameState instance = null;

    public GameConfig gameConfig;

	void Awake ()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance=this;
        Object.DontDestroyOnLoad(gameObject);
	}
}
