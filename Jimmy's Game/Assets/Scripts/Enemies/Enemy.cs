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

    Animator mAnimator;

	void Start ()
    {
        mAnimator = GetComponent<Animator>();

        mAnimator.SetInteger("Row", 0);

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
        transform.Translate(Vector3.forward * curSpeed);

        curSpeed += acceleration;

        if (curSpeed > maxSpeed)
        {
            curSpeed = maxSpeed;
        }
    }

    void Finish()
    {
        canRow = false;
    }
}
