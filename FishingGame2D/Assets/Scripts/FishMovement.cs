using DG.Tweening;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] GameObject Fish;
    [SerializeField] GameObject Obstacle;

    public float _Rightspeed = 3f;

    public float _Leftspeed = 3f;

    List<GameObject> rightSpawns = new List<GameObject>();

    List<GameObject> leftSpawns = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateFishRight", 1f, 3f);
        InvokeRepeating("CreateObstacleRight", 5f, 3f);

        InvokeRepeating("CreateFishLeft", 3f, 4f);
        InvokeRepeating("CreateObstacleLeft", 12.5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {   
        foreach(GameObject obj in rightSpawns)
        {   
            if(obj != null && obj.transform.parent == null) { 
                obj.transform.position += Vector3.right * Time.deltaTime * 2f;   
            }
        }

        foreach (GameObject obj in leftSpawns)
        {
            if (obj != null && obj.transform.parent == null)
            {
                obj.transform.position += Vector3.left * Time.deltaTime * 2f;
            }               
        }
    }

    void CreateFishRight()
    {
        GameObject newFish = Instantiate(Fish, new Vector3(-8, Random.Range(0, -4), 0), Quaternion.Euler(180, 0, 180));
        rightSpawns.Add(newFish);
    }

    void CreateObstacleRight()
    {
        GameObject newObst = Instantiate(Obstacle, new Vector3(-8, Random.Range(0, -4), 0), Quaternion.Euler(180, 0, 180));
        rightSpawns.Add(newObst);
    }

    void CreateFishLeft()
    {
        GameObject newFish = Instantiate(Fish, new Vector3(8, Random.Range(0, -4), 0), Quaternion.Euler(0, 0, 0));
        leftSpawns.Add(newFish);
    }

    void CreateObstacleLeft()
    {
         GameObject newObst = Instantiate(Obstacle, new Vector3(8, Random.Range(0, -4), 0), Quaternion.Euler(0, 0, 0));
        leftSpawns.Add(newObst);
    }

}
