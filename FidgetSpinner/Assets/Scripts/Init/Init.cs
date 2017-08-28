using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Player;
using Fidget.Common;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

namespace Fidget.Init
{
    public class Init : MonoBehaviour
    {
        
        void Awake()
        {
            ExpTable expTable = new ExpTable();
            if (User.Instance.RunCount < 1)
            {
                InitGame();
            }
            else
            {
                User.Instance.RunCount++;
            }
            Analytics.CustomEvent("gameOver", new Dictionary<string, object>
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