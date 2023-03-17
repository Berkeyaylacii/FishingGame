using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TapAnimation : MonoBehaviour
{
    [SerializeField] private GameObject hand_icon;
    [SerializeField] private TextMeshProUGUI tap_txt;
    // Start is called before the first frame update
    void Start()
    {
        tap_txt.transform.DOScale(1.1f, 0.5f).SetLoops(10000, LoopType.Yoyo).SetEase(Ease.InOutFlash);
        hand_icon.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-200f, -100f), 1f).SetLoops(100000, LoopType.Yoyo).SetEase(Ease.InOutFlash);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
