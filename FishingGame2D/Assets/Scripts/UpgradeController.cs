using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeController : MonoBehaviour
{
    public HookCollisions hookCollisions;
    public CollectScreen collectScreen;

    public TextMeshProUGUI totalScoree;
    public TextMeshProUGUI gameScore;


    private float boatCapacity;
    private float increaseCapacityCost = 15;
    void Start()
    {   
        
        if(PlayerPrefs.GetFloat("BoatCapacity") == 0)
        {   
           boatCapacity = 5;           
        }
        else
        {
            boatCapacity = PlayerPrefs.GetFloat("BoatCapacity");   
        }
                 
        Debug.Log("Boat capacity is: " + boatCapacity);
    }

    // Update is called once per frame
    void Update()
    {
        if(float.Parse(gameScore.text) == boatCapacity)
        {
            collectScreen.isCapacityFull = true;
        }
    }

    public void increaseBoatCapacity()
    {   
        float total = float.Parse(totalScoree.text);        
        total = total - increaseCapacityCost;
;       totalScoree.text = total.ToString();
        boatCapacity += 3;

        Debug.Log("Total Score: "+ totalScoree.text + ". Kapasite arttýrýldý. Kapasite: " + boatCapacity);
     
        PlayerPrefs.SetFloat("TotalScore", total);
        PlayerPrefs.SetFloat("BoatCapacity", boatCapacity); //playerprefs kullanýmýný düzelt
    }
}
