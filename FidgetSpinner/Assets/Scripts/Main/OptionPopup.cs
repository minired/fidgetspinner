using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Player;

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
        // Use this for initialization
        void Start()
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
        }

        public void PopupClose()
        {
            gameObject.SetActive(false);
        }
    }
}