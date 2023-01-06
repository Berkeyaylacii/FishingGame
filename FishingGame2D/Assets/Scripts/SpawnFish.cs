using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    public GameObject fish;
    public GameObject obstacle;
    public float _speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateFish", 0.0f, 2f);
        InvokeRepeating("CreateObstacle", 0.0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateFish()
    {
        float rand = Random.Range(1, 3);
        if(rand == 1)
        {
            Instantiate(fish, new Vector3( 6, Random.Range(0,-4) , 0), Quaternion.Euler(0, 0, 0)); 
        }
        else 
        {
            Instantiate(fish, new Vector3(-6, Random.Range(0,-4), 0), Quaternion.Euler(180, 0, 180));                 
        }
    }

    void CreateObstacle()
    {
        float rand = Random.Range(1, 3);
        if (rand == 1)
        {
            Instantiate(obstacle, new Vector3(6, Random.Range(0, -4), 0), Quaternion.Euler(0, 0, 0));
        }
        else
        {
            Instantiate(obstacle, new Vector3(-6, Random.Range(0, -4), 0), Quaternion.Euler(180, 0, 180));

        }
    }
}
