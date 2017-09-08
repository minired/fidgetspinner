using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Common;
using Fidget.Player;
using Fidget.Data;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

namespace Fidget.TimingGame
{
    public class MainCamera : MonoBehaviour
    {
        public GaugeUI gaugeUI;

        public BackUI backBtn;

        public UILabel levelLabel;

        public UILabel speedLabel;

        public UILabel scoreLabel;

        public ResultPopup resultPopup;

        public CoinUI coinUI;

        public BackgroundSelector backgroundSelector;

        public CoinAnimation coinAnimation;


        public GameObject clickIcon;

        public FidgetSpinner fidgetSpinner;

        public TimingCircle timingCircle;

        public UILabel timeLabel;

        public AdmobPlayer admobPlayer;

        ExpTable expTable = new ExpTable();

        FidgetSpinnerDetail fidgetDetail;

        bool isGameStart = false;

        bool isSpinStart = false;

        float fidgetPoint = 0.0f;

        float gameTime = 0.0f;

        public GameAudio gameaudio;

        public NativeAd nativeAd;

        private void Awake()
        {
            Init();
            InitGame();
            timeLabel.text = "30";
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

        bool IsButtonArea(Vector3 pos)
        {
            if (pos.x < 120.0f && pos.y < 120.0f)
            {
                return true;
            }

            if (pos.x > 380.0f && pos.y < 120.0f)
            {
                return true;
            }

            return false;
        }

        private void Init()
        {
            backBtn.backBtn += BackBtn_backBtn;
            resultPopup.popupClosed += ResultPopup_popupClosed;
            SetFidgetSpinnerDetail();
            SetFidgetSpinner();
        }

        private void ResultPopup_popupClosed()
        {
            gameTime = 30.0f;
            User.Instance.Coin = User.Instance.Coin + ((ulong)(User.Instance.Score * 2));
            coinUI.SetCoinLabel(User.Instance.Coin);
            SetScoreLabel();
            User.Instance.Score = 0;
            coinAnimation.OnPlayAnimation();
            clickIcon.SetActive(true);
            fidgetSpinner.InitPosition();
            timeLabel.text = "30";
            nativeAd.AdDestory();
        }

        void InitGame()
        {
            resultPopup.gameObject.SetActive(false);
            User.Instance.Score = 0;
            SetLevelLabel();
            SetScoreLabel();
            speedLabel.text = "0 m/s";
            isSpinStart = false;
        }

        private void BackBtn_backBtn()
        {
            gameaudio.ButtonBeepPop();
            SceneManager.LoadScene("Main");
        }

        void SetFidgetSpinnerDetail()
        {
            int equipIndex = User.Instance.EquipIndex;
            fidgetDetail = FidgetSpinnerData.GetFidgetSpinnerDetail(equipIndex, User.Instance.GetFidgetSpinnerLevel(equipIndex) - 1);
        }
        void SetFidgetSpinner()
        {
            fidgetSpinner.SetSprite(User.Instance.EquipIndex);
            fidgetSpinner.SetMaxSpeed(fidgetDetail.speed);
            fidgetSpinner.SetDamping(fidgetDetail.damping);
            fidgetSpinner.SetHaste(fidgetDetail.haste);
            fidgetSpinner.SetCoin(fidgetDetail.coin);
            fidgetSpinner.SetCoinDelay(fidgetDetail.coin);
            fidgetSpinner.SetLoopMode();
        }

        void SetScoreLabel()
        {
            scoreLabel.text = User.Instance.Score.ToString("N0");
        }
        // Use this for initialization
        void Start()
        {
            backgroundSelector.SetBackground(User.Instance.EquipIndex);
            clickIcon.SetActive(true);
            gameTime = 30.0f;
        }

        void SetLevelLabel()
        {
            int level = expTable.GetLevel(User.Instance.Exp);
            levelLabel.text = "Level. " + level.ToString();
            float rate = expTable.GetLevelRate(level, User.Instance.Exp);
            gaugeUI.SetGaugeAmount(rate);
        }

        void GameEnd()
        {
            GameInfo.gameCount++;
            timeLabel.text = "0";
            isGameStart = false;
            isSpinStart = false;
            fidgetSpinner.OnSpinStop();
            timingCircle.SpinStop();

            if (User.Instance.Score > User.Instance.HighScoreLoop)
            {
                User.Instance.HighScoreLoop = User.Instance.Score;
                resultPopup.BestSpriteOn();
                if (Social.localUser.authenticated)
                {
                    Social.ReportScore(User.Instance.HighScoreLoop, "CgkIyIDh6tIfEAIQCA", (bool success) =>
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
            resultPopup.highscoreLabel.text = User.Instance.HighScoreLoop.ToString();
            resultPopup.coinGainLabel.text = "COIN  " + User.Instance.Score.ToString();
            resultPopup.coinMoreLabel.text = (User.Instance.Score * 2).ToString();
            resultPopup.coinAdLabel.text = (User.Instance.Score * 4).ToString();
            resultPopup.gameObject.SetActive(true);
            resultPopup.ShowScore(User.Instance.Score);
            resultPopup.BottomBtnAnimation();
            if (GameInfo.gameCount % 5 == 0)
            {
                AdPopupChecker();
            }
            else
            {
                nativeAd.RequestNativeExpressAdView();
            }
        }

        void SpinProc()
        {
            gameTime -= Time.deltaTime;
            timeLabel.text = gameTime.ToString("0.0");
            SetLevelLabel();
            SetScoreLabel();
        }

        void GameStart()
        {
            fidgetPoint = 0.0f;
            clickIcon.SetActive(false);
            timingCircle.SpinStart();
            if (!fidgetSpinner.IsSpin)
            {
                fidgetSpinner.SetLeftDirection();
                fidgetSpinner.OnSpinStart();
            }
            fidgetPoint = timingCircle.GetPoint();
            StartCoroutine(FidgetSpin());
            gameTime = 30.0f;
            isSpinStart = true;
            isGameStart = true;
        }

        void CheckSpinClick()
        {
            timingCircle.CheckDistance();
           
            if (timingCircle.IsGoodPoint())
            {
                timingCircle.ClickGood();
                fidgetSpinner.SpeedUp(fidgetSpinner.Haste * timingCircle.GetGoodBonus());
                gameaudio.ButtonSwipe1();
                gameaudio.ButtonBeepGood();
            }
            else
            {
                timingCircle.ClickBad();
                fidgetSpinner.SpeedDown(fidgetSpinner.Damping * 3f);
                SoundManager.Instance.Vibrate();
                gameaudio.ButtonBeepBad();
            }
        }



        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main");
            }
            if (isSpinStart && gameTime <= 0.0f)
            {
                GameEnd();
                return;
            }

            if (isSpinStart)
            {
                SpinProc();
            }


            if (Input.GetMouseButtonDown(0))
            {
                if (isSpinStart)
                {
                    CheckSpinClick();
                    return;
                }
                if(!isGameStart && !resultPopup.gameObject.activeInHierarchy)
                {
                    if (IsButtonArea(Input.mousePosition))
                    {
                        return;
                    }


                    InitGame();
                    GameStart();
                    return;
                }
            }
        }


        IEnumerator FidgetSpin()
        {
            while (true)
            {
                if (!fidgetSpinner.IsSpin)
                {
                    fidgetSpinner.SetLeftDirection();
                    fidgetSpinner.OnSpinStart();
                }
                speedLabel.text = ((int)(fidgetSpinner.Speed)).ToString() + " m/s";
                SetLevelLabel();
                SetScoreLabel();
                timingCircle.SetSpeedFromFidget(fidgetSpinner.Speed);
                yield return new WaitForSeconds(0.2f);
            }
        }

    }
}