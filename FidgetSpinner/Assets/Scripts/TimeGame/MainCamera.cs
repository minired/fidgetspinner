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

        private void Awake()
        {
            swipeMouse.leftSwipe += SwipeMouse_leftSwipe;
            swipeMouse.rightSwipe += SwipeMouse_rightSwipe;
            swipeTouch.leftSwipe += SwipeTouch_leftSwipe;
            swipeTouch.rightSwipe += SwipeTouch_rightSwipe;

            gaugeUI.SetGaugeAmount(0.5f);

        }

        private void SwipeTouch_rightSwipe()
        {
            OnRightSwipe();
        }

        private void SwipeTouch_leftSwipe()
        {
            OnLeftSwipe();
        }

        private void SwipeMouse_rightSwipe()
        {
            OnRightSwipe();
        }

        private void SwipeMouse_leftSwipe()
        {
            OnLeftSwipe();
        }


        void OnRightSwipe()
        {
            if(fidgetSpinner.IsSpin)
            {
                if (fidgetSpinner.IsRightDirection())
                {
                    fidgetSpinner.SpeedUp(50);
                }
                else
                {
                    fidgetSpinner.SpeedUp(-50);
                }
            }
            else
            {
                fidgetSpinner.SetRightDirection();
                fidgetSpinner.SpeedUp(50);
                fidgetSpinner.OnSpinStart();
            }
        }

        void OnLeftSwipe()
        {

            if (fidgetSpinner.IsSpin)
            {
                if (fidgetSpinner.IsLeftDirection())
                {
                    fidgetSpinner.SpeedUp(50);
                }
                else
                {
                    fidgetSpinner.SpeedUp(-50);
                }
            }
            else
            {
                fidgetSpinner.SetLeftDirection();
                fidgetSpinner.SpeedUp(50);
                fidgetSpinner.OnSpinStart();
            }
           
        }



        public void MoveBackTitle()
        {
            SceneManager.LoadScene("Main");
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