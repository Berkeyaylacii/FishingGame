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

    GameObject hook;
    GameObject hand;           //Main Menu icon
    GameObject upgrade1;       //Main Menu button
    GameObject fishingBagText; //Main Menu button
    GameObject baitCountText;
    public GameObject gameOverPanel;  

    public GameObject fishSpawner;

    [SerializeField] private TextMeshProUGUI tap_txt;
    [SerializeField] private TextMeshProUGUI baitCount_txt;

    [SerializeField] private GameObject hand_icon;
    [SerializeField] private GameObject score;

    GameObject fishingLine;
    
    Camera mainCamera;

    public bool moveCamera = false;
    public bool moveCameraToMenu = false;
    public bool isInMainMenu = true;
    //
    Vector3 startPos;
    Vector3 targetPos;
    //float desiredDuration = 1f;
    //float elapsedTime;
    float time;

    void Start()
    {
        mainCamera = Camera.main;

        //fishingLine = GameObject.FindGameObjectWithTag("MainCamera").GetComponent(FishingLine).ena;

        startPos = mainCamera.transform.position;
        targetPos = new Vector3(0, 0, -10);

        hook = GameObject.FindGameObjectWithTag("Hook");
        hand = GameObject.FindGameObjectWithTag("Hand");
        score = GameObject.FindGameObjectWithTag("Score");
        baitCountText = GameObject.FindGameObjectWithTag("BaitCount");
        //gameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
        
        //hook.GetComponent<SpawnFish>().enabled = false;

        hook.SetActive(false);
        score.SetActive(false);
        baitCountText.SetActive(false);
        gameOverPanel.SetActive(false);
        fishSpawner.SetActive(false);
        
        baitCount_txt.SetText("3");
         
        tap_txt.transform.DOScale(1.1f, 0.5f).SetLoops(10000, LoopType.Yoyo).SetEase(Ease.InOutFlash);                                                 //Text scale animation
        hand_icon.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-200f, -100f), 1f).SetLoops(100000, LoopType.Yoyo).SetEase(Ease.InOutFlash);   //Hand icon animation
    }

    private void Update()
    {   
        if (moveCamera != false)             //Moves camera smoothly to the game scene, and Game Starts !
        {
            //Debug.Log(moveCamera);
            time += Time.deltaTime * 0.6f;
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, time); 
            if(mainCamera.transform.position == new Vector3(0, 0, -10f))
            {
                moveCamera = false;
                time = 0;                   // Time set to 0 for Lerp to work correct way
            }
        }

        if (moveCameraToMenu != false)       //Moves camera smoothly when tapped to main menu (smooth movement not working)
        {
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
            }
        }

        if (isInMainMenu == true)
        {
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
        gameOverPanel.SetActive(false);
        mainCamera.GetComponent<LineRenderer>().enabled = false;
        moveCameraToMenu = true;       
    }     

}
