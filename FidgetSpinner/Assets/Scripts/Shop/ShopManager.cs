using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fidget.Common;
using Fidget.Player;
using Fidget.Data;

namespace Fidget.Shop
{
    public class ShopManager : MonoBehaviour
    {
        public BackgroundChanger bgChanger;
        int currentSpinner;
        int previousSpinner;
        public SpringPanel panel;

        public Fidget.Common.BackUI backUI;


        void UpdateBackground()
        {
            bgChanger.ChangeBackground(currentSpinner);
        }

        void Start()
        {
            
        }

        void Update()
        {
            currentSpinner = -(int)(Mathf.Round(panel.target.x) / 630);
            
            if (previousSpinner != currentSpinner)
                UpdateBackground();

            previousSpinner = currentSpinner;
        }

        private void Awake()
        {
            backUI.backBtn += BackUI_backBtn;
        }

        private void BackUI_backBtn()
        {
            SceneManager.LoadScene("Main");
        }
    }
}