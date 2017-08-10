using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Fidget.GameSpin
{
    public class Timer : MonoBehaviour
    {

        public float decreaseAmount;
        public float successAmount;
        public float failAmount;

        // Use this for initialization
        void Start()
        {
            decreaseAmount = 0.002f;
            successAmount = 0.1f;
            failAmount = 0.1f;
        }

        // Update is called once per frame
        void Update()
        {
            this.GetComponent<UISprite>().fillAmount -= decreaseAmount;
        }

        public void Success()
        {
            this.GetComponent<UISprite>().fillAmount += successAmount;
        }

        public void Fail()
        {
            this.GetComponent<UISprite>().fillAmount -= failAmount;
        }
    }
}