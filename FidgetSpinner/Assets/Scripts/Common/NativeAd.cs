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
#elif UNITY_IOS || UNITY_IPHONE
        string adUnitId = "ca-app-pub-3026969986215886/5656546510";
#else
        string adUnitId = "unexpected_platform";
#endif
            float screenHeightInch = Screen.height / Screen.dpi;
            float screenWidthInch = Screen.width / Screen.dpi;

            int x = 30;
			int y = 300;

			if (screenHeightInch < 3f) 
			{
			    y = (int)(109f * screenHeightInch);
			} 
			else 
			{
				float temp = screenHeightInch - 3f;
			    y = (int)(screenHeightInch * (110f - (temp*6f)));
			}


            if (screenWidthInch < 2.5f)
            {
                x = (int)(15f * screenWidthInch);
            }
            else if (screenWidthInch < 3f)
            {
                x = (int)(25f * screenWidthInch);
            }
            else
            {
                float temp = screenWidthInch - 3f;
                x = (int)(screenWidthInch * (33f + temp));
            }

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