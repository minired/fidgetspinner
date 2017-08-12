using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.GameSpin
{
    public class Score : MonoBehaviour
    {
        public Spinner spinner;
        public UILabel label;

        float score;
        float scoringTime;
        public float fixedSpeed;
        public float relativeSpeed;

        // Use this for initialization
        void Start()
        {
            score = 0f;
            label.text = score.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            scoringTime += Time.deltaTime;
            relativeSpeed = spinner.relativeSpeed;
            if(scoringTime > 0.1f)
            {
                score += (relativeSpeed * fixedSpeed);
                label.text = ((int)score).ToString();
            }

        }
    }
}