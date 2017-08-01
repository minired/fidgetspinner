using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.Shop
{
    public class GaugeControlUnit : MonoBehaviour
    {
        public UISprite fillGauge;
        public UILabel speedText;

        // Use this for initialization
        void Start()
        {
            speedText.text = fillGauge.fillAmount.ToString();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void changeGauge()
        {
            Debug.Log("Click");
            fillGauge.fillAmount += 0.1f;
            
            speedText.text = string.Format("{0:0.##}", fillGauge.fillAmount);
        }
    }
}