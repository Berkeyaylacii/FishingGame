using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ObstacleColliders : MonoBehaviour
{
    public GameObject hookedFish;
    public HookCollisions HookCollisions;

    [SerializeField] private AudioSource wormDropSound;
    public TextMeshProUGUI baitCount_txt;

    // Start is called before the first frame update
    public bool ifHookedFishesFell = false;
    void Start()
    {   
        if(HookCollisions != null)
        {
            HookCollisions = GameObject.FindGameObjectWithTag("Hook").GetComponent<HookCollisions>();
        }
        

        if(GameObject.Find("Canvas/BaitCount") != null)
        {
            baitCount_txt = GameObject.Find("Canvas/BaitCount/WormIcon/x/FishCapacity").GetComponent<TextMeshProUGUI>();
        }         
    }

    // Update is called once per frame
    void Update()
    {
        hookedFish = GameObject.FindGameObjectWithTag("HookedFish");

        if (hookedFish != null)
        {
            //hookedFish = GameObject.FindGameObjectWithTag("HookedFish");
            ifHookedFishesFell = true;
            float dist = Vector3.Distance(hookedFish.transform.position, this.transform.position);

            if ( dist <= 0.9f )
            {
                Debug.Log("çarptý");
                wormDropSound.Play();                  //worm drop sound
                Destroy(hookedFish);

                if(ifHookedFishesFell == true)
                {   
                    if(HookCollisions != null)
                    {
                        HookCollisions.fishCount -= 1 ;
                        Debug.Log("Toplam balýk: "+HookCollisions.fishCount);
                    }       
                    
                    ifHookedFishesFell = false;
                }

                float baitct = float.Parse(baitCount_txt.text);

                if (baitct > 0)
                {
                    baitct = baitct - 1;
                    Debug.Log("Balýk varken Yem düþtü -1");
                    baitCount_txt.text = baitct.ToString();
                }
            }
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "HookedFish")  //Olta yemlikyen balýða çarpýyor
        {
            if (GameObject.FindGameObjectWithTag("HookedFish") != null)
            {
                wormDropSound.Play();                  //worm drop sound
                Destroy(collision.gameObject);

                float baitct = float.Parse(baitCount_txt.text);

                if (baitct > 0)
                {
                    baitct = baitct - 1;
                    Debug.Log("Balýk varken Yem düþtü -1");
                    baitCount_txt.text = baitct.ToString();
                }
            }            
        }
    }*/

}
