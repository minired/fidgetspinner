using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fidget.Shop
{
    public class ShopManager : MonoBehaviour
    {
        //public UISprite buyButton;
        public UIAtlas buyAtlas;
        public UILabel buyLabel;
        public UISprite buyButton;
        public UILabel coin;
        int currentCoin;

        // Use this for initialization
        void Start()
        {
            currentCoin = int.Parse(coin.text);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void movePrevious()
        {
            SceneManager.LoadScene("Main");
        }

        public void buyClicked()
        {
            if (buyLabel.text == "BUY" && currentCoin >= 500)
            {
                buyLabel.text = "EQUIP";
                currentCoin -= 500;
                coin.text = currentCoin.ToString();
            }
            else if (buyLabel.text == "EQUIP")
            {
                buyButton.GetComponent<UIButton>().normalSprite = "BTN_inactive_bG";
            }
        }
    }
}