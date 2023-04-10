using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ObstacleColliders : MonoBehaviour
{
    public GameObject hookedFish;
    public HookCollisions HookCollisions;
    public MenuManager MenuManager;
    public UpgradeController UpgradeController;

    public GameObject Worm;

    [SerializeField] private AudioSource wormDropSound;
    public TextMeshProUGUI baitCount_txt;

    private bool decreseaBaitOnce = false;
    void Start()
    {
        MenuManager = GameObject.Find("Canvas").GetComponent<MenuManager>();
        UpgradeController = GameObject.FindGameObjectWithTag("UpgradeController").GetComponent<UpgradeController>();
        Worm = GameObject.FindGameObjectWithTag("Worm");

        if (GameObject.Find("Canvas/BaitCount") != null)
        {
            baitCount_txt = GameObject.Find("Canvas/BaitCount/WormIcon/x/FishCapacity").GetComponent<TextMeshProUGUI>();
        }         
    }

    // Update is called once per frame
    void Update()
    {      
        if (MenuManager.isInGame == true)
        {    
        HookCollisions = GameObject.FindGameObjectWithTag("Hook").GetComponent<HookCollisions>();
        hookedFish = GameObject.FindGameObjectWithTag("HookedFish");
        if(hookedFish != null) { 
        if (/*HookCollisions.ifHooked == true*/  GameObject.FindGameObjectWithTag("Hook").activeSelf == true )
        {      
            float dist = Vector3.Distance(hookedFish.transform.position, this.transform.position);
            if (dist <= 0.9f)    //If hookedfish hits to obstacle
            {
                wormDropSound.Play();                  //worm drop sound
                float baitct = float.Parse(baitCount_txt.text);
         
                if (baitct > 0)
                {   
                    
                    /*Debug.Log("tek");
                    baitct = baitct - 1;
                    baitCount_txt.text = baitct.ToString();
                    HookCollisions.fishCount = 0;      //Set total hookedfish to 0                           
                    HookCollisions.ifHooked = false;
                    Destroy(hookedFish);*/

                        if (UpgradeController.multipleCatchisOn == false)
                        {
                             Debug.Log("tek");
                             baitct = baitct - 1;
                             baitCount_txt.text = baitct.ToString();
                             Worm.SetActive(false);
                             HookCollisions.fishCount = 0;      //Set total hookedfish to 0                           
                             HookCollisions.ifHooked = false;
                             Destroy(hookedFish);
                        }
                        else if (UpgradeController.multipleCatchisOn == true && decreseaBaitOnce == false)
                        {
                             Debug.Log("multiple");
                             baitct = baitct - 1;
                             baitCount_txt.text = baitct.ToString();
                             Worm.SetActive(false);
                             HookCollisions.fishCount = 0;      //Set total hookedfish to 0                           
                             HookCollisions.ifHooked = false;

                            GameObject[] hookedFishes = GameObject.FindGameObjectsWithTag("HookedFish");
                            foreach (GameObject fish in hookedFishes)
                                Destroy(fish);
                            decreseaBaitOnce = true;
                        }

                    }
            }

        }

        }}

    }



}
