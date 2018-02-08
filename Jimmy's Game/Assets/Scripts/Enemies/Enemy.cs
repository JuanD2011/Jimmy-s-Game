using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float curSpeed = 0.0f;
    public float maxSpeedmin;
    public float maxSpeedmax;
    public float acceleration = 1.0f;

    float maxSpeed;

    public float jumpMag = 200f;

    bool canRow;

    bool onAir;

    Rigidbody mBody;

	void Start ()
    {
        mBody = GetComponent<Rigidbody>();
        canRow = false;

        RaceTime.OnRowBots += CanRow;

        maxSpeed = Random.Range(maxSpeedmin, maxSpeedmax);

        Debug.Log(maxSpeed);
	}
	
	void Update ()
    {
		if(canRow)
        {
            Row();
        }
	}

    public void CanRow()
    {
        canRow = true;
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

            curSpeed -= acceleration * 0.2f;

            if (curSpeed > maxSpeed)
            {
                curSpeed = maxSpeed;
            }       
        }
    }

    void Jump()
    {
        mBody.AddForce(transform.up * jumpMag);
    }
/*
    private void OnTriggerEnter(Collider other)
    {
        GameObject triggered = other.gameObject;

        if(triggered.tag == "Obstacle")
        {
            Jump();
            Collider triggeredCollider = triggered.GetComponent<Collider>();
            triggeredCollider.enabled = false;
        }
    }*/

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

        if (collisioned.tag == "Obstacle")
        {
            Jump();
            Collider triggeredCollider = collisioned.GetComponent<Collider>();
            triggeredCollider.enabled = false;
        }
    }
}
