using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Player;
using Fidget.Data;
using Fidget.Common;

namespace Fidget.GameSpin
{
    public class Score : MonoBehaviour
    {
        public Spinner spinner;
        public UILabel label;
        public UILabel bonusLabel;
        public UILabel levelLabel;
        public Timer timer;
        public GaugeUI gaugeUI;
        ExpTable expTable = new ExpTable();

        int bonusLevel;
        int level;

        float score;
        float scoringTime;
        float increasingAmount;
        float temp;
        float fidgetSpeed;
        float fidgetCoin;
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
            User.Instance.Exp += (int)(increasingAmount * 0.1f);
        }

        public void Init()
        {
            bonusLevel = 1;
            score = 0f;
            increasingAmount = initialAmount;
            label.text = score.ToString();
        }

        // Use this for initialization
        void Start()
        {
            int fidgetIndex = User.Instance.EquipIndex;
            level = User.Instance.GetFidgetSpinnerLevel(fidgetIndex);
			if (level < 1) 
			{
				level = 1;
			}
            fidgetSpeed = FidgetSpinnerData.fidgetSpinnerDetails[fidgetIndex, level - 1].speed;
            fidgetCoin = FidgetSpinnerData.fidgetSpinnerDetails[fidgetIndex, level - 1].coin;
            fixedSpeed += (fidgetSpeed * 0.08f);
            initialAmount += fidgetCoin * 0.08f;
            Init();
        }

        // Update is called once per frame
        void Update()
        {
            if (!timer.isStarted)
                return;
            scoringTime += Time.deltaTime;
            relativeSpeed = spinner.relativeSpeed;
            if(scoringTime > 0.1f)
            {
                temp = (relativeSpeed * fixedSpeed);
                score += temp;
                label.text = ((int)score).ToString();
                User.Instance.Exp += (int)(temp * 0.35f);
                level = expTable.GetLevel(User.Instance.Exp);
                levelLabel.text = "Lv." + level.ToString();
                float rate = expTable.GetLevelRate(level, User.Instance.Exp);
                gaugeUI.SetGaugeAmount(rate);
            }
        }
    }
}