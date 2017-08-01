using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fidget.Shop
{
    public class ShopManager : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void movePrevious()
        {
            SceneManager.LoadScene("Main");
        }
    }
}