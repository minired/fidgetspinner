using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Player;
using Fidget.Common;
using Fidget.Data;

namespace Fidget.GameSpin
{
    public class Fever : MonoBehaviour
    {
        public UISprite sprite;
        public PlanetManager manager;
        public Score score;
        public CoinUI coinUI;

        bool isFever;

        public float feverCount;
        public float feverDuration;

        private int cardID;
        private ulong coinBonus;

        public void Success()
        {
            sprite.fillAmount += (1f / feverCount);
        }

        public void IncreaseCoin()
        {
            User.Instance.Coin += coinBonus;
            coinUI.SetCoinLabel(User.Instance.Coin);
        }

        public void Fail()
        {
            sprite.fillAmount = 0;
        }

        void FeverOn()
        {
            isFever = true;
            score.AddBonus();
            manager.FeverOn();
        }

        void FeverOff()
        {
            isFever = false;
            manager.FeverOff();
            sprite.fillAmount = 0f;
        }

        public void Init()
        {
            FeverOff();
        }

        private void Awake()
        {
            cardID = User.Instance.EquipIndex;
            coinBonus = (ulong)FidgetSpinnerData.fidgetSpinnerDetails[cardID, User.Instance.GetFidgetSpinnerLevel(cardID) - 1].coin * 10;
        }

        // Use this for initialization
        void Start()
        {
            Init();
        }

        // Update is called once per frame
        void Update()
        {
            if (sprite.fillAmount > 0.99f && !isFever)
                FeverOn();

            if (isFever)
                sprite.fillAmount -= (Time.deltaTime / feverDuration);

            if (sprite.fillAmount < 0.01f && isFever)
                FeverOff();
        }
    }
}