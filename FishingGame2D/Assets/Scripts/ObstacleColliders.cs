using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using TMPro;

public class ObstacleColliders : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "HookedFish")  //Olta yemlikyen balýða çarpýyor
        {
            if (GameObject.FindGameObjectWithTag("HookedFish") != null)
            {
                Destroy(collision.gameObject);

                /*float baitct = float.Parse(baitCount_txt.text);

                if (baitct > 0)
                {
                    baitct = baitct - 1;
                    Debug.Log("Balýk varken Yem düþtü -1");
                    baitCount_txt.text = baitct.ToString();
                }*/
            }            
        }
    }
}
