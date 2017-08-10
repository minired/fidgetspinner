using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.Shop
{
    public class FidgetSpinners : MonoBehaviour
    {
        private int fidgetId;
        private string fidgetName;
        private int upgradeCost;
        private int buyState;

        private float speed;
        private float haste;
        private float damping;
        private float coin;

        public int FigdetId
        {
            get { return fidgetId; }
            set { fidgetId = value; }
        }
        public string FigdetName
        {
            get { return fidgetName; }
            set { fidgetName = value; }
        }
        public int UpgradeCost
        {
            get { return upgradeCost; }
            set { upgradeCost = value; }
        }
        public int BuyState
        {
            get { return buyState; }
            set { buyState = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public float Haste
        {
            get { return haste; }
            set { haste = value; }
        }
        public float Damping
        {
            get { return damping; }
            set { damping = value; }
        }
        public float Coin
        {
            get { return coin; }
            set { coin = value; }
        }

        /*
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        */
    }
}
