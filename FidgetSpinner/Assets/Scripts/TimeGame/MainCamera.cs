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

            gaugeUI.SetGaugeAmount(0.5f);

        }

        private void SwipeTouch_downSwipe()
        {
          
            if (swipeTouch.GetFirstPressPos().x > 250.0f)
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
            if (swipeTouch.GetFirstPressPos().x > 250.0f)
            {
                RightSpeedUp();
            }
            else
            {
                LeftSpeedUp();
            }
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

        private void BackBtn_backBtn()
        {
            SceneManager.LoadScene("Main");
        }

        private void SwipeTouch_rightSwipe()
        {
            OnRightSwipe(swipeTouch.GetFirstPressPos());
        }

        private void SwipeTouch_leftSwipe()
        {
            OnLeftSwipe(swipeTouch.GetFirstPressPos());
        }

        private void SwipeMouse_rightSwipe()
        {
            OnRightSwipe(swipeMouse.GetFirstPressPos());
        }

        private void SwipeMouse_leftSwipe()
        {
            OnLeftSwipe(swipeMouse.GetFirstPressPos());
        }


        void OnRightSwipe(Vector2 startPos)
        {
            Debug.Log(startPos);
            bool isRightDir = true;

            if(startPos.y > 350)
            {
                isRightDir = false;
            }

            if (isRightDir)
            {
                RightSpeedUp();
            }
            else
            {
                LeftSpeedUp();
            }
        }
        void OnLeftSwipe(Vector2 startPos)
        {
            bool isLeftDir = true;

            if (startPos.y > 350)
            {
                isLeftDir = false;
            }
            if (isLeftDir)
            {
                LeftSpeedUp();
            }
            else
            {
                RightSpeedUp();
            }
        }
        void RightSpeedUp()
        {
            if (fidgetSpinner.IsSpin)
            {
                if (fidgetSpinner.IsRightDirection())
                {
                    fidgetSpinner.SpeedUp(30);
                }
                else
                {
                    if (fidgetSpinner.Speed > 50)
                    {
                        fidgetSpinner.AlmostStop();
                    }
                    else
                    {
                        fidgetSpinner.SpeedUp(-30);
                    }
                }
            }
            else
            {
                fidgetSpinner.SetRightDirection();
                fidgetSpinner.SpeedUp(30);
                fidgetSpinner.OnSpinStart();
            }
        }

        void LeftSpeedUp()
        {
            if (fidgetSpinner.IsSpin)
            {
                if (fidgetSpinner.IsLeftDirection())
                {
                    fidgetSpinner.SpeedUp(30);
                }
                else
                {
                    if (fidgetSpinner.Speed > 50)
                    {
                        fidgetSpinner.AlmostStop();
                    }
                    else
                    {
                        fidgetSpinner.SpeedUp(-30);
                    }
                }
            }
            else
            {
                fidgetSpinner.SetLeftDirection();
                fidgetSpinner.SpeedUp(30);
                fidgetSpinner.OnSpinStart();
            }
        }



        



     


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}