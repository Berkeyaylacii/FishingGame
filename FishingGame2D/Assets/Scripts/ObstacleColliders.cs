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
    public GameObject Hook;
    public GameObject[] Obstacles;

    public ParticleSystem obstacleHitParticle;

    [SerializeField] private AudioSource wormDropSound;
    public TextMeshProUGUI baitCount_txt;
    public TextMeshProUGUI multiplier_txt;

    public bool decreseaBaitOnce = false;
    public float distanceOfNearObstacle = 0;
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {   
        if (MenuManager.isInGame == true)      //Fish hits obstacle
        {   
            GameObject closestObstacle = null;
            if (GameObject.FindGameObjectWithTag("HookedFish") != null && GameObject.FindGameObjectWithTag("Obstacle") != null)
            {   
                hookedFish = GameObject.FindGameObjectWithTag("HookedFish");
                closestObstacle = FindNearestObstacle();
            }
           

            if (hookedFish != null && Obstacles != null && GameObject.FindGameObjectWithTag("Hook") != null )
            {
                if (GameObject.FindGameObjectWithTag("Hook").activeSelf == true)
                {   
                    
                    distanceOfNearObstacle = Vector3.Distance(hookedFish.transform.position, closestObstacle.transform.position);

                    if (distanceOfNearObstacle <= 0.85f)    //If hookedfish hits to obstacle
                    {
                        wormDropSound.Play();                  //worm drop sound
                        obstacleHitParticle = closestObstacle.GetComponent<ParticleSystem>();
                        obstacleHitParticle.Play();
                        multiplier_txt.text = "1";  //reset to 1 when hit object

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

        if (MenuManager.isInGame == true) {          //Bait hits obstacle
            GameObject closestObstacle2 = null;
            if (GameObject.FindGameObjectWithTag("Obstacle") != null && HookCollisions.ifHooked == false && Worm.activeSelf == true && HookCollisions.fishCount == 0)
            {
                closestObstacle2 = FindNearestObstacleToHook();
                float distance2 = Vector3.Distance(Hook.transform.position, closestObstacle2.transform.position);
                if(Worm.activeSelf == true)
                {               
                    if (distance2 <= 0.55f)
                    {
                        
                        if (Worm.activeSelf == true && HookCollisions.ifHooked == false)
                        {
                            wormDropSound.Play();             //worm hits obstacle
                            obstacleHitParticle = closestObstacle2.GetComponent<ParticleSystem>();
                            obstacleHitParticle.Play();

                            Debug.Log("Particle played");
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

        if (MenuManager.isInGame == false)  //Disable the fish bubbles when not in Game
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Fish"))
            {
                obj.GetComponent<ParticleSystem>().Stop();
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
