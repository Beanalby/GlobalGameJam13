using UnityEngine;
using System;
using System.Collections;

public class TypeMe : MonoBehaviour {

    public string target = "";
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
        if (currentPos == target.Length)
        {
            Debug.Log("Already done");
            return;
        }
        if (target[currentPos] == char.ToLower(c))
        {
            Debug.Log("matched, advancing");
            currentPos++;
            if (currentPos == target.Length)
            {
                Debug.Log("MATCHED!");
            }
        }
        else
        {
            Debug.Log("Input " + c + " didn't match " + target[currentPos] + ", resetting");
            currentPos = 0;
        }
        UpdateTextMesh();
    }
    private void UpdateTextMesh()
    {
        string newStr;
        if (currentPos == 0)
        {
            Debug.Log("Updating as blank");
            newStr = target;
        }
        else
        {
            Debug.Log("Creating with color");
            newStr = "<color=\"#" + matchHex + "\">" + target.Substring(0, currentPos) + "</color>" +target.Substring(currentPos);
        }
        Debug.Log("Updating to [" + newStr + "]");
        tm.text = newStr;
    }
}
