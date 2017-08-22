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
        // Use this for initialization
        void Start()
        {
           
        }

        public void ButtonBeepPop()
        {
            try
            {
                if (!User.Instance.IsSound)
                    return;
                LeanAudio.play(buttonBeepList[0]);
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