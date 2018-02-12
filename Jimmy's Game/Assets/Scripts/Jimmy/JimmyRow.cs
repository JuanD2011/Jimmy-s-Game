﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JimmyRow : MonoBehaviour
{
    Rigidbody mBody;
    bool canRow;
    float penaltyTime;
    bool penalty;
    float time = 0f;

    bool leftArrow;
    bool rightArrow;

    bool firstTime;

    public float penaltyMag = 100f;
    public float rowMag = 100f;

    //Audios

    AudioSource frog;
    AudioSource stone;
    AudioSource can;

    Scene mScene = new Scene();

    bool CanRow
    {
        get
        {
            return canRow;
        }
        set
        {
            canRow = value;
        }
    }

    void Start ()
    {
        mBody = GetComponent<Rigidbody>();
        canRow = true;
        penalty = false;
        penaltyTime = 2f;

        //Audios
        if (mScene.name == "Hood Level 1")
        {
            frog = GameObject.Find("FrogSound").GetComponent<AudioSource>();
            stone = GameObject.Find("StoneSound").GetComponent<AudioSource>();
            can = GameObject.Find("CanSound").GetComponent<AudioSource>();
        }
    }
	
	void Update ()
    {       
        if(canRow && !penalty)
        {
            Row();
        }

		if(penalty)
        {
            time += Time.deltaTime;
            penalty = false;
        }
        if(time >= penaltyTime)
        {
            time = 0;
        }
    }

    void Row()
    {
        if(Input.GetKeyDown("left"))
        {
            if(!leftArrow)
            {
                leftArrow = true;
                rightArrow = false;
                mBody.AddForce(Vector3.forward * rowMag);
            }
            else
            {
                if(leftArrow)
                {
                    Penalty();
                }
            }
        }

        if (Input.GetKeyDown("right"))
        {
            if (!rightArrow)
            {
                rightArrow = true;
                leftArrow = false;
                mBody.AddForce(Vector3.forward * rowMag);
            }
            else
            {
                if (rightArrow)
                {
                    Penalty();
                }
            }
        }

        if(Input.GetKeyDown("right") && Input.GetKeyDown("left"))
        {
            Penalty();
        }
    }
    
    void Penalty()
    {
        penalty = true;
        mBody.AddForce(-Vector3.forward * penaltyMag);
        mBody.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisioned = collision.gameObject;

        if (mScene.name == "Hood Level 1")
        {
            if (collisioned.tag == "Frog")
            {
                frog.Play();
            }

            if (collisioned.tag == "Stone")
            {
                stone.Play();
            }
            if (collisioned.tag == "JerryCan")
            {
                can.Play();
            }
        }
    }

}
