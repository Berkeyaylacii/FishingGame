using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    GameObject hook;
    GameObject hand;
    //GameObject score;

    [SerializeField] private TextMeshProUGUI tap_txt;
    [SerializeField] private GameObject hand_icon;
    [SerializeField] private GameObject score;

    GameObject script;

    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        hook = GameObject.FindGameObjectWithTag("Hook");
        hand = GameObject.FindGameObjectWithTag("Hand");
        score = GameObject.FindGameObjectWithTag("Score");
        hook.GetComponent<SpawnFish>().enabled = false;

        hook.SetActive(false);
        score.SetActive(false);
        mainCamera = Camera.main;
        tap_txt.transform.DOScale(1.1f, 0.5f).SetLoops(10000, LoopType.Yoyo).SetEase(Ease.InOutFlash);
        hand_icon.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-200f, -100f), 1f).SetLoops(100000, LoopType.Yoyo).SetEase(Ease.InOutFlash);
    }


    public void LoadGamee()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MoveCamera()
    {
        mainCamera.transform.Translate(0, -8.5f, 0);
        hook.SetActive(true);
        hand.SetActive(false);
        score.SetActive(true);
        hook.GetComponent<SpawnFish>().enabled = true; 
    }
}
