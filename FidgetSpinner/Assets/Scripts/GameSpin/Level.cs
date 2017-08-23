using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Player;
using Fidget.Common;

namespace Fidget.GameSpin
{
    public class Level : MonoBehaviour {

        ExpTable expTable = new ExpTable();
        public UILabel levelLabel;

        // Use this for initialization
        void Start() {
            int level = expTable.GetLevel(User.Instance.Exp);
            levelLabel.text = "Lv. " + level.ToString();
        }

        // Update is called once per frame
        void Update() {

        }
    }
}