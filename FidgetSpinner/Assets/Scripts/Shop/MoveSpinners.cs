using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.Shop
{
    public class MoveSpinners : MonoBehaviour
    {

        public float currentPosition;
        int amountOfSpinners;
        public SpringPanel panel;

        void Start()
        {
            currentPosition = transform.localPosition.x;
            amountOfSpinners = 4;

        }
        void Update()
        {

        }

        public void MoveRight()
        {
            currentPosition = panel.target.x;
            if (currentPosition <= (amountOfSpinners - 1) * -500f)
                return;
            currentPosition -= 500f;

            Vector3 position = new Vector3(currentPosition, 0, 0);
            SpringPanel.Begin(this.gameObject, position, 8f);
        }


        public void MoveLeft()
        {
            currentPosition = panel.target.x;
            if (currentPosition >= 0f)
                return;
            currentPosition += 500f;

            Vector3 position = new Vector3(currentPosition, 0, 0);
            SpringPanel.Begin(this.gameObject, position, 8f);
        }

    }
}