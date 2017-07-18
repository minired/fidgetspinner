using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.Game2
{
    public class LRObj : MonoBehaviour
    {
        public Obj Left;
        public Obj Right;

        void Start()
        {
            Set();
        }

        public void Set()
        {
            Left.Set();
            Right.Set();
        }

        public void Ani()
        {

        }

        public void Break()
        {
            Destroy(gameObject);
        }
    }
}