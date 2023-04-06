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
    public AdManager AdManager;
    public UpgradeController UpgradeController;

    GameObject hook;
    GameObject hand;           //Main Menu icon
    GameObject fishingBagText; //Main Menu button
    GameObject baitCountText;
    GameObject bubbles;

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
    //
    Vector3 startPos;
    Vector3 targetPos;
    float time;
    float boatCap;
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        mainCamera = Camera.main;

        startPos = mainCamera.transform.position;
        targetPos = new Vector3(0, 1.15f, -10);

        hook = GameObject.FindGameObjectWithTag("Hook");
        hand = GameObject.FindGameObjectWithTag("Hand");
        score = GameObject.FindGameObjectWithTag("Score");
        baitCountText = GameObject.FindGameObjectWithTag("BaitCount");
        bubbles = GameObject.FindGameObjectWithTag("Bubbles");       

        hook.SetActive(false);
        score.SetActive(false);
        baitCountText.SetActive(false);
        gameOverPanel.SetActive(false);
        collectPanel.SetActive(false);
        settingsPanel.SetActive(false);
        fishSpawner.SetActive(false);
        bubbles.SetActive(false);
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
        if (moveCamera != false)             //Moves camera smoothly to the game scene, and Game Starts !
        {
            //Debug.Log(moveCamera);
            totalScoreGroup.SetActive(false);
            bubbles.SetActive(true);
            time += Time.deltaTime * 0.6f;
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, time); 
            if(mainCamera.transform.position == new Vector3(0, 1.15f, -10f))
            {
                gameScoreGroup.SetActive(true);
                moveCamera = false;
                time = 0;                    // Time set to 0 for Lerp to work correct way
            }
            
        }

        if (moveCameraToMenu != false)       //Moves camera smoothly when tapped to main menu (smooth movement not working) Game Scene -> Main Menu
        {   
            bubbles.SetActive(false);
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
                //Upgrade2Panel.SetActive(true);  //Activate upgrade2

                HookCollisions.resetPoint();    //Reset game score when returning to main menu           
            }
        }

        if (isInMainMenu == true)
        { 
            AdManager.addWatchButton.interactable = true;   //to reset the add reward each game

            collectPanel.SetActive(false);   //when in mainmenu collectpanel closes
            //mainMenuSound.Play();
            bubbles.SetActive(false);
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

                foreach (GameObject obj in FishMovement.leftSpawns)
                {
                    if (obj != null)
                    {
                        Destroy(obj);
                    }
                }
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
        //Upgrade2Panel.SetActive(false);

        fishSpawner.SetActive(true);
    }


    public void ReturnMainMenu()         //returns to main menu with when clicked, also Score increases! 
    {
        if (gameSceneSound.isPlaying)
        {
            gameSceneSound.Stop();
        }

        collectPanel.SetActive(false);  
        gameOverPanel.SetActive(false);
        

        mainCamera.GetComponent<LineRenderer>().enabled = false;
        ifCollect = true;
        moveCameraToMenu = true;       
    }     

    public void ReturnMainMenuWithout()
    {
        if (gameSceneSound.isPlaying)
        {
            gameSceneSound.Stop();
        }

        collectPanel.SetActive(false);
        gameOverPanel.SetActive(false);

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

        collectPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        mainCamera.GetComponent<LineRenderer>().enabled = false;
        ifCollect = true;
        ifCollect2x = true;
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
