using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Player;
using Fidget.Common;
using Fidget.Data;

namespace Fidget.GameSpin
{
    public class Timer : MonoBehaviour
    {
        public ResultPopup resultPopup;
        public Score score;
        public CoinUI coinUI;

        public bool isStarted;
        public bool isGameOver;

        public float deltaAmount;
        public float successAmount;
        public float brokenAmount;
        public float harderTime;
        public float flowedTime;
        public float coin;
        public float haste;
        public float damping;

        int fidgetIndex;
        int level;
        
        void Awake()
        {
            User.Instance.SetFidgetSpinnerLevel(0, 1);
        }

        void Start()
        {
            flowedTime = 0f;
            isStarted = false;
            isGameOver = false;
            fidgetIndex = User.Instance.EquipIndex;
            level = User.Instance.GetFidgetSpinnerLevel(fidgetIndex);

            haste = FidgetSpinnerData.fidgetSpinnerDetails[fidgetIndex, level - 1].haste;
            damping = FidgetSpinnerData.fidgetSpinnerDetails[fidgetIndex, level - 1].damping;

            successAmount += (haste * 0.002f);
            deltaAmount -= (damping * 0.000015f);
        }

        // Update is called once per frame
        void Update()
        {
            if (isGameOver)
                return;
            if (!isStarted)
                return;

            if (flowedTime >= harderTime)
            {
                Harder();
                if (harderTime < 20)
                    harderTime += 4f;
                else
                    harderTime += 7f;
            }

            if (this.GetComponent<UISprite>().fillAmount <= 0f)
            {
                isGameOver = true;
                score.SaveScore();

                if (User.Instance.ScoreGameSpin > User.Instance.HighScoreGameSpin)
                {
                    User.Instance.HighScoreGameSpin = User.Instance.ScoreGameSpin;
                    resultPopup.BestSpriteOn();
                }
                else
                {
                    resultPopup.BestSpriteOff();
                }
                User.Instance.Coin += (ulong)User.Instance.ScoreGameSpin * 2;

                //resultPopup.scoreLabel.text = User.Instance.ScoreGameSpin.ToString();
                resultPopup.highscoreLabel.text = User.Instance.HighScoreGameSpin.ToString();
                resultPopup.coinGainLabel.text = "COIN" + User.Instance.ScoreGameSpin.ToString();
                resultPopup.coinMoreLabel.text = (User.Instance.ScoreGameSpin * 2).ToString();
                resultPopup.coinAdLabel.text = (User.Instance.ScoreGameSpin * 4).ToString();
                resultPopup.gameObject.SetActive(true);
                resultPopup.ShowScore(User.Instance.ScoreGameSpin);
                resultPopup.BottomBtnAnimation();
                return;
            }
            
            this.GetComponent<UISprite>().fillAmount -= deltaAmount;
            flowedTime += Time.deltaTime;
            Debug.Log(deltaAmount);
        }

        public void Harder()
        {
            deltaAmount *= 1.4f;
        }

        public void Success()
        {
            this.GetComponent<UISprite>().fillAmount += successAmount;
        }
        
        public void BrokenFail()
        {
            this.GetComponent<UISprite>().fillAmount -= brokenAmount;
            SoundManager.Instance.Vibrate();
        }
    }
}