using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo_Circle : MonoBehaviour {

    public GameObject circle1;
    public GameObject circle2;
    public GameObject circle3;
    public GameObject parent;

    public float raisingTime;
    public float delay;

    Vector3 circlePosition = new Vector3(0, -422f, 0);
    Vector3 circleScale = new Vector3(0.5f, 0.5f, 0.5f);

    public void Success()
    {
        GameObject temp1 = NGUITools.AddChild(parent, circle1);
        GameObject temp2 = NGUITools.AddChild(parent, circle2);
        GameObject temp3 = NGUITools.AddChild(parent, circle3);

        temp1.transform.localPosition = circlePosition;
        temp2.transform.localPosition = circlePosition;
        temp3.transform.localPosition = circlePosition;

        temp1.transform.localScale = circleScale;
        temp2.transform.localScale = circleScale;
        temp3.transform.localScale = circleScale;

        LeanTween.scale(temp1, Vector3.one, raisingTime);
        LeanTween.scale(temp2, Vector3.one, raisingTime).setDelay(delay);
        LeanTween.scale(temp3, Vector3.one, raisingTime).setDelay(delay * 2f);

        LeanTween.Destroy(temp1, (delay * 2f) + raisingTime);
        LeanTween.Destroy(temp2, (delay * 2f) + raisingTime);
        LeanTween.Destroy(temp3, (delay * 2f) + raisingTime);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
