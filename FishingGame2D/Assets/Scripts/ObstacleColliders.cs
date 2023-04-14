using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;

public class ObstacleColliders : MonoBehaviour
{
    public GameObject hookedFish;
    public HookCollisions HookCollisions;
    public MenuManager MenuManager;
    public UpgradeController UpgradeController;

    public GameObject Worm;
    public GameObject Hook;
    public GameObject[] Obstacles;

    [SerializeField] private AudioSource wormDropSound;
    public TextMeshProUGUI baitCount_txt;

    public bool decreseaBaitOnce = false;
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {       
        if (MenuManager.isInGame == true)
        {
            GameObject closestObstacle = null;
            if(GameObject.FindGameObjectWithTag("HookedFish") != null && GameObject.FindGameObjectWithTag("Obstacle") != null)
            {   
                hookedFish = GameObject.FindGameObjectWithTag("HookedFish");
                closestObstacle = FindNearestObstacle();
            }
        
            if (hookedFish != null && Obstacles != null)  { 
                if (GameObject.FindGameObjectWithTag("Hook").activeSelf == true )
                {
                    float distance = Vector3.Distance(hookedFish.transform.position, closestObstacle.transform.position);
                    
                    if (distance <= 0.85f)    //If hookedfish hits to obstacle
                    {
                        wormDropSound.Play();                  //worm drop sound
                        float baitct = float.Parse(baitCount_txt.text);      
                        if (baitct > 0)
                        {   
                            if (UpgradeController.multipleCatchisOn == false)
                            {
                                Debug.Log("single");
                                baitct = baitct - 1;
                                baitCount_txt.text = baitct.ToString();
                                Worm.SetActive(false);                           
                                HookCollisions.fishCount = 0;      //Set total hookedfish to 0                           
                                HookCollisions.ifHooked = false;
                                Destroy(hookedFish);
                            }
                            else if (UpgradeController.multipleCatchisOn == true && decreseaBaitOnce == false)
                            {
                                Debug.Log("multiple");
                                baitct = baitct - 1;
                                baitCount_txt.text = baitct.ToString();
                                Worm.SetActive(false);
                                HookCollisions.fishCount = 0;      //Set total hookedfish to 0                           
                                HookCollisions.ifHooked = false;
                                GameObject[] hookedFishes = GameObject.FindGameObjectsWithTag("HookedFish");
                                foreach (GameObject fish in hookedFishes)
                                    Destroy(fish);
                                decreseaBaitOnce = true;
                            }

                        }
                    }

                    
                }
            }

            //baithitsobstacle  MULTÝPLECATCH AÇIKKEN YEME ÇARPINCA YEM DÜÞÜYOR, ÇÖZÜLMESÝ LAZIM, ÞU AN MULTÝPLECATCH AÇIKKEN HÝÇ TEPKÝ VERMÝYOR
            GameObject closestObstacle2 = null;
            if (GameObject.FindGameObjectWithTag("Obstacle") != null && HookCollisions.ifHooked == false && Worm.activeSelf == true && UpgradeController.multipleCatchisOn == false)
            {
                closestObstacle2 = FindNearestObstacleToHook();
                float distance2 = Vector3.Distance(Hook.transform.position, closestObstacle2.transform.position);
                if(Worm.activeSelf == true)
                {               
                    if (distance2 <= 0.55f)
                    {
                        Debug.Log("Yeme obje çarptý");
                        if (Worm.activeSelf == true && HookCollisions.ifHooked == false)
                        {
                            wormDropSound.Play();             //worm hits obstacle

                            Worm.gameObject.SetActive(false);     //disappear the worm

                            float baitct = float.Parse(baitCount_txt.text);
                            if (baitct > 0)
                            {
                                baitct = baitct - 1;
                                Debug.Log("Bait -1");
                                baitCount_txt.text = baitct.ToString();
                            }
                        }
                    }
                }
            }
        }

    }

    public GameObject FindNearestObstacle()
    {
        Obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        GameObject nearestObstacle = null;
        float dist = Mathf.Infinity;
        Vector3 posOfHookedFish = hookedFish.transform.position;
        foreach (GameObject obs in Obstacles)
        {
            Vector3 diff = obs.transform.position - posOfHookedFish;
            float currDistance = diff.sqrMagnitude;
            if (currDistance < dist)
            {
                nearestObstacle = obs;
                dist = currDistance;
            }
        }

        return nearestObstacle;
    }

    public GameObject FindNearestObstacleToHook()
    {
        Obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        GameObject nearestObstacle = null;
        float dist = Mathf.Infinity;
        Vector3 posOfHook = Hook.transform.position;
        foreach (GameObject obs in Obstacles)
        {
            Vector3 diff = obs.transform.position - posOfHook;
            float currDistance = diff.sqrMagnitude;
            if (currDistance < dist)
            {
                nearestObstacle = obs;
                dist = currDistance;
            }
        }

        return nearestObstacle;
    }

}
