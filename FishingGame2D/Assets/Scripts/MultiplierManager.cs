using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class MultiplierManager : MonoBehaviour
{
    public List<GameObject> ObstaclesPassed = new List<GameObject>();
    public List<GameObject> SharksPassed = new List<GameObject>();

    public GameObject[] Obstacles;
    public GameObject[] Sharks;
    public GameObject FloatingText;

    public HookCollisions HookCollisions;
    public ObstacleColliders ObstacleColliders;
    public SharkOpenMouth SharkOpenMouth;
    public MenuManager MenuManager;

    public TextMeshProUGUI multiplierText;

    private bool ifObstaclePassed = false;
    public float multiplier;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Obstacle") != null && MenuManager.isInGame == true )
        {
            GameObject nearestObstacle;
            nearestObstacle = FindNearestObstacle();
            float distanceOfNearObstacle = Vector3.Distance(this.transform.position, nearestObstacle.transform.position);
            if(HookCollisions.fishCount > 0 && distanceOfNearObstacle <1.5f && ObstacleColliders.hookedFish != null && ObstaclesPassed.Contains(nearestObstacle) != true )
            {
                Debug.Log("Obje yakýn geçti !");
                ObstaclesPassed.Add(nearestObstacle);
                ShowFloatingText(nearestObstacle);
            }

           
         }

        if(GameObject.FindGameObjectWithTag("Shark") != null && MenuManager.isInGame == true)
        {
            GameObject nearestShark;
            nearestShark = FindNearestShark();
            float distanceOfNearShark = Vector3.Distance(this.transform.position, nearestShark.transform.position);

            if (HookCollisions.fishCount > 0 && distanceOfNearShark < 1.5f && ObstacleColliders.hookedFish != null && SharksPassed.Contains(nearestShark) != true)
            {
                Debug.Log("Köpekbalýðý yakýn geçti !");
                SharksPassed.Add(nearestShark);
                ShowFloatingText(nearestShark);             
            }
        }

    }

    void ShowFloatingText(GameObject a)
    {
        GameObject textObj = Instantiate(FloatingText, a.transform.position, Quaternion.identity);
        SetFloatingText();
        multiplierText.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Restart);
        Destroy(textObj, 1f);
    }

    void SetFloatingText()
    {
        float multiplier = 0;
        multiplier = float.Parse(multiplierText.text);
        multiplier += 0.1f;
        multiplierText.text = multiplier.ToString();
        Debug.Log(multiplier);
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
