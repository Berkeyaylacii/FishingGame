using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.UI;
using JetBrains.Annotations;

public class UpgradeController : MonoBehaviour
{
    public HookCollisions hookCollisions;
    public CollectScreen collectScreen;

    public TextMeshProUGUI totalScoree;
    public TextMeshProUGUI gameScore;
    public TextMeshProUGUI increaseCapacityCostText;
    public TextMeshProUGUI multipleCatchText;

    /// for number inc-dec animation
    public TextMeshProUGUI testNumberText;
    public float countDuration = 1f;
    float currentValue;
    Coroutine Crt;
    public ParticleSystem particle;
    /// for number inc-dec animation

    public GameObject Upgrade2Panel;
    public Button MultipleCatchButton; 

    public float boatCapacity = 10;
    private float increaseCapacityCost;
    
    public string multCatch;

    public float fishLimit;
    private float increaseFishLimitCost;

    public bool multipleCatchisOn = false;
    public bool isScoreIncreasing = false;
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        
        MultipleCatchButton = Upgrade2Panel.GetComponent<Button>();

        if (PlayerPrefs.GetFloat("BoatCapacity") == 0)
        {
            boatCapacity = 10;
            PlayerPrefs.SetFloat("BoatCapacity", boatCapacity);     
        }
        else
        {
            boatCapacity = PlayerPrefs.GetFloat("BoatCapacity");   
        }

        if (PlayerPrefs.GetFloat("IncreaseCapacityCost") == 0)
        {
            increaseCapacityCost = 15;
            PlayerPrefs.SetFloat("IncreaseCapacityCost", increaseCapacityCost);
        }
        else
        {            
            increaseCapacityCost = PlayerPrefs.GetFloat("IncreaseCapacityCost");
        }

        if( PlayerPrefs.GetFloat("FishLimit") == 0 )
        {
            fishLimit = 1;
            PlayerPrefs.SetFloat("FishLimit", fishLimit);
        }
        else 
        {
            fishLimit = PlayerPrefs.GetFloat("FishLimit");
        }


        if (PlayerPrefs.GetFloat("IncreaseFishLimitCost") == 0)
        {
            increaseFishLimitCost = 25;   
            PlayerPrefs.SetFloat("IncreaseFishLimitCost", increaseFishLimitCost);
        }
        else
        {
            increaseFishLimitCost = PlayerPrefs.GetFloat("IncreaseFishLimitCost");
        }

        //Debug.Log("Boat capacity is: " + boatCapacity);
    }

    // Update is called once per frame
    void Update()
    {
        boatCapacity = PlayerPrefs.GetFloat("BoatCapacity");
        fishLimit = PlayerPrefs.GetFloat("FishLimit");
        increaseFishLimitCost = PlayerPrefs.GetFloat("IncreaseFishLimitCost");
        multCatch = PlayerPrefs.GetString("MultipleCatch");
        //Debug.Log(currentValue);

        if(multCatch == "True")
        {
            multipleCatchisOn = true;
            //multipleCatchText.text = "Multiple Catch ON";
            //MultipleCatchButton.interactable = false;
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
        float total = float.Parse( (totalScoree.text).Remove(0,1) );   // $ ICON REMOVED HERE
        if(isScoreIncreasing == false && total >= increaseCapacityCost) 
        {
            isScoreIncreasing = true;
            
            if (total >= increaseCapacityCost)
            {              
                DecreaseValue(increaseCapacityCost);   //animated number decrease
                total = total - increaseCapacityCost;

                increaseCapacityCost = ( boatCapacity * (boatCapacity / 5) ) + (2 * boatCapacity);

                PlayerPrefs.SetFloat("IncreaseCapacityCost", increaseCapacityCost);

                boatCapacity += 5;

                PlayerPrefs.SetFloat("TotalScore", total);
                PlayerPrefs.SetFloat("BoatCapacity", boatCapacity); //playerprefs kullanýmýný düzelt
                //isScoreIncreasing = false;
            }      
        }       
    }

    public void multipleCatchUpgradeOn()
    {   
        float total1 = float.Parse( (totalScoree.text).Remove(0,1) );   // $ ICON REMOVED HERE
        if (isScoreIncreasing == false && total1 >= increaseFishLimitCost)
        {   
            isScoreIncreasing = true;
            
            if (total1 >= increaseFishLimitCost)
            {
                //totalScoree.text = total1.ToString();
                DecreaseValue(increaseFishLimitCost);

                total1 = total1 - increaseFishLimitCost;
                PlayerPrefs.SetFloat("TotalScore", total1);

                increaseFishLimitCost = (boatCapacity * (boatCapacity / 5)) + (3 * boatCapacity);
                PlayerPrefs.SetFloat("IncreaseFishLimitCost", increaseFishLimitCost);

                fishLimit += 1;
                PlayerPrefs.SetFloat("FishLimit", fishLimit);

                multipleCatchisOn = true;
                PlayerPrefs.SetString("MultipleCatch", multipleCatchisOn.ToString());  
                //isScoreIncreasing = false;
            }    
        }     
    }

    IEnumerator CountTo(float value)  
    {
        currentValue = float.Parse( (totalScoree.text).Remove(0,1) );   //current value set to Total value, $ ICON REMOVED HERE

        value += currentValue;   
        var rate = Mathf.Abs(value - currentValue) / countDuration;

        while (currentValue != value)
        {
            currentValue = Mathf.MoveTowards(currentValue, value, 1.5f * rate * Time.deltaTime);
 
            totalScoree.text = (((int)currentValue).ToString()).Insert(0,"$");  //TMP text set to new value, $ ICON ADDED HERE
            yield return null;
        }

        isScoreIncreasing = false; 
    }    
    
    public void AddValue(float value)
    {   
        float target = value;
        if (Crt != null)
            StopCoroutine(Crt);

        Crt = StartCoroutine(CountTo(target));    
    }

    public void DecreaseValue(float value)
    {
        float target = -value;
        if (Crt != null)
            StopCoroutine(Crt);

        Crt = StartCoroutine(CountTo(target));
    }

}
