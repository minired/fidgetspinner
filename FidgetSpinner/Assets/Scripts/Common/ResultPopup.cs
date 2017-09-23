using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;
#if (UNITY_IPHONE || UNITY_IOS )
using UnityEngine.SocialPlatforms.GameCenter;
#endif

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
        public UILabel coinAdTitleLabel;
        public UISprite coinAdBGSprite;

        public UISprite coinAdIcon;

        public UISprite bestSprite;

        public GameObject btnBack;
        public GameObject btnRank;
        public GameObject btnShare;


        public GameObject normalCoinObj;
        public GameObject adCoinObj;


        public GooglePlay goolgePlay;

        public GameObject adButtonGroup;

        public GameAudio gameaudio;

        // Use this for initialization
        void Start()
        {
            if (adButtonGroup != null)
            {
                adButtonGroup.SetActive(false);
            }
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


        // Update is called once per frame
        void Update()
        {

        }



        public void ShowScore(int score)
        {
            StartCoroutine(ShowScoreCoroutine(score));
        }

        IEnumerator ShowScoreCoroutine(int score)
        {
            int temp = 0;
            int gap = score / 50;
            while(true)
            {
                if (temp >= score)
                {
                    scoreLabel.text = score.ToString();
                    break;
                }
                scoreLabel.text = temp.ToString();
                temp += gap;
                yield return new WaitForFixedUpdate();
            }
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
            if (gameaudio != null)
            {
                gameaudio.ButtonBeepPop();
            }
        }

        public void SetEnableAdButton()
        {
            coinAdBGSprite.spriteName = "coin2_bg@sprite";
            coinAdBGSprite.gameObject.GetComponent<UIButton>().normalSprite = "coin2_bg@sprite";
            coinAdLabel.color = Color.white;
            coinAdTitleLabel.color = Color.white;
            coinAdIcon.spriteName = "ic_ad@sprite";
        }
        public void SetDisableAdButton()
        {
            coinAdBGSprite.spriteName = "coin2_bg_gray@sprite";
            coinAdBGSprite.gameObject.GetComponent<UIButton>().normalSprite = "coin2_bg_gray@sprite";

            coinAdLabel.color = new Color(0.79f,0.8f,0.78f); 
            coinAdTitleLabel.color = new Color(0.79f, 0.8f, 0.78f);
            coinAdIcon.spriteName = "ic_ad_gray@sprite";
        }
        public void SetEndAdButton()
        {
            coinAdBGSprite.spriteName = "coin_bg@sprite";
            coinAdBGSprite.gameObject.GetComponent<UIButton>().normalSprite = "coin_bg@sprite";

            coinAdLabel.color = new Color(0.79f, 0.72f, 0.90f);
            coinAdTitleLabel.color = new Color(0.79f, 0.72f, 0.90f);
            coinAdIcon.spriteName = "ic_ad@sprite";
        }

        public void OnAdButton()
        {
            Debug.Log("ad on");
        }


        public void OnRank()
        {
#if UNITY_ANDROID
            if (!Social.localUser.authenticated)
            {
                goolgePlay.LoginWithInit();
                return;
            }
            Scene scene = SceneManager.GetActiveScene();
            if (gameaudio != null)
            {
                gameaudio.ButtonBeepPop();
            }
            if (scene.name == "Game")
            {
                PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIyIDh6tIfEAIQAA");
            }
            else if(scene.name == "GameSpin")
            {
                PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIyIDh6tIfEAIQBw");
            }
            else if(scene.name == "TimingGame")
            {
                PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIyIDh6tIfEAIQCA");
            }

#elif (UNITY_IPHONE || UNITY_IOS )

            if (!Social.localUser.authenticated)
            {
                goolgePlay.LoginWithInit();
                return;
            }
			Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "Game")
            {
                GameCenterPlatform.ShowLeaderboardUI("timedspin", UnityEngine.SocialPlatforms.TimeScope.AllTime);
            }
            else if (scene.name == "GameSpin")
            {
                GameCenterPlatform.ShowLeaderboardUI("gamespinrank", UnityEngine.SocialPlatforms.TimeScope.AllTime);
            }
            else if (scene.name == "TimingGame")
            {
                GameCenterPlatform.ShowLeaderboardUI("timingspinrank", UnityEngine.SocialPlatforms.TimeScope.AllTime);
            }
#endif

        }
    }
}