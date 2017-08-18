using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Player;
namespace Fidget.Common
{
    public class CoinUI : MonoBehaviour
    {
        public UILabel coinLabel;

        DigitChanger digitChanger = new DigitChanger();

        public void SetCoinLabel(ulong coin)
        {
            coinLabel.text = digitChanger.UdigitToString(coin);
        }


        // Use this for initialization
        void Start()
        {
            SetCoinLabel(User.Instance.Coin);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}