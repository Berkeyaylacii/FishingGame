using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSpawner : MonoBehaviour
{
    public GameObject fish;
    public GameObject obstacle;
    public float _speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateFish", 0.0f, 3f);
        InvokeRepeating("CreateObstacle", 0.0f, 7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateFish()
    {
        Instantiate(fish, new Vector3(-6, Random.Range(0, -4), 0), Quaternion.Euler(180, 0, 180));
    }

    void CreateObstacle()
    {
        Instantiate(obstacle, new Vector3(-6, Random.Range(0, -4), 0), Quaternion.Euler(180, 0, 180));
    }
}
