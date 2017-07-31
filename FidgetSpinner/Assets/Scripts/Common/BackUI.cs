using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.Common
{
    public class BackUI : MonoBehaviour
    {

        public delegate void BackButtonDelegate();
        public event BackButtonDelegate backBtn;
        // Use this for initialization
        void Start()
        {

        }

        public void OnBackBtn()
        {
            if (backBtn != null)
            {
                backBtn();
            }
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}