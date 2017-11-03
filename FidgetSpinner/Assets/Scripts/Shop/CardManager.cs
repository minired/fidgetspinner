using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Data;
using Fidget.Player;
using Fidget.Common;

namespace Fidget.Shop
{
    public class CardManager : MonoBehaviour
    {
        enum State
        {
            NONE,
            BUYED,
            EQUIPED
        }

        private const int MAXLEVEL = 20;
        public List<UIAtlas> atlasList;
        private ExpTable expTable = new ExpTable();

        // Coin
        public CoinUI coin;

        // Fidget Base Data
        public int cardID;
        public UISprite fidgetSprite;
        public UILabel nameLabel;
        private int spriteLevel;

        // Button
        private State buyState;
        public UISprite buyButton;
        public UILabel buyLabel;
        public UISprite upgradeButton;
        public UILabel upgradeLabel;
        private ulong upgradeCost;
        public UISprite upgradeIcon;
        public UILabel requireLv;
        public UILabel maxLabel;

        // status
        public UISprite speedGauge;
        public UISprite hasteGauge;
        public UISprite dampingGauge;
        public UISprite coinGauge;

        public UILabel speedLabel;
        public UILabel hasteLabel;
        public UILabel dampingLabel;
        public UILabel coinLabel;

        DigitChanger digitChanger = new DigitChanger();

        void InitCard()
        {
            int atlasCode;
            atlasCode = FidgetSpinnerData.fidgetSpinnerItems[cardID].atlasIndex;
            fidgetSprite.GetComponent<UISprite>().atlas = atlasList[FidgetSpinnerData.fidgetSpinnerItems[cardID].atlasIndex];
            fidgetSprite.GetComponent<UISprite>().spriteName = FidgetSpinnerData.fidgetSpinnerItems[cardID].spriteName;

            spriteLevel = User.Instance.GetFidgetSpinnerLevel(cardID);

            if (cardID == 0 && User.Instance.GetFidgetSpinnerLevel(cardID) == 0)
            {
                User.Instance.SetFidgetSpinnerLevel(cardID, ++spriteLevel);
                buyState = State.EQUIPED;
                User.Instance.EquipIndex = cardID;
            }

            if (spriteLevel == 0)
            {
                nameLabel.text = "Lv.1 " + FidgetSpinnerData.fidgetSpinnerItems[cardID].name;
                upgradeCost = FidgetSpinnerData.fidgetSpinnerItems[cardID].price;
            }
            else
            {
                nameLabel.text = "Lv." + spriteLevel + " " + FidgetSpinnerData.fidgetSpinnerItems[cardID].name;
                upgradeCost = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].upgrade;
            }

            upgradeLabel.text = upgradeCost.ToString("n0");

