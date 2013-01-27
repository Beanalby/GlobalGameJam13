using UnityEngine;
using System;
using System.Collections;

public enum InputResult { Match, Advance, Reset, None };

[RequireComponent(typeof(TextMesh))]
[RequireComponent(typeof(MeshRenderer))]
public class TypeTarget : MonoBehaviour {

    public string text = "TypeMe";
    public int index;
    public Color matchColor = Color.yellow;

    private string matchHex;
    private int currentPos = 0;
    private TextMesh tm;

    public bool IsFinished
    {
        get { return currentPos == text.Length; }
    }
	// Use this for initialization
	void Start()
    {
        tm = GetComponentInChildren<TextMesh>();
        matchHex = ((int)(255*matchColor.r)).ToString("X2") + ((int)(255*matchColor.g)).ToString("X2") + ((int)(255*matchColor.b)).ToString("X2");
        UpdateTextMesh();
	}
	
    public InputResult HandleInput(char c)
    {
        InputResult ret;
        if (char.ToLower(text[currentPos]) == char.ToLower(c))
        {
            currentPos++;
            if (currentPos == text.Length)
            {
                ret = InputResult.Match;
            }
            else
            {
                ret = InputResult.Advance;
            }
        }
        else /* doesn't match */
        {
            if (currentPos != 0)
            {
                // if we're on the first character and what they typed
                // matches the first character, accept it.
                if (currentPos == 1 && char.ToLower(text[0]) == char.ToLower(c))
                {
                    ret = InputResult.Match;
                }
                else
                {
                    currentPos = 0;
                    ret = InputResult.Reset;
                }
            }
            else
            {
                // nothing on this and it didn't match
                ret = InputResult.None;
            }
        }
        UpdateTextMesh();
        return ret;
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
}
