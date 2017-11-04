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
          
        }
       

        // Use this for initialization
        void Start()
        {
			#if UNITY_ANDROID
            Advertisement.Initialize("1515814", false);
			#elif (UNITY_IPHONE || UNITY_IOS )
			Advertisement.Initialize("1515815", false);
			#endif

            if (User.Instance.RunCount < 2)
            {
                googlePlay.LoginWithInit();
            }
            else
            {
                googlePlay.Init();
            }
            googlePlay.UpdateCheckAchievements();
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
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
