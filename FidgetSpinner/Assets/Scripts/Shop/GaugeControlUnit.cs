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
        //const float PADDING = 90f;

        public SpringPanel panel;
        public UILabel upgradeMoney;
        int int_upgradeMoney;

        public UISprite speedGauge;
        public UISprite hasteGauge;
        public UISprite dampingGauge;
        public UISprite coinGauge;

        void Start()
        {
            currentPositionX = transform.localPosition.x;
            currentPositionY = transform.localPosition.y;
            /*
            speedGauge.fillAmount = 0.3f;
            hasteGauge.fillAmount = 0.2f;
            dampingGauge.fillAmount = 0.4f;
            coinGauge.fillAmount = 0.1f;
            */
        }

        void Update()
        {
            currentPositionX = panel.target.x;
            Debug.Log(currentPositionX);

            if (((currentPositionX) / 500) >= -(int)Spinners.spinner0)
            {
                speedGauge.fillAmount = 0.1f;
            }
            else if(((currentPositionX) / 500) == -(int)Spinners.spinner1)
            {
                speedGauge.fillAmount = 0.5f;
            }
        }

        public void UpgradeStats()
        {
            int_upgradeMoney = int.Parse(upgradeMoney.text);

            speedGauge.fillAmount += 0.1f;
            hasteGauge.fillAmount += 0.1f;
            dampingGauge.fillAmount += 0.1f;
            coinGauge.fillAmount += 0.1f;

            int_upgradeMoney += 500;
            upgradeMoney.text = int_upgradeMoney.ToString();
        }
    }
}