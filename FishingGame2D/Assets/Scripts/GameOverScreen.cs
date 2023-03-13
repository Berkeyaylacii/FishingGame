using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverScreen : MonoBehaviour
{
    public GameObject GameOverPanel;

    [SerializeField] public TextMeshProUGUI baitCount_txt;

    [SerializeField] public TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(baitCount_txt.text == "0")
        {
            GameOver();
            Time.timeScale = 0;
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
        Time.timeScale = 1;
        Debug.Log("Reklam izlendi ve 1 yem verildi");
    }
}
