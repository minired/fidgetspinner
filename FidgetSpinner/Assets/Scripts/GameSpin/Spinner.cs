﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Player;
using Fidget.Data;

namespace Fidget.GameSpin
{
    public class Spinner : MonoBehaviour
    {
        public bool isStarted;

        public Timer timer;
        public List<UIAtlas> atlasList;
        public Score score;

        public float fixedSpeed;
        public float relativeSpeed;
        public float fidgetSpeed;

        int fidgetIndex;


        void Awake()
        {
            fidgetIndex = User.Instance.EquipIndex;
        }

        void Start()
        {
            isStarted = false;
            GetComponent<UISprite>().atlas = atlasList[FidgetSpinnerData.fidgetSpinnerItems[fidgetIndex].atlasIndex];
            GetComponent<UISprite>().spriteName = FidgetSpinnerData.fidgetSpinnerItems[fidgetIndex].spriteName;
            fidgetSpeed = FidgetSpinnerData.fidgetSpinnerDetails[fidgetIndex, 1].speed;
            fixedSpeed += (fidgetSpeed * 0.7f);
        }

        void Update()
        {
            if (isStarted)
            {
                relativeSpeed = timer.GetComponent<UISprite>().fillAmount;
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.localEulerAngles.z + (fixedSpeed * relativeSpeed));
            }
        }
    }
}