using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.Common
{
    public class CoinUI : MonoBehaviour
    {
        public UILabel coinLabel;

        public void SetCoinLabel(ulong coin)
        {
            coinLabel.text = coin.ToString();
        }


        // Use this for initialization
        void Start()
        {
            SetCoinLabel(500);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}