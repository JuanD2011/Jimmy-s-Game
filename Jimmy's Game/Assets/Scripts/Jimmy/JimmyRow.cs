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
    AudioSource buffSound;
    AudioSource debuffSound;
    AudioSource ah;

    Scene mScene;

    GameObject buffJimmyParticle;
    bool getJimmyParticle = false;
    float timeParticleBuff = 0f;

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
        buffJimmyParticle = GameObject.Find("BuffJimmyParticle");
        buffJimmyParticle.gameObject.SetActive(false);

        buffSound = GameObject.Find("BuffSound").GetComponent<AudioSource>();
        debuffSound = GameObject.Find("DebuffSound").GetComponent<AudioSource>();
        ah = GameObject.Find("Ah").GetComponent<AudioSource>();

        mScene = SceneManager.GetActiveScene();

        mBody = GetComponent<Rigidbody>();
        canRow = false;
        penalty = false;
        penaltyTime = 0.025f;
        jimmyJump = GetComponent<JimmyJump>();

        firstAssigned = false;

        mAnimator = GetComponent<Animator>();

        RaceTime.OnJimmyFinish += Finish;
    }
	
	void Update ()
    {
        if(canRow && !penalty)
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
        

        if(canRow)
        {
            if(!jimmyJump.OnAir)
            {
                mBody.AddForce(Vector3.forward * constForce);
            }
            if(jimmyJump.OnAir)
            {
                mBody.AddForce(Vector3.forward * constForce*0.75f);
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

        if (getJimmyParticle == true)
        {
            timeParticleBuff += Time.deltaTime;
            buffJimmyParticle.gameObject.SetActive(true);

            if (timeParticleBuff > 3)
            {
                buffJimmyParticle.gameObject.SetActive(false);
                getJimmyParticle = false;
                timeParticleBuff = 0f;
            }
        }
    }

    void Row()
    {
        if(Input.GetKeyDown("left"))
        {
            mAnimator.SetInteger("Row", -1);

            if (!jimmyJump.OnAir)
            {
                if (!leftArrow)
                {
                    leftArrow = true;
                    rightArrow = false;
                    mBody.AddForce(Vector3.forward * rowMag);
                }
                else
                {
                    if (leftArrow)
                    {
                        Penalty();
                    }
                }
            }
            if(jimmyJump.OnAir)
            {
                if (!leftArrow)
                {
                    leftArrow = true;
                    rightArrow = false;
                    mBody.AddForce(Vector3.forward * rowMag*0.25f);
                }
                else
                {
                    if (leftArrow)
                    {
                        Penalty();
                    }
                }
            }        
        }

        if (Input.GetKeyDown("right"))
        {
            mAnimator.SetInteger("Row", 1);

            if (!jimmyJump.OnAir)
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
            if(jimmyJump.OnAir)
            {
                if (!rightArrow)
                {
                    rightArrow = true;
                    leftArrow = false;
                    mBody.AddForce(Vector3.forward * rowMag*0.25f);
                }
                else
                {
                    if (rightArrow)
                    {
                        Penalty();
                    }
                }
            }         
        }
        /*
        if(Input.GetKeyDown("right") && Input.GetKeyDown("left"))
        {
            Penalty();
        }
        */
    }
    
    void Penalty()
    {
        ah.Play();
        penalty = true;
        firstAssigned = false;
        mBody.AddForce(-Vector3.forward * penaltyMag);
    }
    
    void Finish()
    {
        canRow = false;
        //mAnimator.SetInteger("Row", 0);
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
            buffSound.Play();
            getJimmyParticle = true;
        }
        if(triggered.tag == "Debuff")
        {
            Debuff();
            triggered.SetActive(false);
            debuffSound.Play();
            ah.Play();
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
