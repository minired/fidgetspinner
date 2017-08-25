using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using Fidget.Player;
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
            if (Advertisement.IsReady())
            {
                ShowOptions options = new ShowOptions();
                options.resultCallback = AdCallbackhandler;
                Advertisement.Show("rewardedVideo", options);
            }
            gameObject.SetActive(false);
        }
        void AdCallbackhandler(ShowResult result)
        {
            switch (result)
            {
                case ShowResult.Finished:
                    User.Instance.Coin += 100000;
                    Debug.Log("Ad Finished. Rewarding player...");
                    break;
                case ShowResult.Skipped:
                    Debug.Log("Ad skipped. Son, I am dissapointed in you");
                    break;
                case ShowResult.Failed:
                    Debug.Log("I swear this has never happened to me before");
                    break;
            }
        }
    }
}