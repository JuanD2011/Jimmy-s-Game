using System.Collections;
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
    AudioSource splashWater;

    Scene mScene = new Scene();

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
        canJumpInit = false;

        mBody = GetComponent<Rigidbody>();

        if (mScene.name == "Hood Level 1")
        {
            splashWater = GameObject.Find("SplashAudio").GetComponent<AudioSource>();
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisioned = collision.gameObject;

        if(collisioned.tag == "Water")
        {
            canJump = true;
            onAir = false;

            if (mScene.name == "Hood Level 1")
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
        }
    }
}
