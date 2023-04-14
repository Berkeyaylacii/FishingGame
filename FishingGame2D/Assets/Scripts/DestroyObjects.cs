using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    public GameObject Fish;
    public GameObject Obstacle;
    // Start is called before the first frame update

    void Update (){
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fish" || collision.gameObject.tag == "Obstacle")
        {
            Destroy(collision.gameObject);
        }

        //Find alternative way to this.
    }
}
