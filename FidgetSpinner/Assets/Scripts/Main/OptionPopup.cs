using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Player;
using Fidget.Common;

namespace Fidget.Main
{
    public class OptionPopup : MonoBehaviour
    {
        public UISprite musicOn;
        public UISprite vibrationOn;
        public UISprite alarmOn;

        public UISprite musicOff;
        public UISprite vibrationOff;
        public UISprite alarmOff;

        public GameAudio gameAudio;

        // Use this for initialization
        void Awake()
        {
            SetIcon();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SetIcon()
        {
            if (User.Instance.Sound)
            {
                musicOn.gameObject.SetActive(true);
                musicOff.gameObject.SetActive(false);
            }
            else
            {
                musicOn.gameObject.SetActive(false);
                musicOff.gameObject.SetActive(true);
            }

            if (User.Instance.Vibration)
            {
                vibrationOn.gameObject.SetActive(true);
                vibrationOff.gameObject.SetActive(false);
            }
            else
            {
                vibrationOn.gameObject.SetActive(false);
                vibrationOff.gameObject.SetActive(true);
            }

            if (User.Instance.Alarm)
            {
                alarmOn.gameObject.SetActive(true);
                alarmOff.gameObject.SetActive(false);
            }
            else
            {
                alarmOn.gameObject.SetActive(false);
                alarmOff.gameObject.SetActive(true);
            }
        }

        public void OnMusicToggle()
        {
            User.Instance.Sound = !User.Instance.Sound;
            if (User.Instance.Sound)
            {
                musicOn.gameObject.SetActive(true);
                musicOff.gameObject.SetActive(false);
            }
            else
            {
                musicOn.gameObject.SetActive(false);
                musicOff.gameObject.SetActive(true);
            }
            GameInfo.isSoundOn = User.Instance.Sound;
            gameAudio.ButtonBeepPop();
        }
        public void OnVibrationToggle()
        {
            User.Instance.Vibration = !User.Instance.Vibration;
            if (User.Instance.Vibration)
            {
                vibrationOn.gameObject.SetActive(true);
                vibrationOff.gameObject.SetActive(false);
            }
            else
            {
                vibrationOn.gameObject.SetActive(false);
                vibrationOff.gameObject.SetActive(true);
            }
            gameAudio.ButtonBeepPop();
        }
        public void OnAlarmToggle()
        {
            User.Instance.Alarm = !User.Instance.Alarm;
            if (User.Instance.Alarm)
            {
                alarmOn.gameObject.SetActive(true);
                alarmOff.gameObject.SetActive(false);
            }
            else
            {
                alarmOn.gameObject.SetActive(false);
                alarmOff.gameObject.SetActive(true);
            }
            gameAudio.ButtonBeepPop();
        }

        public void PopupClose()
        {
            gameObject.SetActive(false);
        }
    }
}