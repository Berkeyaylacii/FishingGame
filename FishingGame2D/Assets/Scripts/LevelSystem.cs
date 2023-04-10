using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public GameObject totalScore;
    // Start is called before the first frame update
    void Start()
    {
        totalScore = GameObject.FindGameObjectWithTag("TotalScore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void calculateLevel()
    {

    }
}
