﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimmyJump : MonoBehaviour
{
    Rigidbody mBody;
    bool canJump;
    public float jumpForce = 200f;

    AudioSource splashWater;

	void Start ()
    {
        mBody = GetComponent<Rigidbody>();

        splashWater = GameObject.Find("SplashAudio").GetComponent<AudioSource>();
	}
	
	void Update ()
    {
		if(canJump)
        {
            Jump();
        }
	}

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            mBody.AddForce(transform.up * jumpForce);
            mBody.AddForce(-transform.forward * 150f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisioned = collision.gameObject;

        if(collisioned.tag == "Water")
        {
            canJump = true;
            splashWater.Play();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject collisioned = collision.gameObject;

        if(collisioned.tag == "Water")
        {
            canJump = false;
        }
    }
}
