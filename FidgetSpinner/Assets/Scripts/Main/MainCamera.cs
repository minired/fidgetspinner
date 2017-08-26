using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fidget.Common;
using UnityEngine.Advertisements;
using Fidget.Player;
namespace Fidget.Main
{
    public class MainCamera : MonoBehaviour
    {

        public OptionPopup optionPopUp;

        public GameAudio gameAudio;

        public GooglePlay googlePlay;


        private void Awake()
        {
           if(User.Instance.InitMobile < 1)
            {
                InitGame();
            }
        }
        void InitGame()
        {
            User.Instance.Vibration = true;
            User.Instance.Sound = true;
            User.Instance.Alarm = true;

            User.Instance.InitMobile = 2;
        }

        // Use this for initialization
        void Start()
        {
            Advertisement.Initialize("1515814", true);
            googlePlay.LoginWithInit();
            googlePlay.UpdateCheckAchievements();
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
            gameAudio.ButtonBeepPop();
            optionPopUp.gameObject.SetActive(true);
        }
    }
}
