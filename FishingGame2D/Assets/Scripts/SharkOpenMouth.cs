using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SharkOpenMouth : MonoBehaviour
{
    public GameOverScreen GameOverScreen;

    public GameObject hook;
    public GameObject shark;

    public SpriteRenderer image;

    public TextMeshProUGUI baitCount;

    public Sprite sharkOM;
    public Sprite sharkNormal;

    public float sharkDistancetoHook;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   

        if (GameObject.FindGameObjectWithTag("Hook") != null && GameObject.FindGameObjectWithTag("Shark") != null ) //Shark opens mouth
        {
            //hook = GameObject.FindGameObjectWithTag("Hook");
            shark = GameObject.FindGameObjectWithTag("Shark");
            image = shark.GetComponent<SpriteRenderer>();
            //GameObject closestHookedFish = null;
            //closestHookedFish = FindNearestHookedFish();
            sharkDistancetoHook = Vector3.Distance(shark.transform.position, hook.transform.position);
            if(sharkDistancetoHook < 1.5f)
            {
                image.sprite = sharkOM;
                if(sharkDistancetoHook < 0.7f)
                {
                    sharkAttack();
                }
            }
            else
            {
                image.sprite = sharkNormal;
            }
        }
    }

    void sharkAttack()
    {
        GameOverScreen.baitCount_txt.text = "0";
    }
   
}
