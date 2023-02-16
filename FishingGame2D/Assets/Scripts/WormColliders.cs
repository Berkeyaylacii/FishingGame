using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WormColliders : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI baitCount_txt;


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Fish")
        {
            Debug.Log("Yem yendi");
            this.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Yem düþtü !");
            this.gameObject.SetActive(false);

            float skor = float.Parse(baitCount_txt.text);
            if (skor >= 0)
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
