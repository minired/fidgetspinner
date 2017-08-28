using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Common;
using UnityEngine.SceneManagement;
using Fidget.Player;
using Fidget.Data;
using UnityEngine.Advertisements;
namespace Fidget.TimeGame
{
    public class MainCamera : MonoBehaviour
    {
        public SwipeMouse swipeMouse;

        public SwipeScript swipeTouch;

        public FidgetSpinner fidgetSpinner;

        public GaugeUI gaugeUI;

        public BackUI backBtn;

        public UILabel levelLabel;

        public UILabel timeLabel;

        public UILabel speedLabel;

        public UILabel scoreLabel;

        public ResultPopup resultPopup;

        public CoinUI coinUI;

        public BackgroundSelector backgroundSelector;


        public CoinAnimation coinAnimation;

        public MoveHandIcon moveHandIcon;

        public AdmobPlayer admobPlayer;

        float rightEventTime = 0.0f;
        bool isGameStart = false;
        float gameTime = 0.0f;
        float coinDelay = 5.0f;


        ExpTable expTable = new ExpTable();

        FidgetSpinnerDetail fidgetDetail;


        public UISprite adBtnSprite;
        public UISprite shopBtnSprite;
        public UISprite backBtnSprite;


        public GameAudio gameAudio;

        public NativeAd nativeAd;

        float highSpeed = 0.0f;

        int speedUpCount = 0;


        private void Awake()
        {

            EventInit();
            resultPopup.gameObject.SetActive(false);
            User.Instance.Score = 0;
            SetFidgetSpinnerDetail();
            SetFidgetSpinner();
            SetLevelLabel();
            SetScoreLabel();
            timeLabel.text = "20";
            speedLabel.text = "0 m/s";
            highSpeed = 0.0f;
        }

        void SetFidgetSpinnerDetail()
        {
            int equipIndex = User.Instance.EquipIndex;
            fidgetDetail = FidgetSpinnerData.GetFidgetSpinnerDetail(equipIndex, User.Instance.GetFidgetSpinnerLevel(equipIndex)-1);
        }

        void EventInit()
        {
            swipeMouse.leftSwipe += SwipeMouse_leftSwipe;
            swipeMouse.rightSwipe += SwipeMouse_rightSwipe;
            swipeTouch.leftSwipe += SwipeTouch_leftSwipe;
            swipeTouch.rightSwipe += SwipeTouch_rightSwipe;

            swipeMouse.upSwipe += SwipeMouse_upSwipe;
            swipeMouse.downSwipe += SwipeMouse_downSwipe;
            swipeTouch.upSwipe += SwipeTouch_upSwipe;
            swipeTouch.downSwipe += SwipeTouch_downSwipe;

            backBtn.backBtn += BackBtn_backBtn;
            resultPopup.popupClosed += ResultPopup_popupClosed;
        }


        void SetFidgetSpinner()
        {
            fidgetSpinner.SetSprite(User.Instance.EquipIndex);
            fidgetSpinner.SetMaxSpeed(fidgetDetail.speed);
            fidgetSpinner.SetDamping(fidgetDetail.damping);
            fidgetSpinner.SetHaste(fidgetDetail.haste);
            fidgetSpinner.SetCoin(fidgetDetail.coin);
            fidgetSpinner.SetCoinDelay(fidgetDetail.coin);
        }

        void SetLevelLabel()
        {
            int level = expTable.GetLevel(User.Instance.Exp);
            levelLabel.text = "Level. " + level.ToString();
            float rate = expTable.GetLevelRate(level, User.Instance.Exp);
            gaugeUI.SetGaugeAmount(rate);
        }

        void SetScoreLabel()
        {
            scoreLabel.text = User.Instance.Score.ToString("N0");
        }


        private void ResultPopup_popupClosed()
        {
            gameTime = 20.0f;
            coinDelay = fidgetSpinner.CoinDelay;
            User.Instance.Coin = User.Instance.Coin + ((ulong)(User.Instance.Score * 2));
            coinUI.SetCoinLabel(User.Instance.Coin);
            SetScoreLabel();
            User.Instance.Score = 0;
            coinAnimation.OnPlayAnimation();
            moveHandIcon.AnimationOn();
            fidgetSpinner.InitPosition();
            timeLabel.text = "20";
            highSpeed = 0.0f;
            speedUpCount = 0;
            nativeAd.AdDestory();
        }

        private void SwipeMouse_downSwipe()
        {
            if (swipeMouse.GetFirstPressPos().x > 250.0f)
            {
                LeftSpeedUp();
            }
            else
            {
                RightSpeedUp();
            }
        }

        private void SwipeMouse_upSwipe()
        {
            if (swipeMouse.GetFirstPressPos().x > 250.0f)
            {
                RightSpeedUp();
            }
            else
            {
                LeftSpeedUp();
            }
        }
        static int eventCount = 0;

        private void BackBtn_backBtn()
        {
            SceneManager.LoadScene("Main");
        }



        private void SwipeTouch_rightSwipe()
        {

            if (swipeTouch.GetFirstPressPos().y < 900.0f)
            {
                RightSpeedUp();
            }
            else
            {
                LeftSpeedUp();
            }
        }

        private void SwipeTouch_leftSwipe()
        {
            if (swipeTouch.GetFirstPressPos().y < 900.0f)
            {
                LeftSpeedUp();
            }
            else
            {
                RightSpeedUp();
            }
        }


