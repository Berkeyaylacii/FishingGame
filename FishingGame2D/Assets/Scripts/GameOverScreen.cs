using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverScreen : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject FishManager;
    public GameObject hook;
    public MenuManager menuManager;
    public ObstacleColliders ObstacleColliders;

    [SerializeField] public TextMeshProUGUI baitCount_txt;

    [SerializeField] public TextMeshProUGUI score;

    public Camera mainCamera;

    
    void Start()
    {
        //isCapacityFull = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isCapacityFull);
        if(baitCount_txt.text == "0" )
        {   
            FishManager.SetActive(false);
            hook.SetActive(false);
            if(menuManager.isInMainMenu == false)
            {
                GameOverPanel.SetActive(true);
                menuManager.isInGame = false;

}
            else if(menuManager.isInMainMenu == true)
            {
                GameOverPanel.SetActive(false);
            }                            
        }
    }

    public void RestartGame()  //If ad watched, give 1 more bait to play
    {
            Debug.Log("reklam izlendi 1 kere ve ödül verildi.");
            baitCount_txt.text = "1";

            GameOverPanel.SetActive(false);
            FishManager.SetActive(true);
            hook.SetActive(true);
            menuManager.isInGame = true;

    }

}
