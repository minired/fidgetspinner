using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour {

    int combo;
    public UILabel label;

	// Use this for initialization
	void Start () {
        combo = 0;
        label.text = combo.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Success()
    {
        combo++;
        label.text = combo.ToString();
    }

    public void Fail()
    {
        combo = 0;
        label.text = combo.ToString();
    }
}
