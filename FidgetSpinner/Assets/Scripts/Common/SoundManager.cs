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

#if (UNITY_IPHONE || UNITY_IOS || UNITY_ANDROID)
            if (User.Instance.Vibration)
            {
                Handheld.Vibrate();
            }
#endif
        }
    }
}