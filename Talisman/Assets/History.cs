using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class History : MonoBehaviour {
    //something is no yes
    
    string s;
    public TextMeshProUGUI history;
	public void Start ()
    {
        history = gameObject.AddComponent<TextMeshProUGUI>();
	}
	public void addToHistory(string message)
    {
        history = new TextMeshProUGUI();
        s = s + message;
        history.text = s;
        history.ForceMeshUpdate();
    }
    public void Show()
    {
        Debug.Log(history.text);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
