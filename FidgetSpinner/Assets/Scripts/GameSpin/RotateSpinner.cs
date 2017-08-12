using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.GameSpin
{
    public class RotateSpinner : MonoBehaviour
    {
        public Timer timer;
        public float fixedSpeed;
        float relativeSpeed;

        void Start()
        {
        }

        void Update()
        {
            relativeSpeed = timer.GetComponent<UISprite>().fillAmount;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.localEulerAngles.z + (fixedSpeed * relativeSpeed));
        }
    }
}