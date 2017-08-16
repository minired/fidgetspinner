using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo_Circle : MonoBehaviour {

    public GameObject circle1;
    public GameObject circle2;
    public GameObject circle3;
    public GameObject parent;
    
    public float delay;

    Vector3 circlePosition = new Vector3(0, -422f, 0);

    public void Success()
    {
        StartCoroutine(SuccessCoroutine());
    }

    IEnumerator SuccessCoroutine()
    {
        GameObject temp1 = NGUITools.AddChild(parent, circle1);
        temp1.transform.localPosition = circlePosition;
        yield return new WaitForSeconds(delay);

        GameObject temp2 = NGUITools.AddChild(parent, circle2);
        temp2.transform.localPosition = circlePosition;
        yield return new WaitForSeconds(delay);

        GameObject temp3 = NGUITools.AddChild(parent, circle3);
        temp3.transform.localPosition = circlePosition;
        yield return new WaitForSeconds(delay);

        Destroy(temp1);
        yield return new WaitForSeconds(delay);
        Destroy(temp2);
        yield return new WaitForSeconds(delay);
        Destroy(temp3);
        yield return new WaitForSeconds(delay);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
