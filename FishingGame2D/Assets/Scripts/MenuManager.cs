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

    GameObject hook;
    GameObject hand;           //Main Menu icon
    GameObject upgrade1;       //Main Menu button
    GameObject fishingBagText; //Main Menu button
    GameObject baitCountText;
    GameObject bubbles;

    public GameObject gameOverPanel;
    public GameObject settingsPanel;

    public GameObject fishSpawner;

    public GameObject gameScoreGroup;
    public GameObject totalScoreGroup;

    [SerializeField] private TextMeshProUGUI tap_txt;
    [SerializeField] private TextMeshProUGUI baitCount_txt;

    [SerializeField] private GameObject hand_icon;
    [SerializeField] private GameObject score;

    [SerializeField] private AudioSource mainMenuSound;
    [SerializeField] private AudioSource gameSceneSound;


    GameObject fishingLine;
    
    Camera mainCamera;

    public bool moveCamera = false;
    public bool moveCameraToMenu = false;
    public bool isInMainMenu = true;
    //
    Vector3 startPos;
    Vector3 targetPos;
    float time;

    void Start()
    {   
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
        settingsPanel.SetActive(false);
        fishSpawner.SetActive(false);
        bubbles.SetActive(false);

        gameScoreGroup.SetActive(false);
        totalScoreGroup.SetActive(true);


       baitCount_txt.SetText("3");
         
        tap_txt.transform.DOScale(2f, 0.7f).SetLoops(10000, LoopType.Yoyo).SetEase(Ease.InOutFlash);                                                 //Text scale animation
        hand_icon.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-200f, -100f), 1f).SetLoops(100000, LoopType.Yoyo).SetEase(Ease.InOutFlash);   //Hand icon animation
    }

    private void Update()
    {
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

                HookCollisions.increaseTotalScore();
                totalScoreGroup.SetActive(true);

                HookCollisions.resetPoint();    //Reset game score when returning to main menu           
            }
        }

        if (isInMainMenu == true)
        {
            //mainMenuSound.Play();
            bubbles.SetActive(false);
            //gameSceneSound.Play();               //Plays main menu sound
            gameScoreGroup.SetActive(false);
            totalScoreGroup.SetActive(true);
            if ( (FishMovement.rightSpawns != null) && (FishMovement.leftSpawns != null) )
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

        fishSpawner.SetActive(true);
    }

    public void UpgradeRoad1()
    {
        upgrade1.SetActive(false);
    }

    public void ReturnMainMenu()
    {
        if (gameSceneSound.isPlaying)
        {
            gameSceneSound.Stop();
        }
        
        gameOverPanel.SetActive(false);
        mainCamera.GetComponent<LineRenderer>().enabled = false;
        moveCameraToMenu = true;       
    }     

    public void openSettings()
    {
        settingsPanel.SetActive(true);
    }
}
