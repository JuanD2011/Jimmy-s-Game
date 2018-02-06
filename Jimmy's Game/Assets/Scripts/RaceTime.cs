using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceTime : MonoBehaviour {

    public Text timerText;
    public GameObject colliderToStart;

    public float timeCountDown = 3f;
    public float raceTime = 0f;
    public float sharedTime = 0f;
    ScoreManager mScoreManager;

    public GameObject scoreBoard;

    public string playerOne = "P1";
    public string playerTwo = "P2";
    public string playerThree = "P3";
    public string playerFive = "P5";
    public string playerSix = "P6";

    private void Start()
    {
    }

    private void Update()
    {
        TimesUp();

        raceTime += Time.deltaTime;

    }

    public void TimesUp()
    {
        if (timeCountDown < 4f && timeCountDown > -1f)
        {
            timeCountDown -= Time.deltaTime;

            timerText.text = timeCountDown.ToString("0");
        }
        if (timeCountDown < 0)
        {
            timerText.gameObject.SetActive(false);

            colliderToStart.gameObject.SetActive(false);
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        
        GameObject collisioned = other.gameObject;

        if (collisioned.name == playerOne)
        {
            sharedTime = raceTime;

            mScoreManager.SetScore(playerOne, "Time", sharedTime);
            i++;
        }

        if (collisioned.name == playerTwo)
        {
            sharedTime = raceTime;

            mScoreManager.SetScore(playerTwo, "Time", sharedTime);
            i++;
        }

        if (collisioned.name == playerThree)
        {
            sharedTime = raceTime;

            mScoreManager.SetScore(playerThree, "Time", sharedTime);
            i++;
        }

        if (collisioned.name == playerFive)
        {
            sharedTime = raceTime;

            mScoreManager.SetScore(playerFive, "Time", sharedTime);
            i++;
        }
        if (collisioned.name == playerSix)
        {
            sharedTime = raceTime;

            mScoreManager.SetScore(playerSix, "Time", sharedTime);
            i++;
        }
        if (collisioned.name == "JimmyIddle")
        {
            sharedTime = raceTime;
            mScoreManager.SetScore("Jimmy", "Time", sharedTime);
            i ++;
        }
    }*/
}
