using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class RightMover : MonoBehaviour
{
    public GameObject GameOverPanel;

    public GameObject fish;

    public Rigidbody2D rbofFish;

    public GameObject hook;

    TextMeshProUGUI score_txt; 

    public bool ifFishHooked = false;
    public float _speed = 2f;

    TextMeshProUGUI baitCount_txt;

    void Start()
    {
        score_txt = GameObject.Find("Canvas/Score").GetComponent<TextMeshProUGUI>();

        baitCount_txt = GameObject.Find("Canvas/BaitCount/FishCapacity").GetComponent<TextMeshProUGUI>();

        hook = GameObject.FindGameObjectWithTag("Hook");
        hook.SetActive(true);

        GameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
    }

    void Update()
    {
        if (ifFishHooked == false)
        {
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }

        if (baitCount_txt.text == "0")
        {
            hook.SetActive(false);
            GameOver();
            Time.timeScale = 0;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hook")
        {
            ifFishHooked = true;

            transform.DOShakeRotation(5, Vector3.forward * 10, 10, 90, false).SetLoops(1, LoopType.Yoyo).OnComplete(delegate
            {
                transform.rotation = Quaternion.identity;
            });

            transform.SetParent(GameObject.FindGameObjectWithTag("Hanger").transform, true);
            transform.position = GameObject.FindGameObjectWithTag("Hanger").transform.position + new Vector3(-0.3f, 0, 0);
        }

        if (collision.gameObject.tag == "FishingBag")
        {
            float skor = float.Parse(score_txt.text);
            skor = skor + 1;
            score_txt.text = skor.ToString();

            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }



        if (collision.gameObject.tag == "Obstacle" && ifFishHooked == true)
        {   
            Destroy(gameObject);

           float skor = float.Parse(baitCount_txt.text);
            if (skor > 0)
            {
                skor = skor - 1;
                Debug.Log("Bal�k varken Yem d��t� -1");
                baitCount_txt.text = skor.ToString();

            }
        }

    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
    }

}
