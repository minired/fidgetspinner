using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public void LoadPrevScene()
        {
            SceneManager.LoadScene("Main");
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}