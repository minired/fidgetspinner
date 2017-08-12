using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Fidget.GameSpin
{
    public class Timer : MonoBehaviour
    {

        public float deltaAmount;
        public float successAmount;
        public float brokenAmount;
        //public float stoneAmount;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            this.GetComponent<UISprite>().fillAmount -= deltaAmount;
        }

        public void Success()
        {
            this.GetComponent<UISprite>().fillAmount += successAmount;
        }
        
        public void BrokenFail()
        {
            this.GetComponent<UISprite>().fillAmount -= brokenAmount;
        }
        /*
        public void StoneFail()
        {
            this.GetComponent<UISprite>().fillAmount -= stoneAmount;
        }
        */
    }
}