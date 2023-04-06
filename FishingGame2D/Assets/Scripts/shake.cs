using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using DG.Tweening;

public class shake : MonoBehaviour
{
    public GameObject Fish;
    void Awake()
    {
        Fish = GameObject.FindGameObjectWithTag("Fish");
    }

    // Update is called once per frame
    void Update()
    {
        if (Fish != null)
        {
        transform.DOShakeRotation(5, Vector3.forward * 45, 2, 25, false).SetLoops(1, LoopType.Yoyo).OnComplete(delegate
        {
            transform.rotation = Quaternion.identity;
        });
        }
            
    }
}
