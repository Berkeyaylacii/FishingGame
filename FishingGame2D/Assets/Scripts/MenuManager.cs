using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public FishMovement FishMovement;
    public HookCollisions HookCollisions;
    public CollectScreen CollectScreen;
    public BannerAdManager BannerAdManager;
    public UpgradeController UpgradeController;
    public GecisAdManager GecisAdManager;
    public MultiplierManager MultiplierManager;

    GameObject hook;
    GameObject hand;           //Main Menu icon
    GameObject baitCountText;
    public ParticleSystem bubbleParticle;

    public GameObject gameOverPanel;
    public GameObject collectPanel;
    public GameObject settingsPanel;
    public GameObject Upgrade1Panel;
    public GameObject Upgrade2Panel;
    public GameObject howToPlayPanel;

    public GameObject fishSpawner;

    public GameObject gameScoreGroup;
    public GameObject totalScoreGroup;
    public GameObject multiplierGroup;
    

    [SerializeField] private TextMeshProUGUI tap_txt;
    [SerializeField] private TextMeshProUGUI baitCount_txt;
    [SerializeField] private TextMeshProUGUI boatCapacity_txt;
    [SerializeField] private TextMeshProUGUI currentCapacity;
    [SerializeField] private TextMeshProUGUI increaseCapacityCostText;  //Upgrade 1 Panel Price Text

    [SerializeField] private TextMeshProUGUI gameScoreAtPanel;    //These are on collect screen
    [SerializeField] private TextMeshProUGUI multiplierScoreAtPanel;  //These are on collect screen

    [SerializeField] private TextMeshProUGUI fishesV1TextAtPanel;  //These are on collect screen
    [SerializeField] private TextMeshProUGUI fishesV2TextAtPanel;  //These are on collect screen
    [SerializeField] private TextMeshProUGUI fishesV3TextAtPanel;  //These are on collect screen
    [SerializeField] private TextMeshProUGUI fishesV4TextAtPanel;  //These are on collect screen
    [SerializeField] private TextMeshProUGUI fishesV5TextAtPanel;  //These are on collect screen
    [SerializeField] private TextMeshProUGUI fishesV6TextAtPanel;  //These are on collect screen

    public GameObject fishV1Group;
    public GameObject fishV2Group;
    public GameObject fishV3Group;
    public GameObject fishV4Group;
    public GameObject fishV5Group;
    public GameObject fishV6Group;

    public Button collectButton;
    public Button collect2xButton;

    [SerializeField] private TextMeshProUGUI fishLimit;
    [SerializeField] private TextMeshProUGUI increaseFishLimitCost;  //Upgrade 2 Panel Price Text


    [SerializeField] private GameObject hand_icon;
    [SerializeField] private GameObject score;

    [SerializeField] private AudioSource mainMenuSound;
    [SerializeField] private AudioSource gameSceneSound;
    
    Camera mainCamera;

    public bool moveCamera = false;
    public bool moveCameraToMenu = false;
    public bool isInMainMenu = true;
    public bool ifCollect = false;
    public bool ifCollect2x = false;
    public bool isInGame = false;
    //
    Vector3 startPos;
    Vector3 targetPos;

    public int timesPlayed = 0;
    float timee;

    public float multiplierEarned = 1;

    public bool showFishv1onMenu= false;
    public bool showFishv2onMenu = false;
    public bool showFishv3onMenu = false;

    public bool showingFishAnimClosed = false;
    void Start()
    {
        mainCamera = Camera.main;

        startPos = mainCamera.transform.position;
        targetPos = new Vector3(0, 1.15f, -10f); // 0, 1.15f, -10 eskiiiiiiii

        hook = GameObject.FindGameObjectWithTag("Hook");
        hand = GameObject.FindGameObjectWithTag("Hand");
        score = GameObject.FindGameObjectWithTag("Score");
        baitCountText = GameObject.FindGameObjectWithTag("BaitCount");    

        hook.SetActive(false);
        score.SetActive(false);
        baitCountText.SetActive(false);
        gameOverPanel.SetActive(false);
        collectPanel.SetActive(false);
        settingsPanel.SetActive(false);
        fishSpawner.SetActive(false);
        howToPlayPanel.SetActive(false);

        gameScoreGroup.SetActive(false);
        multiplierGroup.SetActive(false);
        totalScoreGroup.SetActive(true);
        
        baitCount_txt.SetText("1");  //3 ÝDÝ
        //boatCapacity_txt.SetText((UpgradeController.boatCapacity).ToString());
         
        tap_txt.transform.DOScale(2f, 0.7f).SetLoops(10000, LoopType.Yoyo).SetEase(Ease.InOutFlash);                                                 //Text scale animation
        hand_icon.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-200f, -100f), 1f).SetLoops(100000, LoopType.Yoyo).SetEase(Ease.InOutFlash);   //Hand icon animation      
    }

    private void Update()
    {
        boatCapacity_txt.SetText((UpgradeController.boatCapacity).ToString());
        currentCapacity.SetText(PlayerPrefs.GetFloat("BoatCapacity").ToString());
        increaseCapacityCostText.SetText( (PlayerPrefs.GetFloat("IncreaseCapacityCost").ToString()).Insert(0,"$") );  // $ ICON ADDED TO INCREASE CAPACITY COST TEXT
        fishLimit.SetText(PlayerPrefs.GetFloat("FishLimit").ToString());
        increaseFishLimitCost.SetText( (PlayerPrefs.GetFloat("IncreaseFishLimitCost").ToString()).Insert(0, "$"));    // $ ICON ADDED TO INCREASE FISH LIMIT COST TEXT

        if (moveCamera != false)             //Moves camera smoothly Game Scene, and Game Starts !
        {
            //Debug.Log(moveCamera);           
            totalScoreGroup.SetActive(false);
            timee += Time.deltaTime * 0.6f;
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, timee);

            if(mainCamera.transform.position == new Vector3(0, 1.15f, -10f))  
            {                
                isInGame = true;    //if game is on 
                gameScoreGroup.SetActive(true);
                multiplierGroup.SetActive(true);
                moveCamera = false;
                timee = 0;                    
            }           
        }

        if (moveCameraToMenu != false)       //Moves camera smoothly Game Scene -> Main Menu
        {   
            CollectScreen.isCapacityFull = false; // restart the capacity
         
            gameScoreGroup.SetActive(false);
            multiplierGroup.SetActive(false);
            timee += Time.deltaTime * 0.6f;
            mainCamera.transform.position = Vector3.Lerp(targetPos, new Vector3(0, 8.5f, -10f), timee);  

            if (mainCamera.transform.position == new Vector3(0, 8.5f, -10f))
            {

                if(HookCollisions.totalFishV1Count > 0)        //Activate the collect screen fish anims
                {
                    fishV1Group.SetActive(true);
                }
                if (HookCollisions.totalFishV2Count > 0)
                {
                    fishV2Group.SetActive(true);
                }
                if (HookCollisions.totalFishV3Count > 0)
                {
                    fishV3Group.SetActive(true);
                }
                if (HookCollisions.totalFishV4Count > 0)
                {
                    fishV4Group.SetActive(true);
                }
                if (HookCollisions.totalFishV5Count > 0)
                {
                    fishV5Group.SetActive(true);
                }
                if (HookCollisions.totalFishV6Count > 0)
                {
                    fishV6Group.SetActive(true);
                }


                moveCameraToMenu = false;
                timee = 0;                 //Time set to 0 for Lerp to work correct way
                isInMainMenu = true;      //flag to use hide gameover panel on another script                

                hook.SetActive(false);    //Deactive all necessery object to return the Main Menu
                hand.SetActive(true);
                score.SetActive(true);
                baitCountText.SetActive(false);
                fishSpawner.SetActive(false);

                if(ifCollect == true)
                {   
                    if(ifCollect2x == true)
                    {
                        Debug.Log("puan 2x verildi.");
                        HookCollisions.increaseTotalScore2x();
                        
                    }
                    else if(ifCollect2x == false)
                    {
                        Debug.Log("puan normal verildi.");
                        HookCollisions.increaseTotalScore();
                    }
                    
                    
                    ifCollect = false;
                    ifCollect2x = false;
                }
                
                totalScoreGroup.SetActive(true);

                Upgrade1Panel.SetActive(true);  //Activate upgrade1
                Upgrade2Panel.SetActive(true);  //Activate upgrade2

                HookCollisions.resetPoint();    //Reset game score when returning to main menu           
            }
        }

        if (isInMainMenu == true)
        {
            //collectPanel.SetActive(false);   //when in mainmenu collectpanel closes
            //mainMenuSound.Play();
            //gameSceneSound.Play();               //Plays main menu sound
            gameScoreGroup.SetActive(false);
            multiplierGroup.SetActive(false);
            totalScoreGroup.SetActive(true);

            if ( (FishMovement.rightSpawns != null) && (FishMovement.leftSpawns != null) )   //Deletes gameScene object
             {
                foreach (GameObject obj in FishMovement.rightSpawns)
                {
                    if(obj != null)
                    {
                        Destroy(obj);
                    }
                }
                FishMovement.rightSpawns.Clear();
                foreach (GameObject obj in FishMovement.leftSpawns)
                {
                    if (obj != null)
                    {
                        Destroy(obj);
                    }
                }
                FishMovement.leftSpawns.Clear();

                MultiplierManager.SharksPassed.Clear();
                MultiplierManager.ObstaclesPassed.Clear();
                //Clean also the arrays.
            }
        }
    }

    public void LoadGamee()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MoveCamera()     //Moves camera to play screen, game starts !
    {   
        isInMainMenu = false;
        moveCamera = true; // flag to move camera on update 

        mainCamera.GetComponent<LineRenderer>().enabled = true ;     //Active all necessery object to start the game

        gameSceneSound.Play();
        baitCount_txt.SetText("1");
        hook.SetActive(true);
        hand.SetActive(false);
        score.SetActive(true);
        baitCountText.SetActive(true);
        Upgrade1Panel.SetActive(false);
        Upgrade2Panel.SetActive(false);

        bubbleParticle.Play();  //Play buble particle once

        fishSpawner.SetActive(true);
    }


    public void ReturnMainMenu()         //returns to main menu with when clicked, also Score increases! 
    {
        if (gameSceneSound.isPlaying)
        {
            gameSceneSound.Stop();
        }

        collectPanel.SetActive(true);  
        gameOverPanel.SetActive(false);

        multiplierEarned = float.Parse(HookCollisions.multiplier_txt.text);
        gameScoreAtPanel.text =  Mathf.Round(float.Parse(HookCollisions.score_txt.text) * multiplierEarned).ToString().Insert(0,"$");            //These are on collect menu, number rounded
        //multiplierScoreAtPanel.text = HookCollisions.multiplier_txt.text; //These are on collect menu
        
        fishesV1TextAtPanel.text = "x" + HookCollisions.totalFishV1Count;      //These are on collect menu
        fishesV2TextAtPanel.text = "x" + HookCollisions.totalFishV2Count;
        fishesV3TextAtPanel.text = "x" + HookCollisions.totalFishV3Count;
        fishesV4TextAtPanel.text = "x" + HookCollisions.totalFishV4Count;
        fishesV5TextAtPanel.text = "x" + HookCollisions.totalFishV5Count;
        fishesV6TextAtPanel.text = "x" + HookCollisions.totalFishV6Count;

        timesPlayed++;

        if(timesPlayed % 4 == 0)
        {
            GecisAdManager.ShowAd();
        }
        
        mainCamera.GetComponent<LineRenderer>().enabled = false;
        moveCameraToMenu = true;       
    }     

    public void ReturnMainMenuWithout()
    {
        if (gameSceneSound.isPlaying)
        {
            gameSceneSound.Stop();
        }

        HookCollisions.resetFishSpeciesCount();
        //collectPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        timesPlayed++;
        if (timesPlayed % 4 == 0)
        {
            GecisAdManager.ShowAd();
        }

        mainCamera.GetComponent<LineRenderer>().enabled = false;
        ifCollect = false;
        ifCollect2x = false;
        moveCameraToMenu = true;
    }

    public void ReturnMainMenu2xCollect()
    {
        if (gameSceneSound.isPlaying)
        {
            gameSceneSound.Stop();
        }

        //collectPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        timesPlayed++;

        mainCamera.GetComponent<LineRenderer>().enabled = false;

        //ifCollect = true;
        //ifCollect2x = true;
        moveCameraToMenu = true;
    }

    public void openSettings()
    {   
        settingsPanel.SetActive(true);
        Time.timeScale = 0;
        fishSpawner.SetActive(false);
    }

    public void closeSettingsPanel()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1;
        fishSpawner.SetActive(true);
    }

    public void openHowToPlay()
    {
        howToPlayPanel.SetActive(true);
        Time.timeScale = 0;
        fishSpawner.SetActive(false);
    }

    public void closeHowTopPlayPanel()
    {
        howToPlayPanel.SetActive(false);
        Time.timeScale = 1;
        fishSpawner.SetActive(true);
    }

}
