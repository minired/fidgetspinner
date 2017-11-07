using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Player;
using GoogleMobileAds.Api;
namespace Fidget.Common
{
    public class PopupAd : MonoBehaviour
    {

		private RewardBasedVideoAd rewardBasedVideo;
		public void Init()
		{
			this.rewardBasedVideo = RewardBasedVideoAd.Instance;
	
			// Called when the user should be rewarded for watching a video.
			rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;

			this.RequestRewardedVideo();
		}
	

		public void HandleRewardBasedVideoRewarded(object sender, Reward args)
		{
			string type = args.Type;
			double amount = args.Amount;
			User.Instance.Coin += (ulong)amount;

		}

		private void RequestRewardedVideo()
		{
			#if UNITY_ANDROID
			string adUnitId = "ca-app-pub-3940256099942544/5224354917";
			#elif UNITY_IPHONE
			string adUnitId = "ca-app-pub-3026969986215886/7086613417";
			#else
			string adUnitId = "unexpected_platform";
			#endif

			// Create an empty ad request.
			AdRequest request = new AdRequest.Builder().Build();
			// Load the rewarded video ad with the request.
			this.rewardBasedVideo.LoadAd(request, adUnitId);
		}

		public bool IsLoded()
		{
			if (rewardBasedVideo.IsLoaded()) {
				return true;
			}
			return false;
		}
	
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
			/*
            if (Advertisement.IsReady())
            {
                ShowOptions options = new ShowOptions();
                options.resultCallback = AdCallbackhandler;
                Advertisement.Show("rewardedVideo", options);
            }
            */
			if (rewardBasedVideo.IsLoaded()) {
				rewardBasedVideo.Show();
			}

            gameObject.SetActive(false);
        }
		/*
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
        */
    }
}