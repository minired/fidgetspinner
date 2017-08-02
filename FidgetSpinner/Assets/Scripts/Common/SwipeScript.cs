﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Fidget.Common
{

    public class SwipeScript : MonoBehaviour
    {


        private float fingerStartTime = 0.0f;
        private Vector2 fingerStartPos = Vector2.zero;

        private bool isSwipe = false;
        private float minSwipeDist = 50.0f;
        private float maxSwipeTime = 0.5f;



        public delegate void SwipeDelegate();
        public event SwipeDelegate leftSwipe;
        public event SwipeDelegate rightSwipe;


        // Update is called once per frame
        void Update()
        {
            if (Input.touchCount < 1)
            {
                return;
            }

            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        /* this is a new touch */
                        isSwipe = true;
                        fingerStartTime = Time.time;
                        fingerStartPos = touch.position;
                        break;

                    case TouchPhase.Canceled:
                        /* The touch is being canceled */
                        isSwipe = false;
                        break;

                    case TouchPhase.Ended:

                        float gestureTime = Time.time - fingerStartTime;
                        float gestureDist = (touch.position - fingerStartPos).magnitude;

                        if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist)
                        {
                            Vector2 direction = touch.position - fingerStartPos;
                            Vector2 swipeType = Vector2.zero;

                            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                            {
                                // the swipe is horizontal:
                                swipeType = Vector2.right * Mathf.Sign(direction.x);
                            }
                            else
                            {
                                // the swipe is vertical:
                                swipeType = Vector2.up * Mathf.Sign(direction.y);
                            }

                            if (swipeType.x != 0.0f)
                            {
                                if (swipeType.x > 0.0f)
                                {
                                    if(rightSwipe != null)
                                    {
                                        rightSwipe();
                                    }
                                }
                                else
                                {
                                    if (leftSwipe != null)
                                    {
                                        leftSwipe();
                                    }
                                }
                            }

                            if (swipeType.y != 0.0f)
                            {
                                if (swipeType.y > 0.0f)
                                {
                                    // MOVE UP
                                }
                                else
                                {
                                    // MOVE DOWN
                                }
                            }

                        }

                        break;
                }
            }
        }
    }
}