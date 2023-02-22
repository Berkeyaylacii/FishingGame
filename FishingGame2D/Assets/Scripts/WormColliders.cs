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
            this.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Obstacle")
        {
            this.gameObject.SetActive(false);
        }

    }
}
