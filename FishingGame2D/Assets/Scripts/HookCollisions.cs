using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class HookCollisions : MonoBehaviour
{
    public GameObject fish;

    public Rigidbody2D rbofFish;

    public GameObject hook;

    public GameObject worm;

    public Collider2D colliderofHook;

    [SerializeField] public TextMeshProUGUI baitCount_txt;

    [SerializeField] public TextMeshProUGUI score_txt;

    [SerializeField] public TextMeshProUGUI total_score_txt;

    public bool ifHooked = false;
    public float reset = 0;
    void Start()
    {
        total_score_txt.text = PlayerPrefs.GetFloat("TotalScore").ToString();
        hook.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y >= 3.9f)     //if fish is moved to boat position, to remove the fish from the hook
        {
            takeFishToBoat();
        }          
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fish" && ifHooked == false && worm.activeSelf == true)  //Olta yemlikyen balýða çarpýyor
        {   
            ifHooked = true;

            //colliderofHook.enabled = false;                     //remove the collider of hook to catch fish only once
            worm.gameObject.SetActive(false);                    //disappear the worm

            collision.transform.gameObject.tag = "HookedFish";   //change the fish tag to understand if fish is catched

            if (collision.gameObject != null)
            {
                collision.transform.DOShakeRotation(5, Vector3.forward * 10, 10, 90, false).SetLoops(1, LoopType.Yoyo).OnComplete(delegate        //fish shake effect
                {
                    collision.transform.rotation = Quaternion.identity;
                });
            }

            collision.transform.SetParent(GameObject.FindGameObjectWithTag("Hanger").transform, true);         //fish become parent of hook
     
            if (collision.transform.rotation.eulerAngles.y == 180)                    //
            {
                collision.transform.position = GameObject.FindGameObjectWithTag("Hanger").transform.position + new Vector3(-0.3f, 0, 0);
            }      
            else if (collision.transform.rotation.eulerAngles.y == 0)
            {
                collision.transform.position = GameObject.FindGameObjectWithTag("Hanger").transform.position + new Vector3(0.3f, 0, 0);
            }
        }

        if (collision.gameObject.tag == "Obstacle" && ifHooked == false && worm.activeSelf == true)   // Olta yemliyken objeye çarpýyor
        {   
            colliderofHook.enabled = false;
            worm.gameObject.SetActive(false);
            
            float baitct = float.Parse(baitCount_txt.text);
            if (baitct > 0 )
            {
                baitct = baitct - 1;
                Debug.Log("Objeye çarptý yem düþtü -1");
                baitCount_txt.text = baitct.ToString();
            }
        }

        /*if (collision.gameObject.tag == "Obstacle" && ifHooked == true && worm.activeSelf == false)         //Oltada balýk varken objeye çarpýyor
        {
            if (GameObject.FindGameObjectWithTag("HookedFish") != null)
            {
                Destroy(GameObject.FindGameObjectWithTag("HookedFish"));

                float baitct = float.Parse(baitCount_txt.text);
                    if (baitct > 0)
                    {
                        baitct = baitct - 1;
                        Debug.Log("Balýk varken Yem düþtü -1");
                        baitCount_txt.text = baitct.ToString();
                        ifHooked = false;
                    }
            }
        }*/

    }

    private void takeFishToBoat()
    {
        if (GameObject.FindGameObjectWithTag("HookedFish") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("HookedFish"));
            increasePoint();
        }     
        colliderofHook.enabled = true;
        worm.gameObject.SetActive(true); 
        ifHooked = false;
    }

    private void increasePoint()
    {
        float skor = float.Parse(score_txt.text);
        skor = skor + 1;
        score_txt.text = skor.ToString();
    }

    public void resetPoint()
    {
        score_txt.text = reset.ToString();
    }

    public void increaseTotalScore()
    {
        float totalScore = float.Parse(total_score_txt.text);
        float score = float.Parse(score_txt.text);
        totalScore += score;

        total_score_txt.text = totalScore.ToString();
        PlayerPrefs.SetFloat("TotalScore", totalScore);
    }
}
