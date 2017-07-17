using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Common;


namespace Fidget.TimeGame
{
    public class MainCamera : MonoBehaviour
    {
        public SwipeMouse swipeMouse;

        public SwipeScript swipeTouch;

        public FidgetSpinner fidgetSpinner;

        private void Awake()
        {
            swipeMouse.leftSwipe += SwipeMouse_leftSwipe;
            swipeMouse.rightSwipe += SwipeMouse_rightSwipe;
            swipeTouch.leftSwipe += SwipeTouch_leftSwipe;
            swipeTouch.rightSwipe += SwipeTouch_rightSwipe;
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
            fidgetSpinner.SpeedUp(10);
            fidgetSpinner.OnSpinStart();
        }

        void OnLeftSwipe()
        {
            fidgetSpinner.SpeedUp(10);
            fidgetSpinner.OnSpinStart();
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