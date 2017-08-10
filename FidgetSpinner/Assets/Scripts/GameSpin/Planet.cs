using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.GameSpin
{
    public class Planet : MonoBehaviour
    {
        public bool isPlanet = false;

        public Sprite planet;
        public Sprite broken;
        public Sprite stone;

        public void setSprite(int n)
        {
            switch (n)
            {
                case 0:
                    this.GetComponent<UISprite>().spriteName = "planet@sprite";
                    isPlanet = true;
                    break;
                case 1:
                    this.GetComponent<UISprite>().spriteName = "broken@sprite";
                    isPlanet = false;
                    break;
                case 2:
                    this.GetComponent<UISprite>().spriteName = "stone@sprite";
                    isPlanet = false;
                    break;
                default:
                    break;
            }
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}