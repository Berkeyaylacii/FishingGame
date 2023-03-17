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
}
