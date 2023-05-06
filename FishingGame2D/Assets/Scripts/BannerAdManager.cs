using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;

public class BannerAdManager : MonoBehaviour
{
    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3982814711633983/3398595390"; //ca-app-pub-3982814711633983/3398595390  test:ca-app-pub-3940256099942544/6300978111
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
  private string _adUnitId = "unused";
#endif

    BannerView bannerView;
    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });

        this.RequestBanner();
        
    }

    public void RequestBanner()
    {
        // Create a 320x50 banner at top of the screen
        bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        this.bannerView.LoadAd(request);
    }


}
