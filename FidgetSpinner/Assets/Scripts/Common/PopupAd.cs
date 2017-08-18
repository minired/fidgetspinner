using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.Common
{
    public class PopupAd : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void AdButton()
        {
            gameObject.SetActive(false);
        }
    }
}