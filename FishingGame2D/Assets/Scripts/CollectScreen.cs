using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectScreen : MonoBehaviour
{
    public GameObject CollectPanel;
    public GameObject FishManager;

    public MenuManager menuManager;

    [SerializeField] public TextMeshProUGUI baitCount_txt;

    [SerializeField] public TextMeshProUGUI score;


    public bool isCapacityFull;
    void Start()
    {
        isCapacityFull = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCapacityFull == true)
        {   
            FishManager.SetActive(false);

            if (menuManager.isInMainMenu == false)
            {
                CollectPanel.SetActive(true);
                Debug.Log("Panel açýldý");
            }
            else if (menuManager.isInMainMenu == true)   //burasý çalýþmadý, o yüzden menu manager'da kapatýldý.
            {
                CollectPanel.SetActive(false);   
                Debug.Log("Panel Kapatýldý");
            }         
        }
    }


}
