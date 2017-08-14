using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.GameSpin
{
    public class Fever : MonoBehaviour
    {
        public UISprite sprite;
        public PlanetManager manager;
        public Score score;

        bool isFever;

        public float feverCount;
        public float feverDuration;


        public void Success()
        {
            sprite.fillAmount += (1f / feverCount);
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

        // Use this for initialization
        void Start()
        {
            isFever = false;
            sprite.fillAmount = 0f;
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