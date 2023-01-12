using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidbody;

    [SerializeField] float thrustspeed = 1000f;
    [SerializeField] float rotatespeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessMovement();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space)){
            rigidbody.AddRelativeForce(Vector3.up * thrustspeed * Time.deltaTime);
        }
    }

    void ProcessMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotatespeed * Time.deltaTime);
            rigidbody.freezeRotation = false;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rigidbody.freezeRotation = true;
            transform.Rotate(-Vector3.forward * rotatespeed * Time.deltaTime);
            rigidbody.freezeRotation = false;
        }
    }
}
