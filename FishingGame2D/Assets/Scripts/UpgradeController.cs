using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    public HookCollisions hookCollisions;
    public CollectScreen collectScreen;

    public TextMeshProUGUI totalScoree;
    public TextMeshProUGUI gameScore;
    public TextMeshProUGUI increaseCapacityCostText;
    public TextMeshProUGUI multipleCatchText;

    public GameObject Upgrade2Panel;
    public Button MultipleCatchButton; 

    public float boatCapacity = 5;
    private float increaseCapacityCost = 15;
    public bool multipleCatchisOn = false;
    public string multCatch;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        MultipleCatchButton = Upgrade2Panel.GetComponent<Button>();

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
        multCatch = PlayerPrefs.GetString("MultipleCatch");

        if(multCatch == "True")
        {
            multipleCatchisOn = true;
            multipleCatchText.text = "Multiple Catch ON";
            MultipleCatchButton.interactable = false;
        }
        else if( multCatch == "False")
        {
            multipleCatchisOn = false;
        }
         
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

    public void multipleCatchUpgradeOn()
    {
        float total1 = float.Parse(totalScoree.text);
        if(total1 >= 100)
        {
            total1 = total1 - 100;
            totalScoree.text = total1.ToString();
            PlayerPrefs.SetFloat("TotalScore", total1);

            multipleCatchisOn = true;
            PlayerPrefs.SetString("MultipleCatch", multipleCatchisOn.ToString());
            multipleCatchText.text = "Multiple Catch ON";
            MultipleCatchButton.interactable = false;
        }
    }
}
