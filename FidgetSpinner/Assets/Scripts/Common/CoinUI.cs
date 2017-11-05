using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Player;
namespace Fidget.Common
{
    public class CoinUI : MonoBehaviour
    {
        public UILabel coinLabel;

        public GameObject parent;

        Vector3 preloadPos = new Vector3(299f, 490f, 0f);

        public List<GameObject> labelPool;

        DigitChanger digitChanger = new DigitChanger();

        int labelPoolIndex = 0;

        public void SetCoinLabel(ulong coin)
        {
            coinLabel.text = digitChanger.UdigitToString(coin);
        }


        public void AdditionalCoin(ulong coin)
        {
            StartCoroutine(AdditionalCoinProc(coin));
        }

        IEnumerator AdditionalCoinProc(ulong coin)
        {
            if (labelPoolIndex >= labelPool.Count)
            {
                labelPoolIndex = 0;
            }
            GameObject labelObj = labelPool[labelPoolIndex];
            labelPoolIndex++;

            labelObj.SetActive(false);
            labelObj.transform.localPosition = preloadPos;
            labelObj.GetComponent<UILabel>().text = "+" + coin.ToString();
            labelObj.GetComponent<TweenAlpha>().ResetToBeginning();
            labelObj.SetActive(true);
            LeanTween.moveLocalY(labelObj, 576f, 1f);
            yield return new WaitForSeconds(1f);
            labelObj.SetActive(false);
            Debug.Log(labelObj.name);
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