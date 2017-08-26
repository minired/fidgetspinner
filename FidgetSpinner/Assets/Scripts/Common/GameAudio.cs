using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fidget.Player;
namespace Fidget.Common
{
    public class GameAudio : MonoBehaviour
    {
        public List<AudioClip> buttonBeepList;
        public List<AudioClip> effectList;
        public AudioClip gameSpinSuccess;
        // Use this for initialization
        void Start()
        {
           
        }

        public void ButtonBeepPop()
        {
            try
            {
                if (!GameInfo.isSoundOn)
                    return;
                LeanAudio.play(buttonBeepList[0]);
            }
            catch
            {

            }
        }

        public void GameSpinSuccess()
        {
            try
            {
                if (!GameInfo.isSoundOn)
                    return;
                LeanAudio.play(gameSpinSuccess);
            }
            catch
            {

            }
        }




        // Update is called once per frame
        void Update()
        {

        }
    }
}