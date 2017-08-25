using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fidget.Common;

namespace Fidget.Main
{
    public class MainCamera : MonoBehaviour
    {

        public OptionPopup optionPopUp;

        public GameAudio gameAudio;

        public GooglePlay googlePlay;
        // Use this for initialization
        void Start()
        {
            googlePlay.LoginWithInit();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void MoveGameScene()
        {
            gameAudio.ButtonBeepPop();
            SceneManager.LoadScene("Game");
        }

        public void MoveGameSpinScene()
        {
            gameAudio.ButtonBeepPop();
            SceneManager.LoadScene("GameSpin");
        }

        public void MoveTimingSpinScene()
        {
            gameAudio.ButtonBeepPop();
            SceneManager.LoadScene("TimingGame");
        }

        public void OnOptionButton()
        {
            optionPopUp.gameObject.SetActive(true);
        }
    }
}
