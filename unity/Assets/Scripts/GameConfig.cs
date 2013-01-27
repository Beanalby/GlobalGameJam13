using UnityEngine;
using System.Collections;

public class GameConfig {

    public string name;
    public string description;
    public int rate;
    public Texture menuTexture, bigTexture;

    public GameConfig(string name, int rate, Texture menuTexture, Texture bigTexture)
    {
        this.name = name;
        this.menuTexture = menuTexture;
        this.bigTexture = bigTexture;
        this.rate = rate;
    }
}
