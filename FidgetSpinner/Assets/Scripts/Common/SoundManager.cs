using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Player;
namespace Fidget.Common
{
    public class SoundManager : Singleton<SoundManager>
    {
        protected SoundManager()
        {
        }
        public void Vibrate()
        {
            if (User.Instance.Vibration)
            {
                Handheld.Vibrate();
            }
        }
    }
}