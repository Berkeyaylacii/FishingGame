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
    

    [SerializeField] private TextMeshProUGUI tap_txt;
    [SerializeField] private TextMeshProUGUI baitCount_txt;
    [SerializeField] private TextMeshProUGUI boatCapacity_txt;
    [SerializeField] private TextMeshProUGUI currentCapacity;
    [SerializeField] private TextMeshProUGUI increaseCapacityCostText;

    [SerializeField] private TextMeshProUGUI gameScoreAtPanel;

    [SerializeField] private TextMeshProUGUI fishLimit;
    [SerializeField] private TextMeshProUGUI increaseFishLimitCost;


    [SerializeField] private GameObject hand_icon;
    [SerializeField] private GameObject score;

    [SerializeField] private AudioSource mainMenuSound;
    [SerializeField] private AudioSource gameSceneSound;


    GameObject fishingLine;
    
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
    float time;
    float boatCap;
    void Start()
    {        
        mainCamera = Camera.main;

        startPos = mainCamera.transform.position;
        targetPos = new Vector3(0, 1.15f, -10);

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
        totalScoreGroup.SetActive(true);
        
        baitCount_txt.SetText("3");
        //boatCapacity_txt.SetText((UpgradeController.boatCapacity).ToString());
         
        tap_txt.transform.DOScale(2f, 0.7f).SetLoops(10000, LoopType.Yoyo).SetEase(Ease.InOutFlash);                                                 //Text scale animation
        hand_icon.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-200f, -100f), 1f).SetLoops(100000, LoopType.Yoyo).SetEase(Ease.InOutFlash);   //Hand icon animation
    }

    private void Update()
    {
        boatCapacity_txt.SetText((UpgradeController.boatCapacity).ToString());
        currentCapacity.SetText(PlayerPrefs.GetFloat("BoatCapacity").ToString());
        increaseCapacityCostText.SetText(PlayerPrefs.GetFloat("IncreaseCapacityCost").ToString());
        fishLimit.SetText(PlayerPrefs.GetFloat("FishLimit").ToString());
        increaseFishLimitCost.SetText(PlayerPrefs.GetFloat("IncreaseFishLimitCost").ToString());

        if (moveCamera != false)             //Moves camera smoothly Game Scene, and Game Starts !
        {
            //Debug.Log(moveCamera);           
            totalScoreGroup.SetActive(false);
            time += Time.deltaTime * 0.6f;
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, time); 
            if(mainCamera.transform.position == new Vector3(0, 1.15f, -10f))
            {                
                isInGame = true;    //if game is on 
                gameScoreGroup.SetActive(true);
                moveCamera = false;
                time = 0;                    // Time set to 0 for Lerp to work correct way
            }
            
        }

        if (moveCameraToMenu != false)       //Moves camera smoothly Game Scene -> Main Menu
        {   
            CollectScreen.isCapacityFull = false; // restart the capacity

            gameScoreGroup.SetActive(false); 
            time += Time.deltaTime * 0.6f;
            mainCamera.transform.position = Vector3.Lerp(targetPos, new Vector3(0, 8.5f, -10f), time);  

            if (mainCamera.transform.position == new Vector3(0, 8.5f, -10f))
            {              
                moveCameraToMenu = false;
                time = 0;                 //Time set to 0 for Lerp to work correct way
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
        baitCount_txt.SetText("3");
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
        gameScoreAtPanel.text = HookCollisions.score_txt.text;

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
