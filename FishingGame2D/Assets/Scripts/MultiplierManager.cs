using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultiplierManager : MonoBehaviour
{
    public List<GameObject> ObstaclesPassed = new List<GameObject>();
    public List<GameObject> SharksPassed = new List<GameObject>();

    public GameObject[] Obstacles;
    public GameObject[] Sharks;

    public HookCollisions HookCollisions;
    public ObstacleColliders ObstacleColliders;
    public SharkOpenMouth SharkOpenMouth;
    public MenuManager MenuManager;

    private bool ifObstaclePassed = false;
    public float multiplier;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Obstacle") != null && MenuManager.isInGame == true )
        {
            GameObject nearestObstacle;
            nearestObstacle = FindNearestObstacle();
            float distanceOfNearObstacle = Vector3.Distance(this.transform.position, nearestObstacle.transform.position);

            if(HookCollisions.ifHooked == true && distanceOfNearObstacle <1.5f && ObstacleColliders.hookedFish != null && ObstaclesPassed.Contains(nearestObstacle) != true )
            {
                ObstaclesPassed.Add(nearestObstacle);
            }

           
         }

        if(GameObject.FindGameObjectWithTag("Shark") != null && MenuManager.isInGame == true)
        {
            GameObject nearestShark;
            nearestShark = FindNearestShark();
            float distanceOfNearShark = Vector3.Distance(this.transform.position, nearestShark.transform.position);

            if (HookCollisions.ifHooked == true && distanceOfNearShark < 1.5f && ObstacleColliders.hookedFish != null && SharksPassed.Contains(nearestShark) != true)
            {
                Debug.Log("Close call to shark !");
                SharksPassed.Add(nearestShark);
            }
        }

    }

    void DetectObjectWhenHooked()
    {
        if(HookCollisions.ifHooked == true && FindNearestObstacle() && ObstacleColliders.Obstacles != null && ObstacleColliders.hookedFish != null)
        {              
            Debug.Log("Close call !");
        }

        if (HookCollisions.ifHooked == true && SharkOpenMouth.sharkDistancetoHook < 1.5f && SharkOpenMouth.shark != null)
        {
            Debug.Log("Close call to shark !");
        }
    }

    public GameObject FindNearestObstacle()
    {
        Obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        GameObject nearestObstacle = null;
        float dist = Mathf.Infinity;
        Vector3 posOfHook = this.transform.position;
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

    public GameObject FindNearestShark()
    {
        Sharks = GameObject.FindGameObjectsWithTag("Shark");
        GameObject nearestShark = null;
        float dist = Mathf.Infinity;
        Vector3 posOfHook = this.transform.position;
        foreach (GameObject shrk in Sharks)
        {
            Vector3 diff = shrk.transform.position - posOfHook;
            float currDistance = diff.sqrMagnitude;
            if (currDistance < dist)
            {
                nearestShark = shrk;
                dist = currDistance;
            }
        }

        return nearestShark;
    }

}
