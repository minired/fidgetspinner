using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.GameSpin
{
    public class TabToStart : MonoBehaviour
    {

        public UILabel tabLabel;
        public Timer timer;
        public Spinner spinner;

        IEnumerator FadeOut()
        {
            while (tabLabel.alpha > 0f)
            {
                tabLabel.alpha -= 0.03f;
                yield return new WaitForSeconds(0.01f);
            }
            StartCoroutine(FadeIn());
        }

        IEnumerator FadeIn()
        {
            while (tabLabel.alpha < 1f)
            {
                tabLabel.alpha += 0.03f;
                yield return new WaitForSeconds(0.01f);
            }
            StartCoroutine(FadeOut());
        }

        public void GameStart()
        {
            timer.isStarted = true;
            spinner.isStarted = true;
            gameObject.SetActive(false);
        }

        // Use this for initialization
        void Start()
        {
            StartCoroutine(FadeOut());
        }


        // Update is called once per frame
        void Update()
        {
        }
    }
}