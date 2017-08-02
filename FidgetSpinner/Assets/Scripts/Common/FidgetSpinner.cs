using UnityEngine;
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

            if (speed > 500 )
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

        // Update is called once per frame
        void Update()
        {
            if (isSpin)
            {
                transform.Rotate(zAxis * Time.deltaTime * speed * direction, Space.Self);
                if (speed > 0)
                {
                    speed -= 0.5f;
                }
            }
        }
    }
}