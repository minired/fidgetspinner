using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fidget.Common;

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

        public BackgroundChanger bgChanger;

        FidgetSpinners[] Fidget = new FidgetSpinners[(int)Spinners.total_spinners];
        Dictionary<Spinners, string> nameMatch = new Dictionary<Spinners, string>();

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
        public UILabel fidgetName;

        // Gauge
        public UISprite speedGauge;
        public UISprite hasteGauge;
        public UISprite dampingGauge;
        public UISprite coinGauge;

        void SetDictionary()
        {
            nameMatch.Add(Spinners.spinner0, "Galaxy");
            nameMatch.Add(Spinners.spinner1, "Batman");
            nameMatch.Add(Spinners.spinner2, "Triangle");
            nameMatch.Add(Spinners.spinner3, "Flower");
            nameMatch.Add(Spinners.spinner4, "Flower");
            nameMatch.Add(Spinners.spinner5, "Flower");
            nameMatch.Add(Spinners.spinner6, "Flower");
            nameMatch.Add(Spinners.spinner7, "Flower");
            nameMatch.Add(Spinners.spinner8, "Flower");
            nameMatch.Add(Spinners.spinner9, "Flower");
        }

        void InitSpinners()
        {
            for (int i = 0; i < (int)Spinners.total_spinners; ++i)
            {
                Fidget[i] = new FidgetSpinners()
                {
                    //FidgetName = nameMatch[],
                    Speed = 0.1f * i,
                    Haste = 0.1f * i,
                    Damping = 0.1f * i,
                    Coin = 0.1f * i,

                    BuyCost = 100 * (i + 1),
                    UpgradeCost = 1000 * (i + 1),
                    BuyState = 0
                };
            }
        }

        void UpdateSpinner()
        {
            upgradeLabel.text = Fidget[currentSpinner].UpgradeCost.ToString("n0");

            fidgetName.text = Fidget[currentSpinner].FidgetName;
            speedGauge.fillAmount = Fidget[currentSpinner].Speed;
            hasteGauge.fillAmount = Fidget[currentSpinner].Haste;
            dampingGauge.fillAmount = Fidget[currentSpinner].Damping;
            coinGauge.fillAmount = Fidget[currentSpinner].Coin;
            bgChanger.ChangeBackground(currentSpinner);

            switch (Fidget[currentSpinner].BuyState)
            {
                case (int)State.NONE:
                    buyLabel.text = "BUY";
                    buyButton.GetComponent<UIButton>().normalSprite = "BTN_BUY_BG@sprite";
                    buyButton.GetComponent<UIButton>().pressedSprite = "BTN_active_BG@sprite";
                    break;
                case (int)State.BUY:
                    buyLabel.text = "EQUIP";
                    buyButton.GetComponent<UIButton>().normalSprite = "BTN_BUY_BG@sprite";
                    buyButton.GetComponent<UIButton>().pressedSprite = "BTN_active_BG@sprite";
                    break;
                case (int)State.EQUIP:
                    buyLabel.text = "EQUIP";
                    buyButton.GetComponent<UIButton>().normalSprite = "BTN_inactive_bG@sprite";
                    buyButton.GetComponent<UIButton>().pressedSprite = null;
                    break;
                default:
                    buyButton.GetComponent<UIButton>().pressedSprite = "box_require@spirte";
                    break;
            }
        }

        void Start()
        {
            SetDictionary();
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
                currentCoin -= Fidget[currentSpinner].BuyCost;
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