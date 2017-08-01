using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.Shop
{
    public class MoveSpinners : MonoBehaviour
    {

        public float currentPositionX;
        public float currentPositionY;
        int amountOfSpinners;
        public SpringPanel panel;
        const float PADDING = 310f;

        void Start()
        {
            currentPositionX = transform.localPosition.x;
            currentPositionY = transform.localPosition.y;
            amountOfSpinners = 4;

        }
        void Update()
        {

        }

        public void MoveRight()
        {
            currentPositionX = panel.target.x;
            if (currentPositionX <= (amountOfSpinners - 1) * -PADDING)
                return;
            currentPositionX -= PADDING;

            Vector3 position = new Vector3(currentPositionX, currentPositionY, 0);
            SpringPanel.Begin(this.gameObject, position, 8f);
        }


        public void MoveLeft()
        {
            currentPositionX = panel.target.x;
            if (currentPositionX >= 0f)
                return;
            currentPositionX += PADDING;

            Vector3 position = new Vector3(currentPositionX, currentPositionY, 0);
            SpringPanel.Begin(this.gameObject, position, 8f);
        }

    }
}