using UnityEngine;
using System;
using System.Collections;

public enum InputResult { None, Advance, Reset };

[RequireComponent(typeof(TextMesh))]
[RequireComponent(typeof(MeshRenderer))]
public class TypeTarget : MonoBehaviour {

    public string text = "TypeMe";
    public Color matchColor = Color.yellow;

    private string matchHex;
    private int currentPos = 0;
    private TextMesh tm;

	// Use this for initialization
	void Start()
    {
        tm = GetComponentInChildren<TextMesh>();
        matchHex = ((int)(255*matchColor.r)).ToString("X2") + ((int)(255*matchColor.g)).ToString("X2") + ((int)(255*matchColor.b)).ToString("X2");
        UpdateTextMesh();
	}
	
	// Update is called once per frame
	void Update()
    {
        foreach (char c in Input.inputString)
        {
            HandleInput(c);
        }
	}

    private void HandleInput(char c)
    {
        if (currentPos == text.Length)
        {
            return;
        }
        if (char.ToLower(text[currentPos]) == char.ToLower(c))
        {
            currentPos++;
            if (currentPos == text.Length)
            {
                Debug.Log("MATCHED!");
            }
        }
        else
        {
            currentPos = 0;
        }
        UpdateTextMesh();
    }
    private void UpdateTextMesh()
    {
        string newStr;
        if (currentPos == 0)
        {
            newStr = text;
        }
        else
        {
            newStr = "<color=\"#" + matchHex + "\">" + text.Substring(0, currentPos) + "</color>" +text.Substring(currentPos);
        }
        tm.text = newStr;
    }
    void OnGUI()
    {
    }
}
