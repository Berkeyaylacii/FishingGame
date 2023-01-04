using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    public GameObject fish;

    public bool leftSpawn;

    public float _speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateFish", 0.0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void CreateFish()
    {
        float rand = Random.Range(1, 3);
        Debug.Log(rand);
        if(rand == 1)
        {   
            Debug.Log("rotated");
            Instantiate(fish, new Vector3( 6, Random.Range(0,-4) , 0), Quaternion.Euler(0, 0, 0)); 
        }
        else 
        {
            Debug.Log("rotated");
            Instantiate(fish, new Vector3(-6, Random.Range(0,-4), 0), Quaternion.Euler(180, 0, 180));
                       
        }

    }
}
