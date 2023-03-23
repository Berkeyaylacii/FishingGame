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

    public List<GameObject> rightSpawns = new List<GameObject>();

    public List<GameObject> leftSpawns = new List<GameObject>();

    Vector3 rpos, lpos;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateFishRight", 1f, 3f);
        InvokeRepeating("CreateObstacleRight", 5f, 7f);

        InvokeRepeating("CreateFishLeft", 3f, 4f);
        InvokeRepeating("CreateObstacleLeft", 12.5f, 12f);
    }

    // Update is called once per frame
    void Update()
    {   
        foreach(GameObject obj in rightSpawns)
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
        }
    }

    void CreateFishRight()
    {
        GameObject newFish = Instantiate(Fish, new Vector3(-5, Random.Range(0, -4), 0), Quaternion.Euler(180, 0, 180));
        rightSpawns.Add(newFish);
    }

    void CreateObstacleRight()
    {
        GameObject newObst = Instantiate(Obstacle, new Vector3(-5, Random.Range(0, -4), 0), Quaternion.Euler(180, 0, 190));
        rightSpawns.Add(newObst);
    }

    void CreateFishLeft()
    {
        GameObject newFish = Instantiate(Fish, new Vector3(5, Random.Range(0, -4), 0), Quaternion.Euler(0, 0, 0));
        leftSpawns.Add(newFish);
    }

    void CreateObstacleLeft()
    {
         GameObject newObst = Instantiate(Obstacle, new Vector3(5, Random.Range(0, -4), 0), Quaternion.Euler(0, 0, 10));
        leftSpawns.Add(newObst);
    }

}
