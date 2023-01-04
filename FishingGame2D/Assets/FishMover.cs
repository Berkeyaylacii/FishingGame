using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMover : MonoBehaviour
{
    public GameObject fish;

    public Rigidbody2D rbofFish;

    public Transform[] waypoints1;
    private int _currWaypointIndex = 0;
    private float _speed = 1f;

   // public Transform[] waypoints2;
   // public Transform[] waypoints3;
    //public Transform[] waypoints4;

    bool ifHooked = false;

    void Start()
    {
    }

    
    void Update()
    {

        transform.position += Vector3.left * Time.deltaTime * _speed;
    }



    /*void moveFish()
    {
        //float rand = Random.Range(1, 4);

        Transform wp = waypoints1[_currWaypointIndex];


        if (Vector3.Distance(transform.position, wp.position) < 0.01f)
        {
            _currWaypointIndex = (_currWaypointIndex + 1);
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                wp.position,
                _speed * Time.deltaTime
                );
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hook")
        {
            ifHooked = true;
            transform.SetParent(GameObject.FindGameObjectWithTag("Hanger").transform, true);

            transform.position = GameObject.FindGameObjectWithTag("Hanger").transform.position + new Vector3(0.5f, 0, 0);

        

           // rbofFish.constraints =  RigidbodyConstraints2D.None;

        }

        if(collision.gameObject.tag == "FishingBag")
        {
            Destroy(gameObject);
        }
    }
}
