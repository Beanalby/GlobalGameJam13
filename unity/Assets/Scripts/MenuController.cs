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

    public AudioClip musicWhale;
    public AudioClip musicElephant;
    public AudioClip musicHorse;
    public AudioClip musicCow;
    public AudioClip musicHuman;
    public AudioClip musicDog;
    public AudioClip musicCat;
    public AudioClip musicMouse;

    private List<GameConfig> configs;
    private GameState gameState;

	// Use this for initialization
	void Start () {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();

        configs = new List<GameConfig>();
        configs.Add(new GameConfig("Whale", 20, outlineWhaleSmall, outlineWhale, new Vector3(-1.5f, -1.4f, -.5f), .2f, musicWhale));
        configs.Add(new GameConfig("Elephant", 30, outlineElephantSmall, outlineElephant, new Vector3(-.3f, 1.4f, -.5f), .15f, musicElephant));
        configs.Add(new GameConfig("Horse", 45, outlineHorseSmall, outlineHorse, new Vector3(-.6f, 1.1f, -.5f), .15f, musicHorse));
        configs.Add(new GameConfig("Cow", 60, outlineCowSmall, outlineCow, new Vector3(-.3f, .7f, -.5f), .15f, musicCow));
        configs.Add(new GameConfig("Human", 70, outlineHumanSmall, outlineHuman, new Vector3(.2f, 2.3f, -.5f), .1f, musicHuman));
        configs.Add(new GameConfig("Dog", 90, outlineDogSmall, outlineDog, new Vector3(-1.2f, .4f, -.5f), .15f, musicDog));
        configs.Add(new GameConfig("Cat", 110, outlineCatSmall, outlineCat, new Vector3(-.4f, .6f, -.5f), .2f, musicCat));
        configs.Add(new GameConfig("Mouse", 450, outlineMouseSmall, outlineMouse, new Vector3(1.4f, .3f, -.5f), .15f, musicMouse));
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
