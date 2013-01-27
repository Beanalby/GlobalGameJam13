using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour {

    public GUISkin skin;

    public Texture menuPlaceholder;
    private List<GameConfig> configs;
    private GameState gameState;

	// Use this for initialization
	void Start () {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();

        configs = new List<GameConfig>();
        configs.Add(new GameConfig("Whale", 20, menuPlaceholder, menuPlaceholder));
        configs.Add(new GameConfig("Elephant", 30, menuPlaceholder, menuPlaceholder));
        configs.Add(new GameConfig("Horse", 45, menuPlaceholder, menuPlaceholder));
        configs.Add(new GameConfig("Cow", 60, menuPlaceholder, menuPlaceholder));
        configs.Add(new GameConfig("Human", 70, menuPlaceholder, menuPlaceholder));
        configs.Add(new GameConfig("Dog", 90, menuPlaceholder, menuPlaceholder));
        configs.Add(new GameConfig("Cat", 110, menuPlaceholder, menuPlaceholder));
        configs.Add(new GameConfig("Mouse", 450, menuPlaceholder, menuPlaceholder));
	}

    void OnGUI()
    {
        int buttonSpace = 50;
        GUI.skin = skin;

        int x = (Screen.height - (configs.Count * buttonSpace))/2;
        foreach (GameConfig config in configs)
        {
            string desc = config.name + " - " + config.rate + " beats (words) per minute";
            if(GUI.Button(new Rect(Screen.width/4, x, Screen.width / 2, buttonSpace-5), new GUIContent(desc, config.menuTexture)))
                LaunchConfig(config);
            x += buttonSpace;
        }
    }

    void LaunchConfig(GameConfig config)
    {
        gameState.gameConfig = config;
        Application.LoadLevel("game");
    }
}
