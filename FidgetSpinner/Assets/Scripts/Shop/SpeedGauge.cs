using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.Shop
{
    public class SpeedGauge : MonoBehaviour
    {

        public Fidget.Common.GaugeUI gauge;

        // Use this for initialization
        void Start()
        {
            gauge.SetGaugeAmount(0.5f);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}