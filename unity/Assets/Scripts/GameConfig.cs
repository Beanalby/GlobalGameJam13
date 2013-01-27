using UnityEngine;
using System.Collections;

public class GameConfig {

    public string name;
    public string description;
    public int rate;
    public Texture menuTexture, bigTexture;
    public Vector3 heartOffset;
    public float heartScale;

    public GameConfig(string name, int rate, Texture menuTexture, Texture bigTexture, Vector3 heartOffset, float heartScale)
    {
        this.name = name;
        this.menuTexture = menuTexture;
        this.bigTexture = bigTexture;
        this.rate = rate;
        this.heartOffset = heartOffset;
        this.heartScale = heartScale;
    }
}
