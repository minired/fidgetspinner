using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fidget.Main
{
    public class MainCamera : MonoBehaviour
    {


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void MoveGameScene()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
