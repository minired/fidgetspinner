using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Common;
using UnityEngine.SceneManagement;

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

        public UILabel highScoreLabel;

        public ResultPopup resultPopup;


        float rightEventTime = 0.0f;
        bool isGameStart = false;
        float gameTime = 0.0f;

        private void Awake()
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
            resultPopup.gameObject.SetActive(false);
            gaugeUI.SetGaugeAmount(0.5f);

        }

        private void ResultPopup_popupClosed()
        {
            gameTime = 20.0f;
            isGameStart = true;
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
            if (!fidgetSpinner.IsSpin)
            {
                fidgetSpinner.SetRightDirection();
                fidgetSpinner.OnSpinStart();
                fidgetSpinner.SpeedUp(30);
                return;
            }


            if (fidgetSpinner.IsLeftDirection() && fidgetSpinner.Speed < 100)
            {
                fidgetSpinner.SpeedDown(20);
            }
            else
            {
                fidgetSpinner.SpeedUp(30);
            }
        }

        void LeftSpeedUp()
        {
            if (!isGameStart)
                return;

            if (!fidgetSpinner.IsSpin)
            {
                fidgetSpinner.SetLeftDirection();
                fidgetSpinner.OnSpinStart();
                fidgetSpinner.SpeedUp(30);
                return;
            }

            if (fidgetSpinner.IsRightDirection() && fidgetSpinner.Speed < 100)
            {
                fidgetSpinner.SpeedDown(20);
            }
            else
            {
                fidgetSpinner.SpeedUp(30);
            }
        }



        



     


        // Use this for initialization
        void Start()
        {
            gameTime = 20.0f;
            isGameStart = true;
        }

        // Update is called once per frame
        void Update()
        {
            if(!isGameStart)
            {
                return;
            }

            if(gameTime <= 0.0f)
            {
                timeLabel.text = "0";
                isGameStart = false;
                fidgetSpinner.OnSpinStop();
                resultPopup.gameObject.SetActive(true);
                return;
            }

            gameTime -= Time.deltaTime;
            timeLabel.text = gameTime.ToString("0.0");

            if (fidgetSpinner.IsSpin)
            {
                speedLabel.text = ((int)(fidgetSpinner.Speed)).ToString() + " m/s";
            }
            else
            {
                speedLabel.text = "0 m/s";
            }
        }
    }
}