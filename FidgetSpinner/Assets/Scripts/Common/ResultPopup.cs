using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.Common
{
    public class ResultPopup : MonoBehaviour
    {
        public delegate void PopupCloseDelegate();
        public event PopupCloseDelegate popupClosed;

        public UILabel scoreLabel;
        public UILabel highscoreLabel;

        public UILabel coinGainLabel;
        public UILabel coinMoreLabel;
        public UILabel coinAdLabel;

        public UISprite bestSprite;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void BestSpriteOn()
        {
            bestSprite.gameObject.SetActive(true);
        }

        public void BestSpriteOff()
        {
            bestSprite.gameObject.SetActive(false);
        }

        public void OnRestartGame()
        {
            gameObject.SetActive(false);
            if(popupClosed != null)
            {
                popupClosed();
            }
        }
    }
}