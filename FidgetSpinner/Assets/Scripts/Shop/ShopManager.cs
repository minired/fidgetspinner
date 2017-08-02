using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fidget.Shop
{
    public class ShopManager : MonoBehaviour
    {
        public UIAtlas buyAtlas;
        public UILabel buyLabel;
        public UISprite buyButton;
        public UILabel coin;
        int currentCoin;
        public Fidget.Common.BackUI backUI;

        // Use this for initialization
        void Start()
        {
            currentCoin = int.Parse(coin.text);
        }

        // Update is called once per frame
        void Update()
        {

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
            if (buyLabel.text == "BUY" && currentCoin >= 500)
            {
                
                buyLabel.text = "EQUIP";
                currentCoin -= 500;
                coin.text = currentCoin.ToString();
            }
            else if (buyLabel.text == "EQUIP")
            {
                buyButton.GetComponent<UIButton>().normalSprite = "BTN_inactive_bG@sprite";
                buyButton.GetComponent<UIButton>().pressedSprite = null;
            }
        }
    }
}