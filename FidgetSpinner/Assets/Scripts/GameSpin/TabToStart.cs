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
        public GameObject block;

        IEnumerator FadeOut()
        {
            while (tabLabel.alpha > 0f)
            {
                tabLabel.alpha -= 0.05f;
                yield return new WaitForSeconds(0.03f);
            }
            StartCoroutine(FadeIn());
        }

        IEnumerator FadeIn()
        {
            while (tabLabel.alpha < 1f)
            {
                tabLabel.alpha += 0.05f;
                yield return new WaitForSeconds(0.03f);
            }
            StartCoroutine(FadeOut());
        }

        public void GameStart()
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
            Destroy(block);
            timer.isStarted = true;
            spinner.isStarted = true;
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