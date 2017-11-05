using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Player;
namespace Fidget.Common
{
    public class CoinUI : MonoBehaviour
    {
        public UILabel coinLabel;

        public GameObject additionalCoin;

        public GameObject parent;

        Vector3 preloadPos = new Vector3(299f, 490f, 0f);

        Queue<GameObject> labelPool = new Queue<GameObject>(20);

        DigitChanger digitChanger = new DigitChanger();

        public void SetCoinLabel(ulong coin)
        {
            coinLabel.text = digitChanger.UdigitToString(coin);
        }

        void PreloadLabels()
        {
            for (int i = 0; i < 20; ++i)
            {
                GameObject label = NGUITools.AddChild(parent, additionalCoin);
                label.transform.parent = this.transform;
                label.transform.localScale = new Vector3(1.0f, 1.0f);
                label.transform.localPosition = preloadPos;
                label.SetActive(false);
                labelPool.Enqueue(label);
            }
        }

        public void AdditionalCoin(ulong coin)
        {
            StartCoroutine(AdditionalCoin_Co(coin));
            //User.Instance.Coin += coin;
            //SetCoinLabel(User.Instance.Coin);
        }

        IEnumerator AdditionalCoin_Co(ulong coin)
        {
            GameObject label = labelPool.Dequeue();
            label.SetActive(true);
            label.GetComponent<UILabel>().text = "+" + coin.ToString();
            label.GetComponent<TweenAlpha>().ResetToBeginning();
            LeanTween.moveLocalY(label, 576f, 1f);

            yield return new WaitForSeconds(1f);

            label.SetActive(false);
            label.transform.localPosition = preloadPos;
            labelPool.Enqueue(label);
        }


        // Use this for initialization
        void Start()
        {
            SetCoinLabel(User.Instance.Coin);
            PreloadLabels();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}