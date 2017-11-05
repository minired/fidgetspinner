using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Player;
using Fidget.Common;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using Fidget.Common;

namespace Fidget.Init
{
    public class Init : MonoBehaviour
    {
        public BottomAd bottomAd;

        void Awake()
        {
#if UNITY_IOS
			Application.targetFrameRate = 60;
#endif

            bottomAd.RequestBanner();
            ExpTable expTable = new ExpTable();
            if (User.Instance.RunCount < 1)
            {
                InitGame();
            }
            else
            {
                User.Instance.RunCount++;
            }
            Analytics.CustomEvent("initEvent", new Dictionary<string, object>
            {
                { "equip", User.Instance.EquipIndex },
                { "levelequip"+User.Instance.EquipIndex.ToString(), User.Instance.GetFidgetSpinnerLevel(User.Instance.EquipIndex) },
                { "coin", User.Instance.Coin },
                { "exp", expTable.GetLevel(User.Instance.Exp) }
            });
        }
        void InitGame()
        {
            User.Instance.Vibration = true;
            User.Instance.Sound = true;
            User.Instance.Alarm = true;

            User.Instance.RunCount = 1;
        }
        // Use this for initialization
        void Start()
        {
            SceneManager.LoadScene("Main");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}