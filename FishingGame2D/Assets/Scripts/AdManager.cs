using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public GameObject FishManager;

    private RewardedAd odulluReklam;
    private BannerView bannerAdd;
    private InterstitialAd interstitialAd;

    void Start()
    {        
        string adUnitId ;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3982814711633983/1640931253";

#elif UNITY_IPHONE
        adUnitId = "ca-app-pub-3940256099942544/1033173712";

#else 
        adUnitId = "unexpected_platform";
#endif  
        this.odulluReklam = new RewardedAd(adUnitId);    
        this.odulluReklam.OnUserEarnedReward += GiveReward;

        this.RequestBanner();
        this.RequestInterstitialAd();
    }

    public void WatchAdToPlayAgain()
    {
        
        AdRequest request = new AdRequest.Builder().Build();
        this.odulluReklam.LoadAd(request);
        

        if (this.odulluReklam.IsLoaded())
        {
            this.odulluReklam.Show();
        }
    }

    void GiveReward(object sender, Reward e)
    {
        gameOverScreen.RestartGame();
        FishManager.SetActive(true);
    }

    void RequestBanner()
    {
#if UNITY_ANDROID
        string bannerAddID = "ca-app-pub-3940256099942544/6300978111"; //test-id
#elif UNITY_IPHONE
        string bannerAddID = "ca-app-pub-3940256099942544/1033173712";
#else
         string bannerAddID = "unexpected_platform";
#endif
        //AdSize adsize = new AdSize(50, 50);
        this.bannerAdd = new BannerView(bannerAddID, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        this.bannerAdd.LoadAd(request);
    }

    void RequestInterstitialAd()                  //need trigger
    {
#if UNITY_ANDROID
        string interstitialAdID = "ca-app-pub-3940256099942544/1033173712"; //test-id
#elif UNITY_IPHONE
        string bannerAddID = "ca-app-pub-3940256099942544/1033173712";
#else
         string bannerAddID = "unexpected_platform";
#endif

        this.interstitialAd = new InterstitialAd(interstitialAdID);
        AdRequest request = new AdRequest.Builder().Build();

        this.interstitialAd.LoadAd(request);
    }
}