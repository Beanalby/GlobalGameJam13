using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour {

    public GUISkin skin;

    public Texture menuPlaceholder;
    public Texture outlineWhaleSmall;
    public Texture outlineWhale;
    public Texture outlineElephantSmall;
    public Texture outlineElephant;
    public Texture outlineHorseSmall;
    public Texture outlineHorse;
    public Texture outlineCowSmall;
    public Texture outlineCow;
    public Texture outlineHumanSmall;
    public Texture outlineHuman;
    public Texture outlineDogSmall;
    public Texture outlineDog;
    public Texture outlineCatSmall;
    public Texture outlineCat;
    public Texture outlineMouseSmall;
    public Texture outlineMouse;

    private List<GameConfig> configs;
    private GameState gameState;

	// Use this for initialization
	void Start () {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();

        configs = new List<GameConfig>();
        configs.Add(new GameConfig("Whale", 20, outlineWhaleSmall, outlineWhale));
        configs.Add(new GameConfig("Elephant", 30, outlineElephantSmall, outlineElephant));
        configs.Add(new GameConfig("Horse", 45, outlineHorseSmall, outlineHorse));
        configs.Add(new GameConfig("Cow", 60, outlineCowSmall, outlineCow));
        configs.Add(new GameConfig("Human", 70, outlineHumanSmall, outlineHuman));
        configs.Add(new GameConfig("Dog", 90, outlineDogSmall, outlineDog));
        configs.Add(new GameConfig("Cat", 110, outlineCatSmall, outlineCat));
        configs.Add(new GameConfig("Mouse", 450, outlineMouseSmall, outlineMouse));
	}

    void OnGUI()
    {
        int buttonSpace = 55;
        GUI.skin = skin;

        int x = (Screen.height - (configs.Count * buttonSpace))/2;
        foreach (GameConfig config in configs)
        {
            string desc = config.name + " - " + config.rate + " beats per minute";
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
