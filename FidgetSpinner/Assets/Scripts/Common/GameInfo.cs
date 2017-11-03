using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.Common
{
    public static class GameInfo
    {
        public static bool googlePlayInit = false;
#if UNITY_ANDROID
        public static bool IsIOS = false;
        public const string leaderBoardTimedSpin = "CgkIyIDh6tIfEAIQAA";

        public const string leaderBoardGameSpin = "CgkIyIDh6tIfEAIQBw";

        public const string leaderBoardLoopSpin = "CgkIyIDh6tIfEAIQCA";
#elif (UNITY_IPHONE || UNITY_IOS)
        public static bool IsIOS = true;
          
        public const string leaderBoardTimedSpin = "timedspin";

        public const string leaderBoardGameSpin = "gamespinrank";

        public const string leaderBoardLoopSpin = "timingspinrank";
#endif






        public static bool isSoundOn = true;

        public static uint gameCount = 0;
        
    }
}