using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fidget.Shop
{
    public class ShopManager : MonoBehaviour
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
            spinner8,
            spinner9,

            total_spinners
        }

        enum State
        {
            NONE,
            BUY,
            EQUIP
        }

        FidgetSpinners[] Fidget = new FidgetSpinners[(int)Spinners.total_spinners];

        // button
        public UILabel buyLabel;
        public UISprite buyButton;
        public UILabel coin;
        private int currentCoin;

        // back_Button
        public Fidget.Common.BackUI backUI;

        // upgrade_Button
        public UILabel upgradeLabel;

        // currentSpinner
        public SpringPanel panel;
        private int currentSpinner;
        private int previousSpinner;

        // Gauge
        public UISprite speedGauge;
        public UISprite hasteGauge;
        public UISprite dampingGauge;
        public UISprite coinGauge;

        void InitSpinners()
        {
            for (int i = 0; i < (int)Spinners.total_spinners; ++i)
            {
                Fidget[i] = new FidgetSpinners()
                {
                    Speed = 0.1f * i,
                    Haste = 0.1f * i,
                    Damping = 0.1f * i,
                    Coin = 0.1f * i,

                    UpgradeCost = 1000 * (i + 1),
                    BuyState = 0
                };
            }
        }

        void UpdateSpinner()
        {
            upgradeLabel.text = Fidget[currentSpinner].UpgradeCost.ToString("n0");

            switch (currentSpinner)
            {
                case (int)Spinners.spinner0:
                    speedGauge.fillAmount = Fidget[(int)Spinners.spinner0].Speed;
                    hasteGauge.fillAmount = Fidget[(int)Spinners.spinner0].Haste;
                    dampingGauge.fillAmount = Fidget[(int)Spinners.spinner0].Damping;
                    coinGauge.fillAmount = Fidget[(int)Spinners.spinner0].Coin;
                    break;
                case (int)Spinners.spinner1:
                    speedGauge.fillAmount = Fidget[(int)Spinners.spinner1].Speed;
                    hasteGauge.fillAmount = Fidget[(int)Spinners.spinner1].Haste;
                    dampingGauge.fillAmount = Fidget[(int)Spinners.spinner1].Damping;
                    coinGauge.fillAmount = Fidget[(int)Spinners.spinner1].Coin;
                    break;
                case (int)Spinners.spinner2:
                    speedGauge.fillAmount = Fidget[(int)Spinners.spinner2].Speed;
                    hasteGauge.fillAmount = Fidget[(int)Spinners.spinner2].Haste;
                    dampingGauge.fillAmount = Fidget[(int)Spinners.spinner2].Damping;
                    coinGauge.fillAmount = Fidget[(int)Spinners.spinner2].Coin;
                    break;
                case (int)Spinners.spinner3:
                    speedGauge.fillAmount = Fidget[(int)Spinners.spinner3].Speed;
                    hasteGauge.fillAmount = Fidget[(int)Spinners.spinner3].Haste;
                    dampingGauge.fillAmount = Fidget[(int)Spinners.spinner3].Damping;
                    coinGauge.fillAmount = Fidget[(int)Spinners.spinner3].Coin;
                    break;
                case (int)Spinners.spinner4:
                    speedGauge.fillAmount = Fidget[(int)Spinners.spinner4].Speed;
                    hasteGauge.fillAmount = Fidget[(int)Spinners.spinner4].Haste;
                    dampingGauge.fillAmount = Fidget[(int)Spinners.spinner4].Damping;
                    coinGauge.fillAmount = Fidget[(int)Spinners.spinner4].Coin;
                    break;
                case (int)Spinners.spinner5:
                    speedGauge.fillAmount = Fidget[(int)Spinners.spinner5].Speed;
                    hasteGauge.fillAmount = Fidget[(int)Spinners.spinner5].Haste;
                    dampingGauge.fillAmount = Fidget[(int)Spinners.spinner5].Damping;
                    coinGauge.fillAmount = Fidget[(int)Spinners.spinner5].Coin;
                    break;
                case (int)Spinners.spinner6:
                    speedGauge.fillAmount = Fidget[(int)Spinners.spinner6].Speed;
                    hasteGauge.fillAmount = Fidget[(int)Spinners.spinner6].Haste;
                    dampingGauge.fillAmount = Fidget[(int)Spinners.spinner6].Damping;
                    coinGauge.fillAmount = Fidget[(int)Spinners.spinner6].Coin;
                    break;
                case (int)Spinners.spinner7:
                    speedGauge.fillAmount = Fidget[(int)Spinners.spinner7].Speed;
                    hasteGauge.fillAmount = Fidget[(int)Spinners.spinner7].Haste;
                    dampingGauge.fillAmount = Fidget[(int)Spinners.spinner7].Damping;
                    coinGauge.fillAmount = Fidget[(int)Spinners.spinner7].Coin;
                    break;
                case (int)Spinners.spinner8:
                    speedGauge.fillAmount = Fidget[(int)Spinners.spinner8].Speed;
                    hasteGauge.fillAmount = Fidget[(int)Spinners.spinner8].Haste;
                    dampingGauge.fillAmount = Fidget[(int)Spinners.spinner8].Damping;
                    coinGauge.fillAmount = Fidget[(int)Spinners.spinner8].Coin;
                    break;
                case (int)Spinners.spinner9:
                    speedGauge.fillAmount = Fidget[(int)Spinners.spinner9].Speed;
                    hasteGauge.fillAmount = Fidget[(int)Spinners.spinner9].Haste;
                    dampingGauge.fillAmount = Fidget[(int)Spinners.spinner9].Damping;
                    coinGauge.fillAmount = Fidget[(int)Spinners.spinner9].Coin;
                    break;
                default:
                    break;
            }

            switch (Fidget[currentSpinner].BuyState)
            {
                case (int)State.NONE:
                    buyLabel.text = "BUY";
                    buyButton.GetComponent<UIButton>().normalSprite = "BTN_BUY_BG@sprite";
                    buyButton.GetComponent<UIButton>().pressedSprite = "box_require@spirte";
                    break;
                case (int)State.BUY:
                    buyLabel.text = "EQUIP";
                    buyButton.GetComponent<UIButton>().normalSprite = "BTN_BUY_BG@sprite";
                    buyButton.GetComponent<UIButton>().pressedSprite = "box_require@spirte";
                    break;
                case (int)State.EQUIP:
                    buyLabel.text = "EQUIP";
                    buyButton.GetComponent<UIButton>().normalSprite = "BTN_inactive_bG@sprite";
                    buyButton.GetComponent<UIButton>().pressedSprite = null;
                    break;
            }
        }

        void Start()
        {
            InitSpinners();
            UpdateSpinner();

            previousSpinner = 0;
        }

        void Update()
        {
            /*TODO: Import current spinner from equiped spinner*/
            currentSpinner = -(int)(Mathf.Round(panel.target.x) / 500);

            if (previousSpinner != currentSpinner)
                UpdateSpinner();

            previousSpinner = currentSpinner;
        }

        private void Awake()
        {
            backUI.backBtn += BackUI_backBtn;
        }

        private void BackUI_backBtn()
        {
            SceneManager.LoadScene("Main");
        }

        public void BuyClicked()
        {
            currentCoin = int.Parse(coin.text);

            if (Fidget[currentSpinner].BuyState == (int)State.NONE && currentCoin >= 500)
            {
                buyLabel.text = "EQUIP";
                Fidget[currentSpinner].BuyState = (int)State.BUY;
                currentCoin -= 500;
                coin.text = currentCoin.ToString();
            }
            else if (Fidget[currentSpinner].BuyState == (int)State.BUY)
            {
                Fidget[currentSpinner].BuyState = (int)State.EQUIP;
                buyButton.GetComponent<UIButton>().normalSprite = "BTN_inactive_bG@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = null;

                // base condition: another spinner equip
                for (int i = 0; i < (int)Spinners.total_spinners; ++i)
                {
                    if (i != currentSpinner && Fidget[i].BuyState == (int)State.EQUIP)
                        Fidget[i].BuyState = (int)State.BUY;
                }
            }
        }

        public void UpgradeClicked()
        {
            int tempCost = Fidget[currentSpinner].UpgradeCost;
            currentCoin = int.Parse(coin.text);

            if (tempCost <= currentCoin && Fidget[currentSpinner].BuyState != (int)State.NONE)
            {
                currentCoin -= tempCost;
                Fidget[currentSpinner].UpgradeCost += 500;
                coin.text = currentCoin.ToString();
                upgradeLabel.text = Fidget[currentSpinner].UpgradeCost.ToString("n0");

                Fidget[currentSpinner].Speed += 0.1f;
                Fidget[currentSpinner].Haste += 0.1f;
                Fidget[currentSpinner].Damping += 0.1f;
                Fidget[currentSpinner].Coin += 0.1f;

                speedGauge.fillAmount = Fidget[currentSpinner].Speed;
                hasteGauge.fillAmount = Fidget[currentSpinner].Haste;
                dampingGauge.fillAmount = Fidget[currentSpinner].Damping;
                coinGauge.fillAmount = Fidget[currentSpinner].Coin;
            }
        }
    }
}