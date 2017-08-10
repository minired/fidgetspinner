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
            spinner6,
            spinner7,

            total_spinners
        }

        public SpringPanel panel;
        public UILabel upgradeLabel;
        int[] upgradeCost = new int[(int)Spinners.total_spinners];

        public UILabel currentMoney;
        int int_currentMoney;

        public UISprite speedGauge;
        public UISprite hasteGauge;
        public UISprite dampingGauge;
        public UISprite coinGauge;
        float[] speedValue = new float[(int)Spinners.total_spinners];
        float[] hasteValue = new float[(int)Spinners.total_spinners];
        float[] dampingValue = new float[(int)Spinners.total_spinners];
        float[] coinValue = new float[(int)Spinners.total_spinners];

        int currentSpinner;

        void Init()
        {
            /*TODO: set stats*/
            for (int i = 0; i < (int)Spinners.total_spinners; ++i)
            {
                speedValue[i] = 0.1f * (i + 1);
                hasteValue[i] = 0.1f * (i + 1);
                dampingValue[i] = 0.1f * (i + 1);
                coinValue[i] = 0.1f * (i + 1);
            }

            for (int i = 0; i < (int)Spinners.total_spinners; ++i)
            {
                upgradeCost[i] = 3000 + (500 * i);
            }
        }

        void Start()
        {
            Init();
        }

        void Update()
        {
            /*TODO: Import current spinner from equiped spinner*/
            currentSpinner = -(int)(Mathf.Round(panel.target.x) / 500);
            //Debug.Log(currentSpinner);

            upgradeLabel.text = upgradeCost[currentSpinner].ToString("n0");

            switch (currentSpinner)
            {
                case (int)Spinners.spinner0:
                    speedGauge.fillAmount = speedValue[(int)Spinners.spinner0];
                    hasteGauge.fillAmount = hasteValue[(int)Spinners.spinner0];
                    dampingGauge.fillAmount = dampingValue[(int)Spinners.spinner0];
                    coinGauge.fillAmount = coinValue[(int)Spinners.spinner0];
                    break;
                case (int)Spinners.spinner1:
                    speedGauge.fillAmount = speedValue[(int)Spinners.spinner1];
                    hasteGauge.fillAmount = hasteValue[(int)Spinners.spinner1];
                    dampingGauge.fillAmount = dampingValue[(int)Spinners.spinner1];
                    coinGauge.fillAmount = coinValue[(int)Spinners.spinner1];
                    break;
                case (int)Spinners.spinner2:
                    speedGauge.fillAmount = speedValue[(int)Spinners.spinner2];
                    hasteGauge.fillAmount = hasteValue[(int)Spinners.spinner2];
                    dampingGauge.fillAmount = dampingValue[(int)Spinners.spinner2];
                    coinGauge.fillAmount = coinValue[(int)Spinners.spinner2];
                    break;
                case (int)Spinners.spinner3:
                    speedGauge.fillAmount = speedValue[(int)Spinners.spinner3];
                    hasteGauge.fillAmount = hasteValue[(int)Spinners.spinner3];
                    dampingGauge.fillAmount = dampingValue[(int)Spinners.spinner3];
                    coinGauge.fillAmount = coinValue[(int)Spinners.spinner3];
                    break;
                case (int)Spinners.spinner4:
                    speedGauge.fillAmount = speedValue[(int)Spinners.spinner4];
                    hasteGauge.fillAmount = hasteValue[(int)Spinners.spinner4];
                    dampingGauge.fillAmount = dampingValue[(int)Spinners.spinner4];
                    coinGauge.fillAmount = coinValue[(int)Spinners.spinner4];
                    break;
                case (int)Spinners.spinner5:
                    speedGauge.fillAmount = speedValue[(int)Spinners.spinner5];
                    hasteGauge.fillAmount = hasteValue[(int)Spinners.spinner5];
                    dampingGauge.fillAmount = dampingValue[(int)Spinners.spinner5];
                    coinGauge.fillAmount = coinValue[(int)Spinners.spinner5];
                    break;
                case (int)Spinners.spinner6:
                    speedGauge.fillAmount = speedValue[(int)Spinners.spinner6];
                    hasteGauge.fillAmount = hasteValue[(int)Spinners.spinner6];
                    dampingGauge.fillAmount = dampingValue[(int)Spinners.spinner6];
                    coinGauge.fillAmount = coinValue[(int)Spinners.spinner6];
                    break;
                case (int)Spinners.spinner7:
                    speedGauge.fillAmount = speedValue[(int)Spinners.spinner7];
                    hasteGauge.fillAmount = hasteValue[(int)Spinners.spinner7];
                    dampingGauge.fillAmount = dampingValue[(int)Spinners.spinner7];
                    coinGauge.fillAmount = coinValue[(int)Spinners.spinner7];
                    break;
                default:
                    break;
            }
        }

        public int GetCurrentSpinner()
        {
            return currentSpinner;
        }

        public void UpgradeStats()
        {
            int_currentMoney = int.Parse(currentMoney.text);

            if (int_currentMoney >= upgradeCost[currentSpinner])
            {
                speedValue[currentSpinner] += 0.1f;
                hasteValue[currentSpinner] += 0.1f;
                dampingValue[currentSpinner] += 0.1f;
                coinValue[currentSpinner] += 0.1f;

                int_currentMoney -= upgradeCost[currentSpinner];
                upgradeCost[currentSpinner] += 500;
                upgradeLabel.text = upgradeCost[currentSpinner].ToString("n0");

                currentMoney.text = int_currentMoney.ToString();
            }
        }
    }
}