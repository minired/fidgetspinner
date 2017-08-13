using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.Common
{
    public class ResultPopup : MonoBehaviour
    {
        public delegate void PopupCloseDelegate();
        public event PopupCloseDelegate popupClosed;

        public UILabel scoreLabel;
        public UILabel highscoreLabel;

        public UILabel coinGainLabel;
        public UILabel coinMoreLabel;
        public UILabel coinAdLabel;

        public UISprite bestSprite;

        public GameObject btnBack;
        public GameObject btnRank;
        public GameObject btnShare;


        public GameObject normalCoinObj;
        public GameObject adCoinObj;


        // Use this for initialization
        void Start()
        {
        }

        public void BottomBtnAnimation()
        {
            btnBack.transform.localPosition = new Vector3(btnBack.transform.localPosition.x, -720.0f, btnBack.transform.localPosition.z);
            btnRank.transform.localPosition = new Vector3(btnRank.transform.localPosition.x, -720.0f, btnRank.transform.localPosition.z);
            btnShare.transform.localPosition = new Vector3(btnShare.transform.localPosition.x, -720.0f, btnShare.transform.localPosition.z);
            LeanTween.moveLocalY(btnBack, -463.0f, 0.5f).setEaseOutBounce();
            LeanTween.moveLocalY(btnRank, -463.0f, 0.5f).setEaseOutBounce().setDelay(0.3f);
            LeanTween.moveLocalY(btnShare, -463.0f, 0.5f).setEaseOutBounce().setDelay(0.6f);
        }


        public void CoinBtnAnimation()
        {
            LeanTween.scale(normalCoinObj, new Vector3(1.05f, 1.05f, 1.0f), 0.5f).setLoopPingPong(1);
            LeanTween.scale(adCoinObj, new Vector3(1.05f, 1.05f, 1.0f), 0.5f).setDelay(0.3f).setLoopPingPong(1);
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void BestSpriteOn()
        {
            bestSprite.gameObject.SetActive(true);
        }

        public void BestSpriteOff()
        {
            bestSprite.gameObject.SetActive(false);
        }

        public void OnRestartGame()
        {
            gameObject.SetActive(false);
            if(popupClosed != null)
            {
                popupClosed();
            }
        }
    }
}