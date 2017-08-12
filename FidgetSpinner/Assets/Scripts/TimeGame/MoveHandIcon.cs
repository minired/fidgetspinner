using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.TimeGame
{
    public class MoveHandIcon : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            LeanTween.moveLocalX(gameObject, 70.0f, 1.2f).setEase(LeanTweenType.easeInCirc).setOnComplete(EndAnimation);
        }
        
        void EndAnimation()
        {
            Debug.Log("end");
            LeanTween.moveLocalX(gameObject, 70.0f, 1.2f).setEase(LeanTweenType.easeInCirc).setOnComplete(EndAnimation);
            //gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}