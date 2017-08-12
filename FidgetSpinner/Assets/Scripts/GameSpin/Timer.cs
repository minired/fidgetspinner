using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Player;
using Fidget.Data;

namespace Fidget.GameSpin
{
    public class Timer : MonoBehaviour
    {
        public UIButton rightbutton;
        public UIButton leftbutton;

        bool isGameOver;

        public float deltaAmount;
        public float successAmount;
        public float brokenAmount;
        float coin;
        float haste;
        float damping;

        int fidgetIndex;
        int level;
        
        void Start()
        {
            isGameOver = false;
            fidgetIndex = User.Instance.EquipIndex;
            level = User.Instance.GetFidgetSpinnerLevel(fidgetIndex);

            //haste = FidgetSpinnerData.fidgetSpinnerDetails[fidgetIndex, level - 1].haste;
            //damping = FidgetSpinnerData.fidgetSpinnerDetails[fidgetIndex, level - 1].damping;

            //successAmount += (haste * 0.002f);
            //deltaAmount -= (damping * 0.000001f);
        }

        // Update is called once per frame
        void Update()
        {
            this.GetComponent<UISprite>().fillAmount -= deltaAmount;
            if (this.GetComponent<UISprite>().fillAmount == 0f && !isGameOver)
                GameOver();
        }

        public void Success()
        {
            this.GetComponent<UISprite>().fillAmount += successAmount;
        }
        
        public void BrokenFail()
        {
            this.GetComponent<UISprite>().fillAmount -= brokenAmount;
        }
        
        void GameOver()
        {
        }
    }
}