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
        private const int GAP = 630;

        public BackgroundChanger bgChanger;
        private int currentSpinner;
        private int previousSpinner;

        public SpringPanel springPanel;
        public UIPanel uiPanel;

        public Fidget.Common.BackUI backUI;

        public List<CardManager> cardList;

        public ExpTable expTable = new ExpTable();
        public UILabel userLevel;

        DigitChanger digitChanger = new DigitChanger();
        public UILabel coinLabel;

        void UpdateBackground()
        {
            bgChanger.ChangeBackground(currentSpinner);
        }

        void Start()
        {
            currentSpinner = User.Instance.EquipIndex;

            springPanel.transform.localPosition = new Vector3(-GAP * currentSpinner, 0, 0);
            uiPanel.clipOffset = new Vector2(GAP * currentSpinner, 0);

            previousSpinner = -1;

            userLevel.text = "Lv." + expTable.GetLevel(User.Instance.Exp);
            coinLabel.text = digitChanger.digitToString((int)User.Instance.Coin);
        }

        void Update()
        {
            currentSpinner = -(int)(Mathf.Round(springPanel.target.x) / GAP);

            if (previousSpinner != currentSpinner)
            {
                UpdateBackground();
                cardList[currentSpinner].SetGaugeChanger();
            }

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