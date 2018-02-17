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

    Animator mAnimator;

	void Start ()
    {
        mAnimator = GetComponent<Animator>();

        mAnimator.SetInteger("Row", 0);

        mBody = GetComponent<Rigidbody>();
        canRow = false;

        RaceTime.OnRowBots += CanRow;

        maxSpeed = Random.Range(maxSpeedmin, maxSpeedmax);

        Debug.Log(maxSpeed);
        RaceTime.OnEnemiesFinish += Finish;
    }
	
	void Update ()
    {
		if(canRow)
        {
            Row();
            mAnimator.SetInteger("Row", 1);
        }
        if(!canRow)
        {
            mAnimator.SetInteger("Row", 0);
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
            transform.Translate(Vector3.forward * curSpeed);

            curSpeed += acceleration;

            if (curSpeed > maxSpeed)
            {
                curSpeed = maxSpeed;
            }           
        }
        
        if(onAir)
        {
            transform.Translate(Vector3.forward * curSpeed);

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

    void Finish()
    {
        canRow = false;
    }
}
