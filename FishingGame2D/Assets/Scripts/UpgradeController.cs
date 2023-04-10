using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.UIElements;

public class UpgradeController : MonoBehaviour
{
    public HookCollisions hookCollisions;
    public CollectScreen collectScreen;

    public TextMeshProUGUI totalScoree;
    public TextMeshProUGUI gameScore;
    public TextMeshProUGUI increaseCapacityCostText;

    public float boatCapacity = 5;
    private float increaseCapacityCost = 15;
    public bool multipleCatchisOn = false;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetFloat("BoatCapacity") == 0)
        {
            boatCapacity = 5;
            PlayerPrefs.SetFloat("BoatCapacity", boatCapacity);     
        }
        else
        {
            boatCapacity = PlayerPrefs.GetFloat("BoatCapacity");   
        }

        if (PlayerPrefs.GetFloat("increaseCapacityCost") == 0)
        {
            increaseCapacityCost = 15;
            PlayerPrefs.SetFloat("IncreaseCapacityCost", increaseCapacityCost);
        }
        else
        {
            increaseCapacityCost = PlayerPrefs.GetFloat("IncreaseCapacityCost");
        }

        //Debug.Log("Boat capacity is: " + boatCapacity);
    }

    // Update is called once per frame
    void Update()
    {
        boatCapacity = PlayerPrefs.GetFloat("BoatCapacity");

        //Debug.Log(boatCapacity + " " + increaseCapacityCost);

        if (float.Parse(gameScore.text) == boatCapacity)
        {
            collectScreen.isCapacityFull = true;
        }
    }

    public void increaseBoatCapacity()
    {   
        float total = float.Parse(totalScoree.text);  
        if(total >= increaseCapacityCost)
        {
            total = total - increaseCapacityCost;
;           totalScoree.text = total.ToString();

            increaseCapacityCost += 15;
            PlayerPrefs.SetFloat("IncreaseCapacityCost", increaseCapacityCost);

            boatCapacity += 3;
        }

        //Debug.Log("Total Score: "+ totalScoree.text + ". Kapasite arttýrýldý. Kapasite: " + boatCapacity);    
        PlayerPrefs.SetFloat("TotalScore", total);
        PlayerPrefs.SetFloat("BoatCapacity", boatCapacity); //playerprefs kullanýmýný düzelt
    }

    public void multipleCatch()
    {
        multipleCatchisOn = false;
    }
}
