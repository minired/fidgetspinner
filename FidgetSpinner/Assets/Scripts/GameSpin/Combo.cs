using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.GameSpin
{
    public class Combo : MonoBehaviour {

        public UILabel label;
        public Score score;
        public GameObject comboParent;

        int combo;

        // Use this for initialization
        void Start() {
            combo = 1;
        }

        // Update is called once per frame
        void Update() {

    }

        public void Success()
        {
            combo++;
            label.text = combo.ToString();
            comboParent.SetActive(true);
        }

        public void Fail()
        {
            combo = 0;
            comboParent.SetActive(false);
            label.text = combo.ToString();
        }
    }
}