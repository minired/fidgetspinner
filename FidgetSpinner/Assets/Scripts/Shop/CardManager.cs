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

            if (buyState == State.NONE)
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_buy@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = "box_require@sprite";
                buyLabel.text = "[4e2f9f]" + "BUY";

                upgradeButton.GetComponent<UIButton>().normalSprite = "box_require@sprite";
            }
            else if (buyState == State.BUYED)
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_buy@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = "box_require@sprite";
                buyLabel.text = "[4e2f9f]" + "EQUIP";

                upgradeButton.GetComponent<UIButton>().normalSprite = "box_active_equip@sprite";
            }
            else
            {
                buyButton.GetComponent<UIButton>().normalSprite = "box_inactive_equip@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = null;
                buyLabel.text = "[3e3f40]" + "EQUIP";

                upgradeButton.GetComponent<UIButton>().normalSprite = "box_active_equip@sprite";
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
            if (buyState == State.NONE && currentCoin >= FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].upgrade)
            {
                currentCoin -= FidgetSpinnerData.fidgetSpinnerDetails[cardID, spriteLevel].upgrade;
                haveCoin.text = currentCoin.ToString();
                User.Instance.Coin = currentCoin;

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
        }

        public void UpgradeClicked()
        {
            if (upgradeCost <= currentCoin && buyState != State.NONE)
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

                upgradeLabel.text = upgradeCost.ToString("n0");
            }
        }
    }
}