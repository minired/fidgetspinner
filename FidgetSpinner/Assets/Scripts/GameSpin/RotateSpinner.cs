using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.GameSpin
{
    public class RotateSpinner : MonoBehaviour
    {
        public Timer timer;
        float speed;

        void Start()
        {
        }

        void Update()
        {
            speed = timer.GetComponent<UISprite>().fillAmount;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.localEulerAngles.z + (50f * speed));
        }
    }
}