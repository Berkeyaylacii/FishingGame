using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverScreen : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject FishManager;

    public MenuManager menuManager;

    [SerializeField] public TextMeshProUGUI baitCount_txt;

    [SerializeField] public TextMeshProUGUI score;

    public Camera mainCamera;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(baitCount_txt.text == "0")
        {
            if(menuManager.isInMainMenu == false)
            {   
                GameOver();
                
            }else if(menuManager.isInMainMenu == true)
            {
                GameOverPanel.SetActive(false);
            }
                 

            FishManager.SetActive(false);          
        }
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        baitCount_txt.text = "1";

        GameOverPanel.SetActive(false);
        FishManager.SetActive(true);
        Debug.Log("Reklam izlendi ve 1 yem verildi");
    }
}
