using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Fidget.Common
{

    public class SwipeScript : MonoBehaviour
    {


        private float fingerStartTime = 0.0f;
        private Vector2 fingerStartPos = Vector2.zero;
        private Vector2 fingerWorldStartPos = Vector2.zero;
        Vector2 swipeDirection = Vector2.zero;

        float minSwipeDist = 10.0f;

        private bool isSwipe = false;
        private float maxSwipeTime = 1.0f;


        bool isSwipeEvent = false;

        public delegate void SwipeDelegate();
        public event SwipeDelegate leftSwipe;
        public event SwipeDelegate rightSwipe;

        public event SwipeDelegate upSwipe;
        public event SwipeDelegate downSwipe;

        float timer = 0.0f;

        float eventTimer = 0.0f;


        private void Awake()
        {
            minSwipeDist = Mathf.Max(Screen.width, Screen.height) / 8f;
        }



        public Vector2 GetFirstPressPos()
        {
            return fingerStartPos;
        }

        bool IsCheckSwiped(Vector2 currentPos)
        {
            Vector2 currentSwipe = currentPos - fingerStartPos;
            if(currentSwipe.magnitude > minSwipeDist)
            {
                return true;
            }
            return false;
        }

        void OnSwipeDetected(Vector2 currentSwipe)
        {
            if (eventTimer < 0.0001f)
            {
                eventTimer = timer;
            }
            else
            {
                if(timer - eventTimer < 0.5f)
                {
                    return;
                }
            }
            if (currentSwipe.y > 0.5f && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                if (upSwipe != null)
                {
                    upSwipe();
                    eventTimer = timer;
                }
            }
            //swipe down
            else if (currentSwipe.y < -0.5f && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                if (downSwipe != null)
                {
                    downSwipe();
                    eventTimer = timer;
                }
            }
            //swipe left
            else if (currentSwipe.x < -0.5f && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                if (leftSwipe != null)
                {
                    leftSwipe();
                    eventTimer = timer;
                }
            }
            //swipe right
            else if (currentSwipe.x > 0.5f && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                if (rightSwipe != null)
                {
                    rightSwipe();
                    eventTimer = timer;
                }
            }
        }

        void Update()
        {

            timer += Time.deltaTime;

            if(timer > 999999)
            {
                timer = 0.0f;
                eventTimer = 0.0f;
            }

            if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0);
                if(t.phase == TouchPhase.Began)
                {
                    fingerStartPos = new Vector2(t.position.x, t.position.y);
                    isSwipeEvent = false;
                }
                else if (t.phase == TouchPhase.Moved)
                {
                    Vector2 currentTouchPos = new Vector2(t.position.x, t.position.y);
                    bool isSwipeDetected = IsCheckSwiped(currentTouchPos);
                    swipeDirection = (currentTouchPos - fingerStartPos).normalized;
                    if (!isSwipeEvent && isSwipeDetected)
                    {
                        OnSwipeDetected(swipeDirection);
                        isSwipeEvent = true;
                        //fingerStartPos = new Vector2(t.position.x, t.position.y);
                    }
                }
                else if(t.phase == TouchPhase.Ended)
                {
                    isSwipeEvent = false;
                }
              
            }


        }



    }

}