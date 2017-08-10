using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.Game2
{

    public class MainCamera : MonoBehaviour
    {
        public static MainCamera Instance;
        public GameObject Spinner;
        public LRObjPool Pool;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            Pool.Set();
        }
    }
}