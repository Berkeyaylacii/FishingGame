using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookCollisions : MonoBehaviour
{
    public GameObject fish;

    public GameObject worm;

    public Collider2D colliderofHook;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enableCollider();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            colliderofHook.enabled = false;
        }

        if(collision.gameObject.tag == "Obstacle") 
        {
            worm.gameObject.SetActive(false);
            colliderofHook.enabled = false;
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
