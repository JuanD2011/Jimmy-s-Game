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
    public float constForce = 0.01f;

    bool leftArrow;
    bool rightArrow;

    bool firstAssigned;

    public float penaltyMag = 150f;
    public float rowMag = 100f;

    public float buffMag = 100f;
    public float debuffMag = 300f;

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

    public bool FirstAssigned
    {
        get
        {
            return firstAssigned;
        }
        set
        {
            firstAssigned = value;
        }
    }

    JimmyJump jimmyJump;

    void Start ()
    {
        mScene = SceneManager.GetActiveScene();

        mBody = GetComponent<Rigidbody>();
        canRow = false;
        penalty = false;
        penaltyTime = 0.5f;
        jimmyJump = GetComponent<JimmyJump>();

        firstAssigned = false;

        mAnimator = GetComponent<Animator>();

        RaceTime.OnJimmyFinish += Finish;

        //Audios
        /*if (mScene.name == "Hood Level 1")
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
        }*/
    }
	
	void Update ()
    {
        if(canRow && !jimmyJump.OnAir && !penalty)
        {
            int firstKey = 0;

            if(!firstAssigned)
            {
                firstKey = GetFirstKey();

                switch (firstKey)
                {
                    case -1:
                        leftArrow = true;
                        rightArrow = false;
                        break;
                    case 1:
                        rightArrow = true;
                        leftArrow = false;
                        break;
                    case 0:
                        firstKey = GetFirstKey();
                        break;
                }
                firstAssigned = true;
            }
            else
            {
                Row();
            }        
        }
        

        if(canRow && !jimmyJump.OnAir)
        {
            mBody.AddForce(Vector3.forward * constForce);
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
        firstAssigned = false;
        mBody.AddForce(-Vector3.forward * penaltyMag);
    }

    /*private void OnCollisionEnter(Collision collision)
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
    }*/
    
    void Finish()
    {
        canRow = false;
        mAnimator.SetInteger("Row", 0);
    }

    int GetFirstKey()
    {
        int firstkey = 0;

        if(Input.GetKeyDown("left"))
        {
            firstkey = -1;
            mBody.AddForce(Vector3.forward * rowMag);
        }
        if(Input.GetKeyDown("right"))
        {
            firstkey = 1;
            mBody.AddForce(Vector3.forward * rowMag);
        }

        firstAssigned = true;

        return firstkey;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject triggered = other.gameObject;

        if(triggered.tag == "Buff")
        {
            Buff();
            triggered.SetActive(false);
        }
        if(triggered.tag == "Debuff")
        {
            Debuff();
            triggered.SetActive(false);
        }
    }

    void Buff()
    {
        mBody.AddForce(Vector3.forward * buffMag);
    }

    void Debuff()
    {
        mBody.AddForce(-Vector3.forward * debuffMag);
    }
}
