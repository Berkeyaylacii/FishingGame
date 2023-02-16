using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class RightMover : MonoBehaviour
{

    public GameObject fish;

    public Rigidbody2D rbofFish;

    TextMeshProUGUI score_txt;
    TextMeshProUGUI baitCount_txt;

    public bool ifFishHooked = false;
    public float _speed = 2f;
    // Start is called before the first frame update

    
    void Start()
    {
        score_txt = GameObject.Find("Canvas/Score").GetComponent<TextMeshProUGUI>();

        baitCount_txt = GameObject.Find("Canvas/BaitCount/FishCapacity").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ifFishHooked == false)
        {
            transform.position += Vector3.right * Time.deltaTime * _speed;
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
            Debug.Log("Balýk varken çarptý");
            Destroy(gameObject);

            float skor = float.Parse(baitCount_txt.text);
            if (skor > 0)
            {
                skor = skor - 1;
                baitCount_txt.text = skor.ToString();
            }
            else
            {
                Debug.Log("Game OVer !");
            }
        }

    }
}
