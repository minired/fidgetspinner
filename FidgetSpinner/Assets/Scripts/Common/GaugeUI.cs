using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.Common
{
    public class GaugeUI : MonoBehaviour
    {
        public UISprite gaugeSprite;
        
        
        public void SetGaugeAmount(float amount)
        {
            gaugeSprite.fillAmount = amount;
        }

        public float GetGaugeAmount()
        {
            return gaugeSprite.fillAmount;
        }

        
        // Use this for initialization
        void Start()
        {

        }




        // Update is called once per frame
        void Update()
        {

        }
    }
}