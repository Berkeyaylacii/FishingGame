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

    public ParticleSystem bubbleParticle;

    public Collider2D colliderofHook;

    [SerializeField] public TextMeshProUGUI baitCount_txt;

    [SerializeField] public TextMeshProUGUI score_txt;   
    [SerializeField] public TextMeshProUGUI scoreatPanel;  //To use in collect screen menu
    [SerializeField] public TextMeshProUGUI multiplier_txt; //To show in ame screen menu

    [SerializeField] public TextMeshProUGUI total_score_txt;

    [SerializeField] public AudioSource fishCatchSound;
    [SerializeField] public AudioSource wormDropSound;

    public GameObject FishV1Group;
    public GameObject FishV2Group;
    public GameObject FishV3Group;

    public ParticleSystem particle;

    public bool ifHooked = false;
    public float reset = 0;
    public float fishCount = 0;

    public float totalFishV1Count = 0;
    public float totalFishV2Count = 0;
    public float totalFishV3Count = 0;

    public GameObject[] Fishes;

    void Start()
    {
        total_score_txt.text = (PlayerPrefs.GetFloat("TotalScore").ToString()).Insert(0,"$");  //$ ICON ADDED HERE
        hook.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y >= 3.9f)     //if fish is moved to boat position, to remove the fish from the hook
        {
            takeFishToBoat();
        }

        if(MenuManager.isInGame == true && ifHooked == false && worm.activeSelf == true && fishCount < UpgradeController.fishLimit)  //Fish eats bait
        {
            fishEatsBait();
        }
    }

    private void takeFishToBoat()
    {
        if (GameObject.FindGameObjectWithTag("HookedFish") != null)
        {
            if (GameObject.FindGameObjectWithTag("HookedFish").name == "TestFish(Clone)")  //Fishv1 count at collect menu
            {
                totalFishV1Count += 1;
            }

            if (GameObject.FindGameObjectWithTag("HookedFish").name == "FishV2(Clone)")  //Fishv2 count at collect menu
            {
                totalFishV2Count += 1;
            }

            if(GameObject.FindGameObjectWithTag("HookedFish").name =="Fishv3(Clone)")
            {
                totalFishV3Count += 1;
            }

            Destroy(GameObject.FindGameObjectWithTag("HookedFish"));
            increasePoint();
        }     
        //colliderofHook.enabled = true;
        worm.gameObject.SetActive(true); 
        ifHooked = false;
        ObstacleColliders.decreseaBaitOnce = false;
        fishCount = 0;             //reset the fish count at hook
    }

    private void increasePoint()  //increases gameplay score
    {
        float skor = float.Parse(score_txt.text);
        skor = skor + 1;
        score_txt.text = skor.ToString();
    }

    public void resetPoint()
    {
        score_txt.text = reset.ToString();
        multiplier_txt.text = "1";       
    }

    public void increaseTotalScore()
    {   
        if(UpgradeController.isScoreIncreasing == false)
        {   
            UpgradeController.isScoreIncreasing = true;

            float totalScore =  float.Parse( (total_score_txt.text.ToString()).Remove(0,1) );  //$ ICON REMOVED HERE
            //Debug.Log(MenuManager.multiplierEarned+" önce");
            float score = float.Parse(scoreatPanel.text.Remove(0,1));   //$ icon removed from score
            totalScore += score;

            particle.Play();
            UpgradeController.AddValue(score);   //increaseAnimation

            resetFishSpeciesCount(); //reset fish species count
            score = 0;    //reset
            scoreatPanel.text = score.ToString();  //reset           
            //total_score_txt.text = totalScore.ToString();

            PlayerPrefs.SetFloat("TotalScore", totalScore);

            MenuManager.collectPanel.SetActive(false);  //close the collect panel
            MenuManager.multiplierEarned = 1;
            //Debug.Log("sonra: " + MenuManager.multiplierEarned);

            FishV1Group.SetActive(false);
            FishV2Group.SetActive(false);
            FishV3Group.SetActive(false);
        }
    }

    public void increaseTotalScore2x()
    {
        if (UpgradeController.isScoreIncreasing == false)
        {
            UpgradeController.isScoreIncreasing = true;

            float totalScore = float.Parse( (total_score_txt.text.ToString()).Remove(0,1) );  //$ ICON REMOVED HERE
            float score = float.Parse(scoreatPanel.text.Remove(0,1));  // $ icon removed from score
            totalScore += 2 * score;

            particle.Play();
            UpgradeController.AddValue(2 * score);   //increase animation

            resetFishSpeciesCount(); //reset fishs species count
            score = 0;
            scoreatPanel.text = score.ToString();
            //total_score_txt.text = totalScore.ToString();

            PlayerPrefs.SetFloat("TotalScore", totalScore);

            MenuManager.collectPanel.SetActive(false);  //close the collectpanel
            MenuManager.multiplierEarned = 1;
            //Debug.Log("sonra: " + MenuManager.multiplierEarned);

            FishV1Group.SetActive(false);  
            FishV2Group.SetActive(false);
            FishV3Group.SetActive(false);

            //UpgradeController.isScoreIncreasing = false;
        }
    }


    public void resetFishSpeciesCount()  //To reset fish species count
    {   
        totalFishV1Count = 0;
        totalFishV2Count = 0;
        totalFishV3Count = 0;
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
                fis.GetComponent<ParticleSystem>().Clear();
                fis.GetComponent<ParticleSystem>().enableEmission = false; //Deactivate the bubble particles

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

    public string remove1stSymbolfromString(string a)
    {
        a.Remove(0, 1);
        return a;
    }

    public string addSymboltoString(string a)
    {
        string sym = "$";
        a.Insert(0, sym);

        return a;
    }
}
