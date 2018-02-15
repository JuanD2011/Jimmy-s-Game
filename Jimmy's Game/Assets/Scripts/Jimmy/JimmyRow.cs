using System.Collections;
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

    public float penaltyMag = 150f;
    public float rowMag = 100f;

    Animator mAnimator;

    //Audios

    AudioSource frog;
    AudioSource stone;
    AudioSource can;

    AudioSource asteroid;
    AudioSource spaceSuit;
    AudioSource radioTele;

    Scene mScene;

    public bool CanRow
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

    JimmyJump jimmyJump;

    void Start ()
    {
        mScene = SceneManager.GetActiveScene();

        mBody = GetComponent<Rigidbody>();
        canRow = false;
        penalty = false;
        penaltyTime = 1f;
        jimmyJump = GetComponent<JimmyJump>();

        mAnimator = GetComponent<Animator>();

        RaceTime.OnJimmyFinish += Finish;

        //Audios
        if (mScene.name == "Hood Level 1")
        {
            frog = GameObject.Find("FrogSound").GetComponent<AudioSource>();
            stone = GameObject.Find("StoneSound").GetComponent<AudioSource>();
            can = GameObject.Find("CanSound").GetComponent<AudioSource>();
        }

        if (mScene.name == "Milkyway")
        {
            asteroid = GameObject.Find("Asteroid").GetComponent<AudioSource>();
            spaceSuit = GameObject.Find("SpaceSuit").GetComponent<AudioSource>();
            radioTele = GameObject.Find("RadioTelescope").GetComponent<AudioSource>();
        }
    }
	
	void Update ()
    {
        if(!jimmyJump.OnAir)
        {
            if (canRow && !penalty)
            {
                Row();
            }
        }  

        if (penalty)
        {
            time += Time.deltaTime;
        }
        if(time >= penaltyTime)
        {
            time = 0;
            penalty = false;
        }

    }

    void Row()
    {
        if(Input.GetKeyDown("left"))
        {
            mAnimator.SetInteger("Row", -1);

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
            mAnimator.SetInteger("Row", 1);
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
                if(collision.relativeVelocity.z != 0)
                {
                    collisioned.SetActive(false);
                    Penalty();
                }
                if (collision.relativeVelocity.y != 0)
                {
                    collisioned.SetActive(false);
                    Penalty();
                }
            }
            if (collisioned.tag == "Stone")
            {
                stone.Play();
                if (collision.relativeVelocity.z != 0)
                {
                    collisioned.SetActive(false);
                    Penalty();
                }
                if (collision.relativeVelocity.y > 0)
                {
                    collisioned.SetActive(false);
                    Penalty();
                }
            }
            if (collisioned.tag == "JerryCan")
            {
                can.Play();
                if (collision.relativeVelocity.z != 0)
                {
                    collisioned.SetActive(false);
                    Penalty();
                }
                if (collision.relativeVelocity.y != 0)
                {
                    collisioned.SetActive(false);
                    Penalty();
                }
            }
            if(collisioned.tag == "Obstacle")
            {
                if (collision.relativeVelocity.z != 0)
                {
                    collisioned.SetActive(false);
                    Penalty();
                }
                
                if (collision.relativeVelocity.y != 0)
                {
                    collisioned.SetActive(false);
                    Penalty();
                }
            }
        }

        if (mScene.name == "Milkyway")
        {
            if (collisioned.tag == "Asteroid")
            {
                asteroid.Play();
            }

            if (collisioned.tag == "SpaceSuit")
            {
                spaceSuit.Play();
            }
            if (collisioned.tag == "RadioTelescope")
            {
                radioTele.Play();
            }
        }
    }

    void Finish()
    {
        canRow = false;
        mAnimator.SetInteger("Row", 0);
    }

}
