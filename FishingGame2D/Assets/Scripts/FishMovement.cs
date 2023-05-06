using DG.Tweening;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public MenuManager MenuManager;

    [SerializeField] GameObject Fish;
    [SerializeField] GameObject Fishv2;
    [SerializeField] GameObject Fishv3;
    [SerializeField] GameObject Shark;

    public List<GameObject> fishSpecies = new List<GameObject>();

    [SerializeField] GameObject Obstacle;

    public float _Rightspeed = 3f;

    public float _Leftspeed = 3f;

    public List<GameObject> rightSpawns = new List<GameObject>();

    public List<GameObject> leftSpawns = new List<GameObject>();

    public float maxFishCapacity;

    Vector3 rpos, lpos;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateFishRight", 1f, 1f);
        InvokeRepeating("CreateObstacleRight", 4f, 5f);

        //Level 2 fish spawners
        InvokeRepeating("CreateFishLevel2Right", 8f, 10f);
        InvokeRepeating("CreateFishLevel2Left", 4f, 15f);

        //Level 3 fish spawners
        InvokeRepeating("CreateFishLevel3Right", 8f, 10f);
        InvokeRepeating("CreateFishLevel3Left", 4f, 15f);
        //Level 3 shark spawner
        InvokeRepeating("CreateSharkRight", 15f, 20f);

        InvokeRepeating("CreateFishLeft", 3f, 2f);
        InvokeRepeating("CreateObstacleLeft", 12.5f, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        maxFishCapacity = PlayerPrefs.GetFloat("BoatCapacity");

        foreach (GameObject obj in rightSpawns)
        {   
            if(obj != null && obj.transform.parent == null) {
                if(obj.tag == "Obstacle")
                {
                    rpos = obj.transform.position;
                    rpos += Vector3.right * Time.deltaTime * 0.7f;
                    obj.transform.position = rpos + obj.transform.up * Mathf.Sin(Time.time * 2f) * 0.001f;
                }
                else
                {
                    rpos = obj.transform.position;
                    rpos +=  Vector3.right * Time.deltaTime * 1.5f;
                    obj.transform.position = rpos + obj.transform.up * Mathf.Sin(Time.time * 2f) * 0.0005f;
                }                   
            }

            if (obj != null)
            {
                var pos = obj.transform.position;
                if (Mathf.Abs(Mathf.Abs(pos.x)) > 8f)
                {
                    Destroy(obj);
                }
            }
        }

        foreach (GameObject obj in leftSpawns)
        {
            if (obj != null && obj.transform.parent == null)
            {
                if (obj.tag == "Obstacle")
                {
                    rpos = obj.transform.position;
                    rpos += Vector3.left * Time.deltaTime * 0.7f;
                    obj.transform.position = rpos + obj.transform.up * Mathf.Sin(Time.time * 2f) * 0.001f;
                }
                else
                {
                    rpos = obj.transform.position;
                    rpos += Vector3.left * Time.deltaTime * 1.5f;
                    obj.transform.position = rpos + obj.transform.up * Mathf.Sin(Time.time * 2f) * 0.0005f;
                }
            }

            if (obj != null)
            {
                var poss = obj.transform.position;
                if (Mathf.Abs(Mathf.Abs(poss.x)) > 8f)
                {
                    
                    Destroy(obj);
                }
            }
        }      




    }
   
    void CreateFishRight() //Fish level 1
    {   
        if(MenuManager.isInGame == true) 
        {
            GameObject newFish = Instantiate(Fish, new Vector3(-5, Random.Range(2f, -2f), 0), Quaternion.Euler(180, 0, 180));
            rightSpawns.Add(newFish);      
        }

    }

    void CreateFishLevel2Right()   //Fish level 2
    {   
        if(maxFishCapacity > 20 && MenuManager.isInGame == true)
        {
            GameObject newFish = Instantiate(Fishv2, new Vector3(-5, Random.Range(2f, -2f), 0), Quaternion.Euler(180, 0, 180));
            rightSpawns.Add(newFish);
        }
    }

    void CreateFishLevel3Right()   //Fish level 3
    {
        if (maxFishCapacity > 25 && MenuManager.isInGame == true)
        {
            GameObject newFish = Instantiate(Fishv3, new Vector3(-5, Random.Range(2f, -2f), 0), Quaternion.Euler(180, 0, 180));
            rightSpawns.Add(newFish);
        }
    }

    void CreateSharkRight()     //Create Shark 
    {   
        if(maxFishCapacity > 30 && MenuManager.isInGame == true)
        {
            GameObject newFish = Instantiate(Shark, new Vector3(-5, Random.Range(2f, -2f), 0), Quaternion.Euler(180, 0, 180));
            rightSpawns.Add(newFish);
        }

    }

    void CreateObstacleRight()
    {
        if (MenuManager.isInGame == true)
        {
            GameObject newObst = Instantiate(Obstacle, new Vector3(-5, Random.Range(2f, -2f), 0), Quaternion.Euler(180, 0, 190));
            rightSpawns.Add(newObst);
        }          
    }
    void CreateObstacleLeft()
    {
        if (MenuManager.isInGame == true)
        {
            GameObject newObst = Instantiate(Obstacle, new Vector3(5, Random.Range(2f, -2f), 0), Quaternion.Euler(0, 0, 15));
            leftSpawns.Add(newObst);
        }
    }

    void CreateFishLeft() //Fish level 1
    {
        if (MenuManager.isInGame == true)
        {
            GameObject newFish = Instantiate(Fish, new Vector3(5, Random.Range(2f, -2f), 0), Quaternion.Euler(0, 0, 0));
            leftSpawns.Add(newFish);
        }

    }

    void CreateFishLevel2Left()    //Fish level 2
    {
        if (maxFishCapacity > 20 && MenuManager.isInGame == true)
        {
            GameObject newFish = Instantiate(Fishv2, new Vector3(5, Random.Range(2f, -2f), 0), Quaternion.Euler(0, 0, 0));
            leftSpawns.Add(newFish);
        }
    }

    void CreateFishLevel3Left()    //Fish level 333
    {
        if (maxFishCapacity > 25 && MenuManager.isInGame == true)
        {
            GameObject newFish = Instantiate(Fishv3, new Vector3(5, Random.Range(2f, -2f), 0), Quaternion.Euler(0, 0, 0));
            leftSpawns.Add(newFish);
        }
    }

}
