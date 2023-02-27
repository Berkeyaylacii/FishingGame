using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HookCollisions : MonoBehaviour
{
    public GameOverScreen gameOverScreen;

    public GameObject fish;

    public GameObject worm;

    public Collider2D colliderofHook;

    [SerializeField] public TextMeshProUGUI baitCount_txt;

    public float baitct;

    void Start()
    {
        baitCount_txt = GameObject.Find("Canvas/BaitCount/FishCapacity").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        enableCollider(); //enable collider if hook moved up to renew the bait
        baitct = float.Parse(baitCount_txt.text);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            colliderofHook.enabled = false;
        }

        if (collision.gameObject.tag == "Obstacle") 
        {
            colliderofHook.enabled = false;
            worm.gameObject.SetActive(false);
            
            if(baitct > 0)
            {
                baitct = baitct - 1;
                Debug.Log("Objeye çarptý yem düþtü -1");
                baitCount_txt.text = baitct.ToString();
            }

            
        }

    }

    private void enableCollider()
    {
        if(this.transform.position.y >= 3.3f)
        {
            colliderofHook.enabled = true;
            worm.gameObject.SetActive(true);

        }
    }

}
