using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.GameSpin
{
    public class Combo : MonoBehaviour {

        public UILabel label;
        public Score score;

        int combo;

        // Use this for initialization
        void Start() {
            combo = 0;
            label.text = combo.ToString();
        }

        // Update is called once per frame
        void Update() {

    }

        public void Success()
        {
            combo++;
            label.text = combo.ToString();
        }

        public void Fail()
        {
            combo = 0;
            label.text = combo.ToString();
        }
    }
}