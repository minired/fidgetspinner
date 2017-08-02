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

        int currentSpinner;
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
            /*
            speedGauge.fillAmount = 0.3f;
            hasteGauge.fillAmount = 0.2f;
            dampingGauge.fillAmount = 0.4f;
            coinGauge.fillAmount = 0.1f;
            */
        }

        void Update()
        {
            currentSpinner = -(int)(Mathf.Round(panel.target.x) / 500);
            Debug.Log(currentSpinner);

            switch(currentSpinner)
            {
                case (int)Spinners.spinner0:
                    break;
                case (int)Spinners.spinner1:
                    break;
                case (int)Spinners.spinner2:
                    break;
                case (int)Spinners.spinner3:
                    break;
                case (int)Spinners.spinner4:
                    break;
                case (int)Spinners.spinner5:
                    break;
                default:
                    break;
            }

            //if (((currentPositionX) / 500) == -(int)Spinners.spinner0)
            //{
            //    speedGauge.fillAmount = 0.1f;
            //}
            //else if(((currentPositionX) / 500) == -(int)Spinners.spinner1)
            //{
            //    speedGauge.fillAmount = 0.5f;
            //}
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