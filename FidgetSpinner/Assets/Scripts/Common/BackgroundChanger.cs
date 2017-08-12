using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.Common
{
    public class BackgroundChanger : MonoBehaviour
    {



        public BackgroundSelector background1;
        public BackgroundSelector background2;

        bool isFrontFirst = true;

        private void Awake()
        {
            background1.GetComponent<SpriteRenderer>().sortingOrder = -9;
            background2.GetComponent<SpriteRenderer>().sortingOrder =  -10;
            isFrontFirst = true;
        }

        // Use this for initialization
        void Start()
        {
           
        }


        public void ChangeBackground(int index)
        {
            StopCoroutine("ChangeAlpha");
            if (isFrontFirst)
            {
                background2.SetBackground(index);
                background2.GetComponent<SpriteRenderer>().material.SetFloat("_AlphaVal", 0f);
                background1.GetComponent<SpriteRenderer>().material.SetFloat("_AlphaVal", 1f);
                background1.GetComponent<SpriteRenderer>().sortingOrder = -10;
                background2.GetComponent<SpriteRenderer>().sortingOrder = -9;
                StartCoroutine(ChangeAlpha(0.3f, background2.GetComponent<SpriteRenderer>().material));
            }
            else
            {
                background1.SetBackground(index);
                background1.GetComponent<SpriteRenderer>().material.SetFloat("_AlphaVal", 0f);
                background2.GetComponent<SpriteRenderer>().material.SetFloat("_AlphaVal", 1f);
                background1.GetComponent<SpriteRenderer>().sortingOrder = -9;
                background2.GetComponent<SpriteRenderer>().sortingOrder = -10;
                StartCoroutine(ChangeAlpha(0.3f, background1.GetComponent<SpriteRenderer>().material));
            }
            isFrontFirst = !isFrontFirst;
        }

        private IEnumerator ChangeAlpha(float changeTime, Material material)
        {
            float alphaAdd = 0.05f;
            float progressTime = changeTime * alphaAdd;
            float alpha = 0.0f;
            
            while (alpha < 1.0f)
            {
                alpha += alphaAdd;
                material.SetFloat("_AlphaVal", alpha);
                yield return new WaitForSeconds(progressTime);
            }
            alpha = 1.0f;
            material.SetFloat("_AlphaVal", alpha);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}