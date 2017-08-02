using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.Shop
{
    public class GaugeControlUnit : MonoBehaviour
    {
        enum Spinners
        {
            spinner0,
            spinner1,
            spinner2,
            spinner3,
            spinner4,
            spinner5,

            total_spinners
        }

        float currentPositionX;
        float currentPositionY;
        const float PADDING = 90f;

        public UILabel upgradeMoney;
        int int_upgradeMoney;
        public UISprite speedGauge;

        void Start()
        {
            currentPositionX = transform.localPosition.x;
            currentPositionY = transform.localPosition.y;

            speedGauge.fillAmount = 0.3f;

            int_upgradeMoney = int.Parse(upgradeMoney.text);
        }

        void Update()
        {
            if (((currentPositionX - PADDING) / 500) == -(int)Spinners.spinner0)
            {
                speedGauge.fillAmount = 0.1f;
            }
            else if(((currentPositionX - PADDING) / 500) == -(int)Spinners.spinner1)
            {
                speedGauge.fillAmount = 0.2f;
            }
        }

        public void changeGauge()
        {
            Debug.Log("Click");

            int_upgradeMoney += 500;
            upgradeMoney.text = int_upgradeMoney.ToString();
            speedGauge.fillAmount += 0.1f;
        }
    }
}