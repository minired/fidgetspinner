using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.Common
{
    public class BackgroundChanger : MonoBehaviour
    {
        public BackgroundSelector background1;
        public BackgroundSelector background2;
        // Use this for initialization
        void Start()
        {
            ChangeBackground(3);
        }


        public void ChangeBackground(int index)
        {
            background2.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 20);
            background2.SetBackground(3);
            background2.GetComponent<SpriteRenderer>().sortingOrder = background2.GetComponent<SpriteRenderer>().sortingOrder + 1;
            background2.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 20);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}