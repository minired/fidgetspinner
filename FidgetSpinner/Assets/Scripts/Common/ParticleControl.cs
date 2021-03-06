﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.Common
{
    public class ParticleControl : MonoBehaviour
    {

        public List<ParticleSystem> particleList;

        // Use this for initialization
        void Start()
        {
            StopParticles();
        }


        public void StopParticles()
        {
            foreach(ParticleSystem psys in particleList)
            {
                psys.Stop();
            }
        }

        public void StartParticles()
        {
            foreach (ParticleSystem psys in particleList)
            {
                psys.Play();
            }
        }



        // Update is called once per frame
        void Update()
        {

        }
    }
}