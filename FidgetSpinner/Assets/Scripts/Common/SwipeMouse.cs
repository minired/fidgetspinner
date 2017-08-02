using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.Common
{
    public class SwipeMouse : MonoBehaviour
    {

        Vector2 firstPressPos;
        Vector2 secondPressPos;
        Vector2 currentSwipe;

        public delegate void SwipeMouseDelegate();
        public event SwipeMouseDelegate leftSwipe;
        public event SwipeMouseDelegate rightSwipe;

        public event SwipeMouseDelegate upSwipe;
        public event SwipeMouseDelegate downSwipe;



        public Vector2 GetFirstPressPos()
        {
            return secondPressPos;
        }




        public void Swipe()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //save began touch 2d point
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            if (Input.GetMouseButtonUp(0))
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                //create vector from the two points
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 &&  currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    if(upSwipe != null)
                    {
                        upSwipe();
                    }
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    if (downSwipe != null)
                    {
                        downSwipe();
                    }
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    if(leftSwipe != null)
                    {
                        leftSwipe();
                    }
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    if (rightSwipe != null)
                    {
                        rightSwipe();
                    }
                }
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Swipe();
        }
    }
}