        private void SwipeTouch_downSwipe()
        {
            if (swipeTouch.GetFirstPressPos().x > 500.0f)
            {
                LeftSpeedUp();
            }
            else
            {
                RightSpeedUp();
            }
        }

        private void SwipeTouch_upSwipe()
        {

            if (swipeTouch.GetFirstPressPos().x > 500.0f)
            {
                RightSpeedUp();
            }
            else
            {
                LeftSpeedUp();
            }
        }


        //Editor

        private void SwipeMouse_rightSwipe()
        {
            if (swipeTouch.GetFirstPressPos().y < 350.0f)
            {
                RightSpeedUp();
            }
            else
            {
                LeftSpeedUp();
            }
        }

        private void SwipeMouse_leftSwipe()
        {
            if (swipeTouch.GetFirstPressPos().y < 350.0f)
            {
                LeftSpeedUp();
            }
            else
            {
                RightSpeedUp();
            }
        }
        //Editor


        void RightSpeedUp()
        {
            if (!isGameStart)
                return;

            SwipeSound();


            if (!fidgetSpinner.IsSpin)
            {
                fidgetSpinner.SetRightDirection();
                fidgetSpinner.OnSpinStart();
                fidgetSpinner.SpeedUp(fidgetSpinner.Haste);
                return;
            }

            fidgetSpinner.SpeedUp(fidgetSpinner.Haste);
        }

        void SwipeSound()
        {
            speedUpCount++;
            if (speedUpCount % 3 == 0)
            {
                gameAudio.ButtonSwipe1();
            }
               
        }

        void LeftSpeedUp()
        {
            if (!isGameStart)
                return;

            SwipeSound();

            if (!fidgetSpinner.IsSpin)
            {
                fidgetSpinner.SetLeftDirection();
                fidgetSpinner.OnSpinStart();
                fidgetSpinner.SpeedUp(fidgetSpinner.Haste);
                return;
            }

            fidgetSpinner.SpeedUp(fidgetSpinner.Haste);
        }



        // Use this for initialization
        void Start()
        {
            backgroundSelector.SetBackground(User.Instance.EquipIndex);
            gameTime = 20.0f;
            coinDelay = fidgetSpinner.CoinDelay;
            moveHandIcon.AnimationOn();
        }

      
        void AdPopupChecker()
        {
            if (!admobPlayer.ShowAd())
            {
                if (Advertisement.IsReady())
                {
                    Advertisement.Show("video");
                }
            }
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main");
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (!isGameStart && !resultPopup.gameObject.activeInHierarchy)
                {
                    
                    //if (Advertisement.isShowing)
                    //{
                    //    return;
                    //}


                    moveHandIcon.AnimationOff();
                    isGameStart = true;
                }
            }
            if (!isGameStart)
            {
                return;
            }

            if (gameTime <= 0.0f)
            {
                GameInfo.gameCount++;
                timeLabel.text = "0";
                isGameStart = false;
                fidgetSpinner.OnSpinStop();
                
                if (User.Instance.Score > User.Instance.HighScore)
                {
                    User.Instance.HighScore = User.Instance.Score;
                    resultPopup.BestSpriteOn();
                    if (Social.localUser.authenticated)
                    {
                        Social.ReportScore(User.Instance.HighScore, "CgkIyIDh6tIfEAIQAA", (bool success) =>
                        {
                            // handle success or failure
                        });
                    }
                }
                else
                {
                    resultPopup.BestSpriteOff();
                }

                    //resultPopup.scoreLabel.text = User.Instance.Score.ToString();
                resultPopup.highscoreLabel.text = User.Instance.HighScore.ToString();
                resultPopup.coinGainLabel.text = "COIN  " + User.Instance.Score.ToString();
                resultPopup.coinMoreLabel.text = (User.Instance.Score * 2).ToString();
                resultPopup.coinAdLabel.text = (User.Instance.Score * 4).ToString();
                resultPopup.gameObject.SetActive(true);
                resultPopup.ShowScore(User.Instance.Score);
                resultPopup.BottomBtnAnimation();

                if(highSpeed > User.Instance.TimedSpinHighSpeed)
                {
                    User.Instance.TimedSpinHighSpeed = highSpeed;
                }
                if (GameInfo.gameCount % 5 == 0)
                {
                    AdPopupChecker();
                }
                else
                {
                    nativeAd.RequestNativeExpressAdView();
                }


                return;
            }

            gameTime -= Time.deltaTime;
            coinDelay -= Time.deltaTime;


            if (coinDelay <= 0.0f)
            {
                fidgetSpinner.IncreaseCoin();
                coinDelay = fidgetSpinner.CoinDelay;
                coinUI.SetCoinLabel(User.Instance.Coin);
            }

            timeLabel.text = gameTime.ToString("0.0");

            if (fidgetSpinner.IsSpin)
            {
                speedLabel.text = ((int)(fidgetSpinner.Speed)).ToString() + " m/s";
                SetLevelLabel();
                SetScoreLabel();

                if(fidgetSpinner.Speed > highSpeed)
                {
                    highSpeed = fidgetSpinner.Speed;
                }
            }
            else
            {
                speedLabel.text = "0 m/s";
            }
        }
    }
}