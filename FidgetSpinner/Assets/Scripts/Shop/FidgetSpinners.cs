using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.Shop
{
    public class FidgetSpinners : MonoBehaviour
    {
        private int fidgetId;
        private string fidgetName;
        private int level;
        private int buyCost;
        private ulong upgradeCost;
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
        public string FidgetName
        {
            get { return fidgetName; }
            set { fidgetName = value; }
        }
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public int BuyCost
        {
            get { return buyCost; }
            set { buyCost = value; }
        }
        public ulong UpgradeCost
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
    }
}
