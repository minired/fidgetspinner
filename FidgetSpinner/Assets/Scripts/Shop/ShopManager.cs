using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fidget.Common;
using Fidget.Player;
using Fidget.Data;

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
        private ulong currentCoin;

        // back_Button
        public Fidget.Common.BackUI backUI;

        // upgrade_Button
        public UILabel upgradeLabel;

        // currentSpinner
        public SpringPanel panel;
        public UIPanel uiPanel;
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
            nameMatch.Add(Spinners.spinner4, "Flower1");
            nameMatch.Add(Spinners.spinner5, "Flower2");
            nameMatch.Add(Spinners.spinner6, "Flower3");
            nameMatch.Add(Spinners.spinner7, "Flower4");
            nameMatch.Add(Spinners.spinner8, "Flower5");
            nameMatch.Add(Spinners.spinner9, "Flower6");
        }

        void InitSpinners()
        {
            Spinners imgCode;
            State buyCode;

            for (int i = 0; i < (int)Spinners.total_spinners; ++i)
            {
                imgCode = (Spinners)i;

                if (User.Instance.GetFidgetSpinnerLevel(i) == 0)
                    buyCode = State.NONE;
                else if (User.Instance.GetFidgetSpinnerLevel(i) >= 1 && currentSpinner == i)
                    buyCode = State.EQUIP;
                else
                    buyCode = State.BUY;

                Fidget[i] = new FidgetSpinners()
                {
                    FidgetName = nameMatch[imgCode],
                    Level = User.Instance.GetFidgetSpinnerLevel(i),
                    Speed = FidgetSpinnerData.fidgetSpinnerDetails[i, User.Instance.GetFidgetSpinnerLevel(i)].speed * 0.01f,
                    Haste = FidgetSpinnerData.fidgetSpinnerDetails[i, User.Instance.GetFidgetSpinnerLevel(i)].haste * 0.01f,
                    Damping = FidgetSpinnerData.fidgetSpinnerDetails[i, User.Instance.GetFidgetSpinnerLevel(i)].damping * 0.01f,
                    Coin = FidgetSpinnerData.fidgetSpinnerDetails[i, User.Instance.GetFidgetSpinnerLevel(i)].coin * 0.01f,

                    BuyCost = 100 * (i + 1),
                    UpgradeCost = FidgetSpinnerData.fidgetSpinnerDetails[i, User.Instance.GetFidgetSpinnerLevel(i)].upgrade,
                    BuyState = (int)buyCode
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
            currentSpinner = User.Instance.EquipIndex;
            coin.text = User.Instance.Coin.ToString();

            /*TODO: y*/
            panel.transform.localPosition = new Vector3(-630 * currentSpinner, 0, 0);
            uiPanel.clipOffset = new Vector2(630 * currentSpinner, 0);

            SetDictionary();
            InitSpinners();
            UpdateSpinner();

            previousSpinner = 0;
        }

        void Update()
        {
            currentSpinner = -(int)(Mathf.Round(panel.target.x) / 630);
            
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
            User.Instance.Coin = currentCoin;
            SceneManager.LoadScene("Main");
        }

        public void BuyClicked()
        {
            currentCoin = ulong.Parse(coin.text);

            if (Fidget[currentSpinner].BuyState == (int)State.NONE && currentCoin >= 500)
            {
                buyLabel.text = "EQUIP";
                Fidget[currentSpinner].BuyState = (int)State.BUY;
                currentCoin -= (ulong)Fidget[currentSpinner].BuyCost;
                coin.text = currentCoin.ToString();

                User.Instance.SetFidgetSpinnerLevel(currentSpinner, 1);
            }
            else if (Fidget[currentSpinner].BuyState == (int)State.BUY)
            {
                Fidget[currentSpinner].BuyState = (int)State.EQUIP;
                User.Instance.EquipIndex = currentSpinner;
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
            ulong tempCost = Fidget[currentSpinner].UpgradeCost;
            currentCoin = ulong.Parse(coin.text);

            if (tempCost <= currentCoin && Fidget[currentSpinner].BuyState != (int)State.NONE)
            {
                User.Instance.SetFidgetSpinnerLevel(currentSpinner, User.Instance.GetFidgetSpinnerLevel(currentSpinner) + 1);
                Fidget[currentSpinner].Level = User.Instance.GetFidgetSpinnerLevel(currentSpinner);

                currentCoin -= tempCost;
                coin.text = currentCoin.ToString();

                Fidget[currentSpinner].Speed = FidgetSpinnerData.fidgetSpinnerDetails[currentSpinner, Fidget[currentSpinner].Level].speed * 0.01f;
                Fidget[currentSpinner].Haste = FidgetSpinnerData.fidgetSpinnerDetails[currentSpinner, Fidget[currentSpinner].Level].haste * 0.01f;
                Fidget[currentSpinner].Damping = FidgetSpinnerData.fidgetSpinnerDetails[currentSpinner, Fidget[currentSpinner].Level].damping * 0.01f;
                Fidget[currentSpinner].Coin = FidgetSpinnerData.fidgetSpinnerDetails[currentSpinner, Fidget[currentSpinner].Level].coin * 0.01f;
                Fidget[currentSpinner].UpgradeCost = FidgetSpinnerData.fidgetSpinnerDetails[currentSpinner, Fidget[currentSpinner].Level].upgrade;

                speedGauge.fillAmount = Fidget[currentSpinner].Speed;
                hasteGauge.fillAmount = Fidget[currentSpinner].Haste;
                dampingGauge.fillAmount = Fidget[currentSpinner].Damping;
                coinGauge.fillAmount = Fidget[currentSpinner].Coin;

                upgradeLabel.text = Fidget[currentSpinner].UpgradeCost.ToString("n0");
            }
        }
    }
}