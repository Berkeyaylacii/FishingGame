using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormColliders : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Fish")
        {
            Debug.Log("Yem yendi");
            this.gameObject.SetActive(false);
        }


    }
}
