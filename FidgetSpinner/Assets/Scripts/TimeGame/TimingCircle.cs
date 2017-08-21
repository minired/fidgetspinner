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


        public UISprite spinGood;
        public UISprite spinBad;


        public UISprite infoText;


        float speed = -100f;

        bool isBarSpin = false;

        float distance = 1.0f;


        private void Awake()
        {
            isBarSpin = false;
            timbBar.gameObject.SetActive(false);
            spinGood.gameObject.SetActive(false);
            spinBad.gameObject.SetActive(false);
            infoText.gameObject.SetActive(false);
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


        public void CheckDistance()
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


        public bool IsGoodPoint()
        {
            if(distance < 0.2f)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public void SpeedUp()
        {
            speed -= 20;
        }

        public void SpeedDown()
        {
            speed += 20;
        }

        public float GetPoint()
        {
            if (distance < 0.1f)
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

        public void SetSpeedFromFidget(float fidgetSpeed)
        {
            fidgetSpeed = fidgetSpeed * 0.5f;
            speed = (-1*fidgetSpeed) - 100.0f;
        }

        public void SpinStart()
        {
            timbBar.gameObject.SetActive(true);
            speed = -100f;
            isBarSpin = true;
        }

        public void SpinStop()
        {
            isBarSpin = false;
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

        public void ClickGood()
        {
            spinGood.gameObject.SetActive(true);
            spinBad.gameObject.SetActive(false);


            if(distance < 0.05f)
            {
                SetInfoText(4);
            }
            else if (distance < 0.1f)
            {
                SetInfoText(3);
            }
            else if (distance < 0.15f)
            {
                SetInfoText(2);
            }
            else
            {
                SetInfoText(1);
            }

            infoText.gameObject.SetActive(true);

            spinGood.GetComponent<TweenAlpha>().ResetToBeginning();
            spinGood.GetComponent<TweenAlpha>().PlayForward();

            infoText.GetComponent<TweenAlpha>().ResetToBeginning();
            infoText.GetComponent<TweenAlpha>().PlayForward();
        }

        public void ClickBad()
        {
            spinGood.gameObject.SetActive(false);
            spinBad.gameObject.SetActive(true);
            SetInfoText(0);
            infoText.gameObject.SetActive(true);

            spinBad.GetComponent<TweenAlpha>().ResetToBeginning();
            spinBad.GetComponent<TweenAlpha>().PlayForward();

            infoText.GetComponent<TweenAlpha>().ResetToBeginning();
            infoText.GetComponent<TweenAlpha>().PlayForward();
        }

        void SetInfoText(int grade)
        {
            if(grade == 0)
            {
                infoText.spriteName = "bad";
            }
            else if(grade == 1)
            {
                infoText.spriteName = "good";
            }
            else if (grade == 2)
            {
                infoText.spriteName = "great";
            }
            else if (grade == 3)
            {
                infoText.spriteName = "excellent";
            }
            else if (grade == 4)
            {
                infoText.spriteName = "perfect";
            }
        }
    }
}