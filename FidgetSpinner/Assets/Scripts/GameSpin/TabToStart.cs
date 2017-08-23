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

        public IEnumerator FadeOut()
        {
            while (true)
            {
                tabLabel.alpha -= 0.03f;
                if (tabLabel.alpha <= 0f)
                    break;
                yield return new WaitForSeconds(0.01f);
            }
            StartCoroutine(FadeIn());
        }

        IEnumerator FadeIn()
        {
            while (true)
            {
                tabLabel.alpha += 0.03f;
                if (tabLabel.alpha >= 1f)
                    break;
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

        public void Init()
        {
            StartCoroutine(FadeOut());
        }

        // Use this for initialization
        void Start()
        {
            Init();
        }


        // Update is called once per frame
        void Update()
        {
        }
    }
}