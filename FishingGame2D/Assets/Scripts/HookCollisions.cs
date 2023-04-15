using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class HookCollisions : MonoBehaviour
{
    public ObstacleColliders ObstacleColliders;
    public UpgradeController UpgradeController;
    public MenuManager MenuManager;

    public GameObject fish;
    public GameObject fishMouth;

    public Rigidbody2D rbofFish;

    public GameObject hook;
    public GameObject hookPoint;

    public GameObject worm;

    public Collider2D colliderofHook;

    [SerializeField] public TextMeshProUGUI baitCount_txt;

    [SerializeField] public TextMeshProUGUI score_txt;

    [SerializeField] public TextMeshProUGUI total_score_txt;

    [SerializeField] public AudioSource fishCatchSound;
    [SerializeField] public AudioSource wormDropSound;

    public bool ifHooked = false;
    public float reset = 0;
    public float fishCount = 0;

    public GameObject[] Fishes;
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

        if(MenuManager.isInGame == true && ifHooked == false && worm.activeSelf == true && fishCount < UpgradeController.fishLimit)
        {
            fishEatsBait();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        
        if (collision.gameObject.tag == "Fish"  && ifHooked == false && worm.activeSelf == true)  //Olta yemlikyen balýk yakalanýyor
        {
            fishCatchSound.Play();

            if(UpgradeController.multipleCatchisOn == false)
            {
                ifHooked = true;
            }
           
            
            fishCount++;
            //Debug.Log("toplam balýk: " + fishCount);

            //colliderofHook.enabled = false;                                  //remove the collider of hook to catch fish only once
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            collision.gameObject.GetComponent<HingeJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
            collision.gameObject.GetComponent<Rigidbody2D>().mass = 20f;
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

            if (UpgradeController.multipleCatchisOn == false)
            {
                worm.gameObject.SetActive(false);     //disappear the worm
            }
                              
            collision.transform.gameObject.tag = "HookedFish";   //change the fish tag to understand if fish is catched

            if (collision.gameObject != null)
            {

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

        /*if (collision.gameObject.tag == "Obstacle" && ifHooked == false && worm.activeSelf == true)   // Olta yemliyken objeye çarpýyor
        {
            wormDropSound.Play();             //worm hits obstacle
            colliderofHook.enabled = false;
            worm.gameObject.SetActive(false);
            
            float baitct = float.Parse(baitCount_txt.text);
            if (baitct > 0 )
            {
                baitct = baitct - 1;
                Debug.Log("Bait -1");
                baitCount_txt.text = baitct.ToString();
            }
        }

        if (collision.gameObject.tag == "Obstacle" && ifHooked == true && worm.activeSelf == false)         //Oltada balýk varken objeye çarpýyor
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
        ObstacleColliders.decreseaBaitOnce = false;
        fishCount = 0;             //reset the fish count at hook
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

    public void increaseTotalScore2x()
    {
        float totalScore = float.Parse(total_score_txt.text);
        float score = float.Parse(score_txt.text);
        totalScore += 2*score;

        total_score_txt.text = totalScore.ToString();
        PlayerPrefs.SetFloat("TotalScore", totalScore);
    }

    public void fishEatsBait()
    {
        Fishes = GameObject.FindGameObjectsWithTag("Fish");
        foreach (GameObject fis in Fishes)
        {
            float distance = Vector3.Distance(fis.transform.position, this.transform.position);
            if(distance <= 0.5f)
            {

                fishCatchSound.Play();

                fis.transform.gameObject.tag = "HookedFish";
                if (UpgradeController.multipleCatchisOn == false)
                {
                    ifHooked = true;
                }

                fishCount++;
                //Debug.Log("toplam balýk: " + fishCount);

                fis.gameObject.GetComponent<HingeJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
                fis.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
                fis.gameObject.GetComponent<Rigidbody2D>().mass = 20f;
                fis.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

                if (UpgradeController.multipleCatchisOn == false)
                {
                    worm.gameObject.SetActive(false);     //disappear the worm
                }

                fis.transform.gameObject.tag = "HookedFish";   //change the fish tag to understand if fish is catched

                fis.transform.SetParent(GameObject.FindGameObjectWithTag("Hanger").transform, true);         //fish become parent of hook

                if (fis.transform.rotation.eulerAngles.y == 180)                    //
                {
                    fis.transform.position = GameObject.FindGameObjectWithTag("Hanger").transform.position + new Vector3(-0.3f, 0, 0);
                }
                else if (fis.transform.rotation.eulerAngles.y == 0)
                {
                    fis.transform.position = GameObject.FindGameObjectWithTag("Hanger").transform.position + new Vector3(0.3f, 0, 0);
                }
            }
        }
    }

}
