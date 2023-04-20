using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectScreen : MonoBehaviour
{
    public GameObject CollectPanel;
    public GameObject FishManager;
    public GameObject Hook;

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
            //FishManager.SetActive(false);
            Hook.SetActive(false);
            menuManager.isInGame = false;
            menuManager.ReturnMainMenu();
           
        }
    }


}
