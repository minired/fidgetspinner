using UnityEngine;
using System.Collections;
namespace Fidget.Common
{
    public class FidgetSpinner : MonoBehaviour
    {

        Vector3 zAxis = new Vector3(0, 0, 10);

        float speed = 0.0f;
        bool isSpin = false;



        public void SpeedUp(float fact)
        {
            if (speed > 500)
                return;
            speed += fact;
        }

        public void OnSpinStart()
        {
            isSpin = true;
        }

        public void OnSpinStop()
        {

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
                transform.Rotate(zAxis * Time.deltaTime * speed, Space.Self);

                if (speed > 0)
                {
                    speed -= 0.1f;
                }
                
                if(speed <= 0)
                {
                    speed = 0;
                    isSpin = false;
                }
            }
        }
    }
}