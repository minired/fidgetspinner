using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Data;
using Fidget.Player;

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
        public int cardID;
        public List<UIAtlas> atlasList;

        // Coin
        public UILabel haveCoin;
        private ulong currentCoin;

        // Fidget Base Data
        public UISprite fidgetSprite;
        public UILabel nameLabel;
        public UILabel levelLabel;
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

        // status
        public UISprite speedGauge;
        public UISprite hasteGauge;
        public UISprite dampingGauge;
        public UISprite coinGauge;

        void InitCard()
        {
            currentCoin = User.Instance.Coin;
            haveCoin.text = currentCoin.ToString();

            int atlasCode;
            atlasCode = FidgetSpinnerData.fidgetSpinnerItems[cardID].atlasIndex;
            fidgetSprite.GetComponent<UISprite>().atlas = atlasList[FidgetSpinnerData.fidgetSpinnerItems[cardID].atlasIndex];
            fidgetSprite.GetComponent<UISprite>().spriteName = FidgetSpinnerData.fidgetSpinnerItems[cardID].spriteName;

            nameLabel.text = FidgetSpinnerData.fidgetSpinnerItems[cardID].name;
            spriteLevel = User.Instance.GetFidgetSpinnerLevel(cardID);
            levelLabel.text = "Lv." + spriteLevel;
            upgradeCost = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].upgrade;
            upgradeLabel.text = upgradeCost.ToString("n0");

            speedGauge.fillAmount = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].speed * 0.01f;
            hasteGauge.fillAmount = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].haste * 0.01f;
            dampingGauge.fillAmount = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].damping * 0.01f;
            coinGauge.fillAmount = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].coin * 0.01f;
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

                upgradeButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                upgradeIcon.GetComponent<UISprite>().spriteName = "ic_coin copy 2@sprite";

                requireLv.text = "[3e3f40]" + "MAX";
                upgradeLabel.text = null;
            }
            else if (User.Instance.GetFidgetSpinnerLevel(cardID) >= MAXLEVEL && buyState == State.EQUIPED)
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = null;
                buyLabel.text = "[3e3f40]" + "EQUIP";

                upgradeButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                upgradeIcon.GetComponent<UISprite>().spriteName = "ic_coin copy 2@sprite";

                requireLv.text = "[3e3f40]" + "MAX";
                upgradeLabel.text = null;
            }
            else if (buyState == State.NONE)
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_buy@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = "box_require@sprite";
                buyLabel.text = "[4e2f9f]" + "BUY";
                requireLv.text = "[cab7e7]" + "Require Lv.";

                upgradeButton.GetComponent<UIButton>().normalSprite = "box_require@sprite";
                upgradeIcon.GetComponent<UISprite>().spriteName = "ic_coin@sprite";
                upgradeLabel.text = "[ffffff]" + upgradeCost;
            }
            else if (upgradeCost > currentCoin && buyState == State.BUYED)
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_buy@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = "box_require@sprite";
                buyLabel.text = "[4e2f9f]" + "EQUIP";
                requireLv.text = "[3e3f40]" + "UPGRADE Lv." + (spriteLevel + 1);

                upgradeButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                upgradeIcon.GetComponent<UISprite>().spriteName = "ic_coin copy 2@sprite";
                upgradeLabel.text = "[ffffff]" + upgradeCost;
            }
            else if (upgradeCost <= currentCoin && buyState == State.BUYED)
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_buy@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = "box_require@sprite";
                buyLabel.text = "[4e2f9f]" + "EQUIP";
                requireLv.text = "[fffff6]" + "UPGRADE Lv." + (spriteLevel + 1);

                upgradeButton.GetComponent<UIButton>().normalSprite = "box_active_equip@sprite";
                upgradeIcon.GetComponent<UISprite>().spriteName = "ic_coin copy@sprite";
                upgradeLabel.text = "[512e91]" + upgradeCost;
            }
            else if (upgradeCost > currentCoin && buyState == State.EQUIPED)
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = "box_require@sprite";
                buyLabel.text = "[3e3f40]" + "EQUIP";
                requireLv.text = "[3e3f40]" + "UPGRADE Lv." + (spriteLevel + 1);

                upgradeButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                upgradeIcon.GetComponent<UISprite>().spriteName = "ic_coin copy 2@sprite";
                upgradeLabel.text = "[ffffff]" + upgradeCost;
            }
            else
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = null;
                buyLabel.text = "[3e3f40]" + "EQUIP";
                requireLv.text = "[fffff6]" + "UPGRADE Lv." + (spriteLevel + 1);

                upgradeButton.GetComponent<UIButton>().normalSprite = "box_active_equip@sprite";
                upgradeIcon.GetComponent<UISprite>().spriteName = "ic_coin copy@sprite";
                upgradeLabel.text = "[512e91]" + upgradeCost;
            }
        }

        void Start()
        {
            InitCard();

            BuyUpdate();
        }

        void Update()
        {
            /*TODO: Do optimization*/
            BuyUpdate();
        }

        public void BuyClicked()
        {
            /*TODO: upgradeCost -> buyCost*/
            if (buyState == State.NONE && currentCoin >= FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].upgrade)
            {
                currentCoin -= FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].upgrade;
                haveCoin.text = currentCoin.ToString();
                User.Instance.Coin = currentCoin;

                requireLv.text = "[fffff6]" + "UPGRADE Lv." + (spriteLevel + 1);

                User.Instance.SetFidgetSpinnerLevel(cardID, ++spriteLevel);
                levelLabel.text = "Lv." + spriteLevel.ToString();

                buyButton.GetComponent<UIButton>().normalSprite = "box_buy@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = "box_require@sprite";
                buyLabel.text = "[4e2f9f]" + "EQUIP";
            }
            else if(buyState == State.BUYED)
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = null;
                User.Instance.EquipIndex = cardID;
                buyLabel.text = "[3e3f40]" + "EQUIP";
            }

            if (upgradeCost > currentCoin && buyState != State.NONE)
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
            else if (upgradeCost <= currentCoin && buyState != State.NONE)
            {
                currentCoin -= upgradeCost;
                haveCoin.text = currentCoin.ToString();
                User.Instance.Coin = currentCoin;

                User.Instance.SetFidgetSpinnerLevel(cardID, ++spriteLevel);
                levelLabel.text = "Lv." + spriteLevel.ToString();

                speedGauge.fillAmount = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].speed * 0.01f;
                hasteGauge.fillAmount = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].haste * 0.01f;
                dampingGauge.fillAmount = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].damping * 0.01f;
                coinGauge.fillAmount = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].coin * 0.01f;

                upgradeCost = FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].upgrade;

                if(upgradeCost > currentCoin)
                    upgradeLabel.text = "[ffffff]" + upgradeCost.ToString("n0");
                else
                    upgradeLabel.text = "[512e91]" + upgradeCost.ToString("n0");
            }
        }
    }
}