﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fidget.Player;
namespace Fidget.Common
{
    public class GameAudio : MonoBehaviour
    {
        public List<AudioClip> buttonBeepList;
        public List<AudioClip> effectList;
        public AudioClip gameSpinSuccess;
        // Use this for initialization
        void Start()
        {
           
        }

        public void ButtonBeepPop()
        {
            try
            {
                if (!GameInfo.isSoundOn)
                    return;
                LeanAudio.play(buttonBeepList[0]);
            }
            catch
            {

            }
        }

        public void ButtonSwipe1()
        {
            try
            {
                if (!GameInfo.isSoundOn)
                    return;
                LeanAudio.play(effectList[0]);
            }
            catch
            {

            }
        }
        public void ButtonSwipe2()
        {
            try
            {
                if (!GameInfo.isSoundOn)
                    return;
                LeanAudio.play(effectList[1]);
            }
            catch
            {

            }
        }

        public void ButtonSwipe3()
        {
            try
            {
                if (!GameInfo.isSoundOn)
                    return;
                LeanAudio.play(effectList[2]);
            }
            catch
            {

            }
        }

        public void ButtonBeepGood()
        {
            try
            {
                if (!GameInfo.isSoundOn)
                    return;
                LeanAudio.play(effectList[3]);
            }
            catch
            {

            }
        }

        public void ButtonBeepBad()
        {
            try
            {
                if (!GameInfo.isSoundOn)
                    return;
                LeanAudio.play(effectList[4]);
            }
            catch
            {

            }
        }

        public void GameSpinSuccess()
        {
            try
            {
                if (!GameInfo.isSoundOn)
                    return;
                LeanAudio.play(gameSpinSuccess);
            }
            catch
            {

            }
        }




        // Update is called once per frame
        void Update()
        {

        }
    }
}