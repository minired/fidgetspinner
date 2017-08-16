using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.TimingGame
{
    public class TimingCircle : MonoBehaviour
    {

        public UISprite timbBar;
        public UISprite centerCircle;

        public UISprite pivot1;
        public UISprite pivot2;

        public GameObject particleObj1;
        public GameObject particleObj2;


        float speed = -100f;

        bool isBarSpin = false;

        float distance = 1.0f;


        private void Awake()
        {
            isBarSpin = false;
            timbBar.gameObject.SetActive(false);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // Keep us at orbitDistance from target.
            if (isBarSpin)
            {
                timbBar.gameObject.transform.RotateAround(centerCircle.transform.position, Vector3.forward, speed * Time.deltaTime);
            }
        }


        void CheckDistance()
        {
            float d1 = Vector3.Distance(timbBar.transform.position, pivot1.transform.position);
            float d2 = Vector3.Distance(timbBar.transform.position, pivot2.transform.position);
            if(d1 < d2)
            {
                distance = d1;
            }
            else
            {
                distance = d2;
            }
        }


        public float GetPoint()
        {
            if(distance < 0.1f)
            {
                return 100f - (distance * 80.0f); 
            }
            else if(distance < 0.3f)
            {
                return 100f - (distance * 120.0f);
            }
            else if(distance < 0.4f)
            {
                return 100f - (distance * 140.0f);
            }
            else
            {
                float result = 100f - (distance * 160.0f);

                if(result < 0.0f)
                {
                    return 0.0f;
                }
                else
                {
                    return result;
                }
            }
        }

        public void SpinStart()
        {
            timbBar.gameObject.SetActive(true);
            isBarSpin = true;
        }

        public void SpinStop()
        {
            isBarSpin = false;
            CheckDistance();
        }

        public void AnimationEnd(GameObject obj)
        {
            obj.SetActive(false);
        }


        public void StartPaticleAnimation()
        {
            particleObj1.gameObject.SetActive(true);
            particleObj1.GetComponent<DG.Tweening.DOTweenPath>().DORewind();
            particleObj1.GetComponent<DG.Tweening.DOTweenPath>().DOPlay();

            particleObj2.gameObject.SetActive(true);
            particleObj2.GetComponent<DG.Tweening.DOTweenPath>().DORewind();
            particleObj2.GetComponent<DG.Tweening.DOTweenPath>().DOPlay();
        }
    }
}