using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public GameObject FishManager;
    public MenuManager MenuManager;

    private RewardedAd odulluReklam;
    private RewardedAd odulluReklam2x;

    private BannerView bannerAdd;
    private InterstitialAd interstitialAd;
    private InterstitialAd interstititalAd2x;

    public Button addWatchButton;
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

        this.odulluReklam2x = new RewardedAd(adUnitId);
        this.odulluReklam2x.OnUserEarnedReward += Give2xPoint;

        this.RequestBanner();
        this.RequestInterstitialAd();
        this.RequestInterstitialAd2x();
    }

    private void Update()
    {

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

    public void WatchAddToGet2xPoint()
    {
        AdRequest request2 = new AdRequest.Builder().Build();
        this.odulluReklam2x.LoadAd(request2);

        if (this.odulluReklam2x.IsLoaded())
        {
            this.odulluReklam2x.Show();
        }
    }

    void GiveReward(object sender, Reward e)
    {
        gameOverScreen.RestartGame();
        FishManager.SetActive(true);
       
    }

    void Give2xPoint(object sender, Reward e)
    {
        Debug.Log("buraya girdi");
        MenuManager.ReturnMainMenu2xCollect();
    }

    void RequestBanner()
    {
#if UNITY_ANDROID
        string bannerAddID = "ca-app-pub-3982814711633983/7152884441"; //test-id
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
        string interstitialAdID = "ca-app-pub-3982814711633983/1640931253"; //test-id
#elif UNITY_IPHONE
        string bannerAddID = "ca-app-pub-3940256099942544/1033173712";
#else
         string bannerAddID = "unexpected_platform";
#endif

        this.interstitialAd = new InterstitialAd(interstitialAdID);
        AdRequest request = new AdRequest.Builder().Build();

        this.interstitialAd.LoadAd(request);
    }


    void RequestInterstitialAd2x()                  //need trigger
    {
#if UNITY_ANDROID
        string interstitialAdID = "ca-app-pub-3982814711633983/1640931253"; //test-id
#elif UNITY_IPHONE
        string bannerAddID = "ca-app-pub-3940256099942544/1033173712";
#else
         string bannerAddID = "unexpected_platform";
#endif

        this.interstititalAd2x = new InterstitialAd(interstitialAdID);
        AdRequest request2 = new AdRequest.Builder().Build();

        this.interstitialAd.LoadAd(request2);
    }
}
