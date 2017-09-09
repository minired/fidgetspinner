using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
namespace Fidget.Common
{
    public class NativeAd : MonoBehaviour
    {
        NativeExpressAdView nativeExpressAdView;
        public void RequestNativeExpressAdView()
        {
#if UNITY_EDITOR
            string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3026969986215886/2601757351";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif
            int x = 30;
            int y = 410;
#if (UNITY_IPHONE || UNITY_IOS)
            y += 50;
#endif
            // Create a 320x50 native express ad at the top of the screen.
            nativeExpressAdView = new NativeExpressAdView(adUnitId, new AdSize(300, 80), x,y);
            // Load a banner ad.
            nativeExpressAdView.LoadAd(new AdRequest.Builder().Build());
        }

        public void AdDestory()
        {
            try
            {
                if (nativeExpressAdView != null)
                {
                    nativeExpressAdView.Destroy();
                }
            }
            catch
            {

            }
        }
     
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}