using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Common;
using Fidget.Player;
using Fidget.Data;
using UnityEngine.SceneManagement;

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

        public BottomUI bottomUI;


        public GameObject clickIcon;

        public FidgetSpinner fidgetSpinner;

        public TimingCircle timingCircle;

        public UILabel timeLabel;

        ExpTable expTable = new ExpTable();

        FidgetSpinnerDetail fidgetDetail;

        bool isGameStart = false;

        bool isSpinStart = false;

        float fidgetPoint = 0.0f;

        float gameTime = 0.0f;

        private void Awake()
        {
            backBtn.backBtn += BackBtn_backBtn;
            resultPopup.gameObject.SetActive(false);
            User.Instance.Score = 0;
            SetFidgetSpinnerDetail();
            SetFidgetSpinner();
            SetLevelLabel();
            SetScoreLabel();
            speedLabel.text = "0 m/s";
            isSpinStart = false;
        }

        private void BackBtn_backBtn()
        {
            SceneManager.LoadScene("Main");
        }

        void SetFidgetSpinnerDetail()
        {
            int equipIndex = User.Instance.EquipIndex;
            fidgetDetail = FidgetSpinnerData.GetFidgetSpinnerDetail(equipIndex, User.Instance.GetFidgetSpinnerLevel(equipIndex));
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

        void SetScoreLabel()
        {
            scoreLabel.text = User.Instance.Score.ToString("N0");
        }
        // Use this for initialization
        void Start()
        {
            backgroundSelector.SetBackground(User.Instance.EquipIndex);
            clickIcon.SetActive(true);
            gameTime = 20.0f;
        }

        void SetLevelLabel()
        {
            int level = expTable.GetLevel(User.Instance.Exp);
            levelLabel.text = "Level. " + level.ToString();
            float rate = expTable.GetLevelRate(level, User.Instance.Exp);
            gaugeUI.SetGaugeAmount(rate);
        }

        // Update is called once per frame
        void Update()
        {

            if (isSpinStart)
            {
                gameTime -= Time.deltaTime;
                timeLabel.text = gameTime.ToString("0.0");
            }

            if (gameTime <= 0.0f)
            {
                timeLabel.text = "0";
                isGameStart = false;
                isSpinStart = false;
                fidgetSpinner.OnSpinStop();

                if (User.Instance.Score > User.Instance.HighScore)
                {
                    User.Instance.HighScore = User.Instance.Score;
                    resultPopup.BestSpriteOn();
                }
                else
                {
                    resultPopup.BestSpriteOff();
                }

                resultPopup.scoreLabel.text = User.Instance.Score.ToString();
                resultPopup.highscoreLabel.text = User.Instance.HighScore.ToString();
                resultPopup.coinGainLabel.text = "COIN" + User.Instance.Score.ToString();
                resultPopup.coinMoreLabel.text = (User.Instance.Score * 2).ToString();
                resultPopup.coinAdLabel.text = (User.Instance.Score * 4).ToString();
                resultPopup.gameObject.SetActive(true);
                resultPopup.BottomBtnAnimation();
                resultPopup.CoinBtnAnimation();
                return;
            }



            if (Input.GetMouseButtonDown(0))
            {
                if (isSpinStart)
                {
                    return;
                }
                if(!isGameStart && !resultPopup.gameObject.activeInHierarchy)
                {
                    fidgetPoint = 0.0f;
                    isSpinStart = false;
                    bottomUI.MoveStart();
                    clickIcon.SetActive(false);
                    isGameStart = true;
                    timingCircle.SpinStart();
                    return;
                }

                if (isGameStart)
                {
                    if (!fidgetSpinner.IsSpin)
                    {
                        fidgetSpinner.SetLeftDirection();
                        fidgetSpinner.OnSpinStart();
                        isSpinStart = true;
                        isGameStart = false;
                    }
                    fidgetPoint = timingCircle.GetPoint();
                    StartCoroutine(FidgetSpin());
                    gameTime = 20.0f;
                    return;
                }
            }
        }


        IEnumerator FidgetSpin()
        {
            while (true)
            {
                fidgetSpinner.SpeedUp(fidgetSpinner.Haste);
                speedLabel.text = ((int)(fidgetSpinner.Speed)).ToString() + " m/s";
                SetLevelLabel();
                SetScoreLabel();
                yield return new WaitForSeconds(0.3f);
            }
        }

    }
}