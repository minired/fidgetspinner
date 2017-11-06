using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
namespace Fidget.Common
{
    public class BottomAd : MonoBehaviour
    {
        private BannerView bannerView;

        // Use this for initialization
        void Start()
        {
        }
        public void RequestBanner()
        {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3026969986215886/2438843858";
#elif (UNITY_IPHONE || UNITY_IOS)
            string adUnitId = "ca-app-pub-3026969986215886/1548164513";
#else
            string adUnitId = "unexpected_platform";
#endif

            // Create a 320x50 banner at the top of the screen.
            bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();

            // Load the banner with the request.
            bannerView.LoadAd(request);
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}