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

    bool canRow;

    float time;
    int changeTime;

    bool canChange;

    Animator mAnimator;
    Rigidbody mBody;

	void Start ()
    {
        changeTime = Random.Range(1, 8);

        canChange = true;

        mAnimator = GetComponent<Animator>();
        mBody = GetComponent<Rigidbody>();

        mAnimator.SetInteger("Row", 0);

        canRow = false;

        RaceTime.OnRowBots += CanRow;

        maxSpeed = Random.Range(maxSpeedmin, maxSpeedmax);

        RaceTime.OnEnemiesFinish += Finish;
    }

    void Update()
    {
        if (canRow)
        {
            Row();
            mAnimator.SetInteger("Row", 1);
            time += Time.deltaTime;
        }
        if (!canRow)
        {
            mAnimator.SetInteger("Row", 0);
        }

        if ((time >= changeTime) && canRow && canChange)
        {
            RandomChange();
            time = 0;
            canChange = false;
        }
	}

    public void CanRow()
    {
        canRow = true;

    }

    void Row()
    {
        transform.Translate(Vector3.forward * curSpeed);

        curSpeed += acceleration * Time.deltaTime;

        if (curSpeed > maxSpeed)
        {
            curSpeed = maxSpeed;
        }
    }

    void Finish()
    {
        canRow = false;
    }

    void RandomChange()
    {
        int change;
        change = Random.Range(1, 3);

        if(change == 1)
        {
            mBody.AddForce(-Vector3.forward * 200f);
        }
        if(change == 2)
        {
            mBody.AddForce(Vector3.forward * 200f);
        }
    }
}
