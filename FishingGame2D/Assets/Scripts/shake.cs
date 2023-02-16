using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using DG.Tweening;

public class shake : MonoBehaviour
{  
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.DOShakeRotation(5, Vector3.forward * 45, 2, 25, false).SetLoops(1, LoopType.Yoyo).OnComplete(delegate
        {
            transform.rotation = Quaternion.identity;
        });
    }


}
