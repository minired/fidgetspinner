﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
namespace Fidget.Common
{
    public class AdmobPlayer : MonoBehaviour
    {
        InterstitialAd interstitial;
        private void RequestInterstitial()
        {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3026969986215886/6943229560";
#elif UNITY_IOS || UNITY_IPHONE
        string adUnitId = "ca-app-pub-3026969986215886/7133102103";
#else
        string adUnitId = "ca-app-pub-3026969986215886/6943229560";
#endif

            // Initialize an InterstitialAd.
            interstitial = new InterstitialAd(adUnitId);
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            interstitial.LoadAd(request);
        }

        // Use this for initialization
        void Start()
        {
            RequestInterstitial();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool ShowAd()
        {
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}