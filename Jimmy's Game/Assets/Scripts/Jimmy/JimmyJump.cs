﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JimmyJump : MonoBehaviour
{
    Rigidbody mBody;
    bool canJump;
    public float jumpForce = 200f;
    bool onAir;
    bool canJumpInit;

    Scene mScene;

    AudioSource splashWater;
    AudioSource jumpSound;

    JimmyRow jimmyRow;

    public bool OnAir
    {
        get
        {
            return onAir;
        }
        set
        {
            onAir = value;
        }
    }

    public bool CanJumpInit
    {
        get
        {
            return canJumpInit;
        }
        set
        {
            canJumpInit = value;
        }
    }

	void Start ()
    {
        mScene = SceneManager.GetActiveScene();

        jimmyRow = GetComponent<JimmyRow>();

        canJumpInit = false;

        mBody = GetComponent<Rigidbody>();

        if (mScene.name == "HoodLevel" || mScene.name == "NationalLevel" || mScene.name == "WorldLevel")
        {
            splashWater = GameObject.Find("SplashAudio").GetComponent<AudioSource>();
        }

        jumpSound = GameObject.Find("Jump").GetComponent<AudioSource>();

        RaceTime.OnRaceStart += RaceHasStarted;
	}
	
	void Update ()
    {
		if(canJump && canJumpInit)
        {
            Jump();
        }
	}

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            mBody.AddForce(Vector3.up * jumpForce);
            //mBody.AddForce(-Vector3.forward * 150f);
        }
    }

    void RaceHasStarted()
    {
        canJumpInit = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisioned = collision.gameObject;

        if(collisioned.tag == "Water")
        {
            canJump = true;
            onAir = false;

            if (mScene.name == "HoodLevel" || mScene.name == "NationalLevel" || mScene.name == "WorldLevel")
            {
                splashWater.Play();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject collisioned = collision.gameObject;

        if(collisioned.tag == "Water")
        {
            canJump = false;
            onAir = true;
            jimmyRow.FirstAssigned = false;

            jumpSound.Play();
        }
    }
}
