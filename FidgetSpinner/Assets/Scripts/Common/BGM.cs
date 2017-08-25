using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Player;
namespace Fidget.Common
{
    public class BGM : MonoBehaviour
    {
        private static BGM instance;

        private static bool isCurPlaying = false;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }

        IEnumerator CheckMusioOn()
        {
            while (true)
            {
                if (isCurPlaying != GameInfo.isSoundOn)
                {
                    if (GameInfo.isSoundOn)
                    {
                        PlayBGM();
                    }
                    else
                    {
                        StopBGM();
                    }
                    isCurPlaying = GameInfo.isSoundOn;
                }
                yield return new WaitForSeconds(0.5f);
            }
        }

        public void PlayBGM()
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
        public void StopBGM()
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }
        

        // Use this for initialization
        void Start()
        {
            if (!User.Instance.Sound)
            {
                gameObject.GetComponent<AudioSource>().Stop();
            }
            else
            {
                gameObject.GetComponent<AudioSource>().Play();
            }
            GameInfo.isSoundOn = User.Instance.Sound;
            isCurPlaying = GameInfo.isSoundOn;
            StartCoroutine(CheckMusioOn());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}