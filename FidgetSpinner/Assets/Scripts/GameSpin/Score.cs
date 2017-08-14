using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Player;

namespace Fidget.GameSpin
{
    public class Score : MonoBehaviour
    {
        public Spinner spinner;
        public UILabel label;
        public UILabel bonusLabel;

        int bonusLevel;

        float score;
        float scoringTime;
        float increasingAmount;
        public float initialAmount;
        public float fixedSpeed;
        public float relativeSpeed;

        public void SaveScore()
        {
            increasingAmount = initialAmount;
            User.Instance.ScoreGameSpin = (int)score;
        }

        public void AddBonus()
        {
            bonusLabel.text = "x" + (++bonusLevel).ToString();
            increasingAmount += 10f;
        }

        public void ResetBonus()
        {
            bonusLevel = 1;
            bonusLabel.text = "";
            increasingAmount = initialAmount;
        }

        public void Success()
        {
            score += increasingAmount;
            label.text = score.ToString();
        }

        // Use this for initialization
        void Start()
        {
            bonusLevel = 1;
            score = 0f;
            increasingAmount = initialAmount;
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