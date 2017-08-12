using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.TimeGame
{
    public class MoveHandIcon : MonoBehaviour
    {
        Vector3 originPos;

        void Awake()
        {
            originPos = gameObject.transform.position;
        }

        // Use this for initialization
        void Start()
        {
        }

        
        void EndAnimation()
        {
            StartCoroutine(WaitAndAnimation());
        }


        public void AnimationOn()
        {
            if (LeanTween.isTweening(gameObject))
            {
                LeanTween.cancel(gameObject);
            }
            LeanTween.alpha(gameObject, 1.0f, 0.0f);
            gameObject.transform.position = originPos;
            gameObject.SetActive(true);
            LeanTween.moveLocalX(gameObject, 0.8f, 1.0f).setEase(LeanTweenType.easeInCirc).setOnComplete(EndAnimation);
            LeanTween.alpha(gameObject, 0.0f, 0.5f).setDelay(0.8f);
        }

        public void AnimationOff()
        {
            StopCoroutine(WaitAndAnimation());
            if (LeanTween.isTweening(gameObject))
            {
                LeanTween.cancel(gameObject);
            }
            LeanTween.alpha(gameObject, 1.0f, 0.0f);
            gameObject.transform.position = originPos;
            gameObject.SetActive(false);
        }

        IEnumerator WaitAndAnimation()
        {
            yield return new WaitForSeconds(0.5f);
            AnimationOn();

            yield return null;
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}