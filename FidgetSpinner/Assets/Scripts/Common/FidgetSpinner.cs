﻿using UnityEngine;
using System.Collections;
namespace Fidget.Common
{
    public class FidgetSpinner : MonoBehaviour
    {

        Vector3 zAxis = new Vector3(0, 0, 10);

        float speed = 0.0f;
        bool isSpin = false;



        public float Speed
        {
            get
            {
                return speed;
            }
        }


        public bool IsSpin
        {
            get
            {
                return isSpin;
            }
        }

        float direction = 1f;

        public bool IsLeftDirection()
        {
            if(direction < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsRightDirection()
        {
            if (direction > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



        public void SetRightDirection()
        {
            direction = 1;
        }


        public void SetLeftDirection()
        {
            direction = -1;
        }

        public void SpeedUp(float fact)
        { 

            if (speed > 900 )
                return;
            speed += fact;
        }

        public void SpeedDown(float fact)
        {
            if (speed - fact > 0)
            {
                speed -= fact;
            }
            else
            {
                OnSpinStop();
            }
        }


        public void AlmostStop()
        {
            speed = 30;
        }



        public void OnSpinStart()
        {
            isSpin = true;
        }

        public void OnSpinStop()
        {
            speed = 0;
            isSpin = false;
        }

        // Use this for initialization
        void Start()
        {

        }

        float GetSpeedMinus()
        {
            float result = 0.0f;
            if(speed < 100)
            {
                result = - 0.6f;
            }
            else if (speed < 200)
            {
                result = -0.9f;
            }
            else if(speed < 300)
            {
                result = -1.0f;
            }
            else if (speed < 400)
            {
                result = -1.2f;
            }
            else if(speed < 500)
            {
                result = -1.4f;
            }
            else if (speed < 600)
            {
                result = -1.6f;
            }
            else if (speed < 700)
            {
                result = -1.8f;
            }
            else
            {
                result = -2.0f;
            }


            return result;

        }

        // Update is called once per frame
        void Update()
        {
            if (isSpin)
            {
                transform.Rotate(zAxis * Time.deltaTime * speed * direction, Space.Self);
                float t = GetSpeedMinus();
                speed += t;
                if(speed < 0.1f)
                {
                    OnSpinStop();
                }
            }
        }
    }
}