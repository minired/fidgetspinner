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

        ExpTable expTable = new ExpTable();

        FidgetSpinnerDetail fidgetDetail;

        bool isGameStart = false;

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
            if(Input.GetMouseButtonDown(0))
            {
                if(!isGameStart && !resultPopup.gameObject.activeInHierarchy)
                {
                    bottomUI.MoveStart();
                    clickIcon.SetActive(false);
                    isGameStart = true;
                    timingCircle.SpinStart();
                    return;
                }

                if (isGameStart)
                {
                    timingCircle.SpinStop();
                    isGameStart = false;
                    return;
                }
            }
        }
    }
}