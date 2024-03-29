using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdRewards : MonoBehaviour
{
    public GameObject FishManager;

    public GameOverScreen GameOverScreen;
    public MenuManager MenuManager;
    public HookCollisions HookCollisions;


    public void GiveReward()
    {
        GameOverScreen.RestartGame();
        FishManager.SetActive(true);
    }

    public void Give2xPoint()
    {
        HookCollisions.increaseTotalScore2x();
    }
    
}
