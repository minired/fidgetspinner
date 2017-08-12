using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.GameSpin
{
    public class Score : MonoBehaviour
    {

        int score;
        int successAmount;
        public UILabel label;

        // Use this for initialization
        void Start()
        {
            score = 0;
            successAmount = 10;
            label.text = score.ToString();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Success()
        {
            score += successAmount;
            label.text = score.ToString();
        }
    }
}