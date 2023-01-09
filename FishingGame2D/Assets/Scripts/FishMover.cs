using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishMover : MonoBehaviour
{
    public GameObject worm;

    public GameObject fish;

    public Rigidbody2D rbofFish;

    TextMeshProUGUI score_txt;

    public bool ifFishHooked = false;
    public float _speed = 2f;

    void Start()
    {
        worm = GameObject.FindGameObjectWithTag("Worm");

        score_txt = GameObject.Find("Canvas/Score").GetComponent<TextMeshProUGUI>();
    }

  
    void Update()
    {
        if(ifFishHooked == false)
        {   
            transform.position += Vector3.left * Time.deltaTime * _speed;         
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.tag == "Hook" )
        {
                ifFishHooked = true;
                    
                if (fish.transform.rotation.eulerAngles.y == 180)
                {
                    fish.transform.rotation = Quaternion.identity ;
                }
     
                transform.SetParent(GameObject.FindGameObjectWithTag("Hanger").transform, true);
                transform.position = GameObject.FindGameObjectWithTag("Hanger").transform.position + new Vector3(0.5f, 0, 0);              
        }

        if(collision.gameObject.tag == "FishingBag" )
        {
            float skor = float.Parse(score_txt.text);
            skor = skor + 1;
            score_txt.text = skor.ToString();

            Destroy(gameObject);
        }    

        if (collision.gameObject.tag == "Destroyer")
        {      
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Obstacle" && ifFishHooked == true)
        {
            Debug.Log("Balýk varken çarptý");        
            Destroy(gameObject);
        }
    }


}
