using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float curSpeed = 0.0f;
    public float maxSpeed = 10.0f;
    public float acceleration = 1.0f;

    public float jumpMag = 200f;

    bool canRow = true;

    bool onAir;

    Rigidbody mBody;

	void Start ()
    {
        mBody = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
		if(canRow)
        {
            Row();
        }
	}

    void Row()
    {
        if(!onAir)
        {
            transform.Translate(transform.forward * curSpeed);

            curSpeed += acceleration;

            if (curSpeed > maxSpeed)
            {
                curSpeed = maxSpeed;
            }           
        }
        if(onAir)
        {
            transform.Translate(transform.forward * curSpeed);

            curSpeed -= acceleration * 0.6f;

            if (curSpeed > maxSpeed)
            {
                curSpeed = maxSpeed;
            }       
        }

        Debug.Log("Speed: " + curSpeed);
    }

    void Jump()
    {
        Debug.Log("Jumping");
        mBody.AddForce(transform.up * jumpMag);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject triggered = other.gameObject;

        if(triggered.tag == "Obstacle")
        {
            Jump();
            Collider triggeredCollider = triggered.GetComponent<Collider>();
            triggeredCollider.enabled = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject collisioned = collision.gameObject;

        if(collisioned.tag == "Water")
        {
            onAir = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisioned = collision.gameObject;

        if (collisioned.tag == "Water")
        {
            onAir = false;
        }
    }
}
