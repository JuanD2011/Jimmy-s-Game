using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                mBody.AddForce(transform.forward * rowMag);
                Debug.Log("Left");
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
                mBody.AddForce(transform.forward * rowMag);
                Debug.Log("Right");
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
        mBody.AddForce(-transform.forward * penaltyMag);
        mBody.velocity = Vector3.zero;
        Debug.Log("Penalty");
    }
    
}