            if (spriteLevel == 0)
            {
                speedLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].speed.ToString("N0");
                hasteLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].haste.ToString("N0");
                dampingLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].damping.ToString("N0");
                coinLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].coin.ToString("N0");
            }
            else if (spriteLevel < MAXLEVEL)
            {
                speedLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].speed.ToString("N0") + " > " + FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].speed.ToString("N0");
                hasteLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].haste.ToString("N0") + " > " + FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].haste.ToString("N0");
                dampingLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].damping.ToString("N0") + " > " + FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].damping.ToString("N0");
                coinLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].coin.ToString("N0") + " > " + FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].coin.ToString("N0");
            }
            else
            {
                speedLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].speed.ToString("N0");
                hasteLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].haste.ToString("N0");
                dampingLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].damping.ToString("N0");
                coinLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].coin.ToString("N0");
            }

            SetGaugeChanger();
        }

        public void SetGaugeChanger()
        {
            StopCoroutine(GaugeChanger());
            StartCoroutine(GaugeChanger());
        }

        IEnumerator GaugeChanger()
        {
            float targetSpeed;
            float targetHaste;
            float targetDamping;
            float targetCoin;

            if (spriteLevel == 0)
            {
                targetSpeed = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].speed * 0.01f;
                targetHaste = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].haste * 0.01f;
                targetDamping = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].damping * 0.01f;
                targetCoin = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].coin * 0.01f;
            }
            else
            {
                targetSpeed = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].speed * 0.01f;
                targetHaste = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].haste * 0.01f;
                targetDamping = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].damping * 0.01f;
                targetCoin = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].coin * 0.01f;
            }

            speedGauge.fillAmount = 0.0f;
            hasteGauge.fillAmount = 0.0f;
            dampingGauge.fillAmount = 0.0f;
            coinGauge.fillAmount = 0.0f;

            bool proc = true;

            while (proc)
            {
                proc = false;
                if (targetSpeed > speedGauge.fillAmount)
                {
                    speedGauge.fillAmount += 0.01f;
                    proc = true;
                }
                if (targetHaste > hasteGauge.fillAmount)
                {
                    hasteGauge.fillAmount += 0.01f;
                    proc = true;
                }
                if (targetDamping > dampingGauge.fillAmount)
                {
                    dampingGauge.fillAmount += 0.01f;
                    proc = true;
                }
                if (targetCoin > coinGauge.fillAmount)
                {
                    coinGauge.fillAmount += 0.01f;
                    proc = true;
                }

                yield return new WaitForSeconds(0.01f);
            }
        }

        void BuyUpdate()
        {
            if (User.Instance.GetFidgetSpinnerLevel(cardID) == 0)
                buyState = State.NONE;
            else if (User.Instance.GetFidgetSpinnerLevel(cardID) >= 1 && cardID == User.Instance.EquipIndex)
                buyState = State.EQUIPED;
            else
                buyState = State.BUYED;

            if (User.Instance.GetFidgetSpinnerLevel(cardID) >= MAXLEVEL && buyState == State.BUYED)
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_active_equip@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = "box_require@sprite";
                buyLabel.text = "[4e2f9f]" + "EQUIP";

                upgradeButton.GetComponent<UIButton>().normalSprite = "box_maxlevel@sprite";
                upgradeIcon.GetComponent<UISprite>().spriteName = null;

                requireLv.text = null;
                upgradeLabel.text = null;
                maxLabel.text = "MAX LEVEL";
            }
            else if (User.Instance.GetFidgetSpinnerLevel(cardID) >= MAXLEVEL && buyState == State.EQUIPED)
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = null;
                buyLabel.text = "[3e3f40]" + "EQUIP";

                upgradeButton.GetComponent<UIButton>().normalSprite = "box_maxlevel@sprite";
                upgradeIcon.GetComponent<UISprite>().spriteName = null;

                requireLv.text = null;
                upgradeLabel.text = null;
                maxLabel.text = "MAX LEVEL";
            }
            else if (upgradeCost <= User.Instance.Coin && buyState == State.NONE && FidgetSpinnerData.fidgetSpinnerItems[cardID].requireLevel < expTable.GetLevel(User.Instance.Exp))
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_buy@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = "box_require@sprite";
                buyLabel.text = "[4e2f9f]" + "BUY";

                requireLv.text = "[cab7e7]" + "Require Lv." + FidgetSpinnerData.fidgetSpinnerItems[cardID].requireLevel;
                upgradeButton.GetComponent<UIButton>().normalSprite = "box_require@sprite";
                upgradeIcon.GetComponent<UISprite>().spriteName = "ic_coin@sprite";
                
                upgradeLabel.text = "[ffffff]" + digitChanger.UdigitToString(upgradeCost);
                maxLabel.text = null;
            }
            else if (upgradeCost <= User.Instance.Coin && buyState == State.NONE && FidgetSpinnerData.fidgetSpinnerItems[cardID].requireLevel >= expTable.GetLevel(User.Instance.Exp))
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = null;
                buyLabel.text = "[3e3f40]" + "BUY";

                requireLv.text = "[cab7e7]" + "Require Lv." + FidgetSpinnerData.fidgetSpinnerItems[cardID].requireLevel;
                upgradeButton.GetComponent<UIButton>().normalSprite = "box_require@sprite";
                upgradeIcon.GetComponent<UISprite>().spriteName = "ic_coin@sprite";

                upgradeLabel.text = "[ffffff]" + digitChanger.UdigitToString(upgradeCost);
                maxLabel.text = null;
            }
            else if (upgradeCost > User.Instance.Coin && buyState == State.NONE)
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = null;
                buyLabel.text = "[3e3f40]" + "BUY";

                requireLv.text = "[cab7e7]" + "Require Lv." + FidgetSpinnerData.fidgetSpinnerItems[cardID].requireLevel;
                upgradeButton.GetComponent<UIButton>().normalSprite = "box_require@sprite";
                upgradeIcon.GetComponent<UISprite>().spriteName = "ic_coin@sprite";

                upgradeLabel.text = "[ffffff]" + digitChanger.UdigitToString(upgradeCost);
                maxLabel.text = null;
            }
            else if (upgradeCost > User.Instance.Coin && buyState == State.BUYED)
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_buy@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = "box_require@sprite";
                buyLabel.text = "[4e2f9f]" + "EQUIP";
                requireLv.text = "[3e3f40]" + "UPGRADE Lv." + (spriteLevel + 1);

                upgradeButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                upgradeIcon.GetComponent<UISprite>().spriteName = "ic_coin copy 2@sprite";
                upgradeLabel.text = "[ffffff]" + upgradeCost.ToString("n0");
                maxLabel.text = null;
            }
            else if (upgradeCost <= User.Instance.Coin && buyState == State.BUYED)
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_buy@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = "box_require@sprite";
                buyLabel.text = "[4e2f9f]" + "EQUIP";
                requireLv.text = "[fffff6]" + "UPGRADE Lv." + (spriteLevel + 1);

                upgradeButton.GetComponent<UIButton>().normalSprite = "box_active_equip@sprite";
                upgradeIcon.GetComponent<UISprite>().spriteName = "ic_coin copy@sprite";
                upgradeLabel.text = "[512e91]" + upgradeCost.ToString("n0");
                maxLabel.text = null;
            }
            else if (upgradeCost > User.Instance.Coin && buyState == State.EQUIPED)
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = null;
                buyLabel.text = "[3e3f40]" + "EQUIP";
                requireLv.text = "[3e3f40]" + "UPGRADE Lv." + (spriteLevel + 1);

                upgradeButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                upgradeIcon.GetComponent<UISprite>().spriteName = "ic_coin copy 2@sprite";
                upgradeLabel.text = "[ffffff]" + upgradeCost.ToString("n0");
                maxLabel.text = null;
            }
            else
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = null;
                buyLabel.text = "[3e3f40]" + "EQUIP";
                requireLv.text = "[fffff6]" + "UPGRADE Lv." + (spriteLevel + 1);

                upgradeButton.GetComponent<UIButton>().normalSprite = "box_active_equip@sprite";
                upgradeIcon.GetComponent<UISprite>().spriteName = "ic_coin copy@sprite";
                upgradeLabel.text = "[512e91]" + upgradeCost.ToString("n0");
                maxLabel.text = null;
            }
        }

        void Start()
        {
            InitCard();
            
            BuyUpdate();
        }

        void Update()
        {
            BuyUpdate();
        }
        
        public void BuyClicked()
        {
            int playerLevel = expTable.GetLevel(User.Instance.Exp);

            if (buyState == State.NONE && User.Instance.Coin >= FidgetSpinnerData.fidgetSpinnerItems[cardID].price && playerLevel >= FidgetSpinnerData.fidgetSpinnerItems[cardID].requireLevel)
            {
                User.Instance.Coin -= FidgetSpinnerData.fidgetSpinnerItems[cardID].price;
                coin.SetCoinLabel(User.Instance.Coin);

                requireLv.text = "[fffff6]" + "UPGRADE Lv." + (spriteLevel + 1);

                User.Instance.SetFidgetSpinnerLevel(cardID, ++spriteLevel);
                nameLabel.text = "Lv." + spriteLevel + " " + FidgetSpinnerData.fidgetSpinnerItems[cardID].name;

                buyButton.GetComponent<UIButton>().normalSprite = "box_buy@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = "box_require@sprite";
                buyLabel.text = "[4e2f9f]" + "EQUIP";

                upgradeCost = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].upgrade;
                upgradeLabel.text = upgradeCost.ToString("n0");

                speedLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].speed.ToString("N0") + " > " + FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].speed.ToString("N0");
                hasteLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].haste.ToString("N0") + " > " + FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].haste.ToString("N0");
                dampingLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].damping.ToString("N0") + " > " + FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].damping.ToString("N0");
                coinLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].coin.ToString("N0") + " > " + FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].coin.ToString("N0");
            }
            else if(buyState == State.BUYED)
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = null;
                User.Instance.EquipIndex = cardID;
                buyLabel.text = "[3e3f40]" + "EQUIP";
            }

            if (upgradeCost > User.Instance.Coin && buyState != State.NONE)
            {
                upgradeButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                upgradeIcon.GetComponent<UISprite>().spriteName = "ic_coin copy 2@sprite";
                requireLv.text = "[3e3f40]" + "UPGRADE Lv." + (spriteLevel + 1);
                upgradeLabel.text = "[ffffff]" + upgradeCost;
            }
        }

        public void UpgradeClicked()
        {
            if (User.Instance.GetFidgetSpinnerLevel(cardID) >= MAXLEVEL)
            {
                Debug.Log("Max");
            }
            else if (upgradeCost <= User.Instance.Coin && buyState != State.NONE)
            {
                User.Instance.Coin -= upgradeCost;
                coin.SetCoinLabel(User.Instance.Coin);

                User.Instance.SetFidgetSpinnerLevel(cardID, ++spriteLevel);
                nameLabel.text = "Lv." + spriteLevel + " " + FidgetSpinnerData.fidgetSpinnerItems[cardID].name;

                speedGauge.fillAmount = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel-1].speed * 0.01f;
                hasteGauge.fillAmount = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel-1].haste * 0.01f;
                dampingGauge.fillAmount = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel-1].damping * 0.01f;
                coinGauge.fillAmount = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel-1].coin * 0.01f;

                if (spriteLevel < MAXLEVEL)
                {
                    speedLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].speed.ToString("N0") + " > " + FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].speed.ToString("N0");
                    hasteLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].haste.ToString("N0") + " > " + FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].haste.ToString("N0");
                    dampingLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].damping.ToString("N0") + " > " + FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].damping.ToString("N0");
                    coinLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].coin.ToString("N0") + " > " + FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].coin.ToString("N0");
                }
                else
                {
                    speedLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].speed.ToString("N0");
                    hasteLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].haste.ToString("N0");
                    dampingLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].damping.ToString("N0");
                    coinLabel.text = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].coin.ToString("N0");
                }

                upgradeCost = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel - 1].upgrade;

                if(upgradeCost > User.Instance.Coin)
                    upgradeLabel.text = "[ffffff]" + upgradeCost.ToString("n0");
                else
                    upgradeLabel.text = "[512e91]" + upgradeCost.ToString("n0");
            }
        }
    }
}