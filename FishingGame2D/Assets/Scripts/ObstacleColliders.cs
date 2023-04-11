using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ObstacleColliders : MonoBehaviour
{
    public GameObject hookedFish;
    public HookCollisions HookCollisions;
    public MenuManager MenuManager;
    public UpgradeController UpgradeController;

    public GameObject Worm;
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


}
