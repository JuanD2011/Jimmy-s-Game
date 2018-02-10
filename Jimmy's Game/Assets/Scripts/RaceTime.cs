﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class RaceTime : MonoBehaviour
{
    //Race time
    GameObject countDown;
    public GameObject colliderToStart;
    public Text timerText;
    public float timeCountDown = 3f;
    public float raceTime = 0f;
    public float sharedTime = 0f;

    //Score
    ScoreManager mScoreManager;
    GameObject scoreBoard;

    //Tutorial
    VideoPlayer videoTut;

    //PlayerCount
    int i = 0;
   
    //Events 
    public delegate void OnWin();
    public static event OnWin OnJimmyWon;

    public delegate void ColliderDeactivated();
    public static event ColliderDeactivated OnRowBots;

    private void Start()
    {
        mScoreManager = GetComponent<ScoreManager>();

        scoreBoard = GameObject.Find("CanvasScoreBoard");
        scoreBoard.gameObject.SetActive(false);

        videoTut = GameObject.Find("VideoTutorial").GetComponent<VideoPlayer>();

        countDown = GameObject.Find("TextCountDown");
        countDown.gameObject.SetActive(false);
    }

    private void Update()
    {
        TutorialComplete();
        TimesUp();
    }

    public void TutorialComplete()
    {
        videoTut.Play();

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            videoTut.gameObject.SetActive(false);
        }
    }

    public void TimesUp()
    {
        if (videoTut.gameObject.activeInHierarchy == false)
        {
            countDown.gameObject.SetActive(true);

            if (timeCountDown < 4f && timeCountDown > -1f)
            {
                timeCountDown -= Time.deltaTime;

                timerText.text = timeCountDown.ToString("0");
            }
            if (timeCountDown < 0)
            {
                timerText.gameObject.SetActive(false);
                colliderToStart.gameObject.SetActive(false);

                OnRowBots();

                raceTime += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        GameObject collisioned = other.gameObject;

        if (collisioned.name == "P1")
        {
            sharedTime = raceTime;
            mScoreManager.ChangeScore("P1", "Time", sharedTime);
            i++;
        }
        if (collisioned.name == "JimmyIddle")
        {
            sharedTime = raceTime;
            mScoreManager.ChangeScore("Jimmy", "Time", sharedTime);
            i++;
        }
        if (collisioned.name == "P2")
        {
            sharedTime = raceTime;
            mScoreManager.ChangeScore("P2", "Time", sharedTime);
            i++;
        }
        if (collisioned.name == "P3")
        {
            sharedTime = raceTime;
            mScoreManager.ChangeScore("P3", "Time", sharedTime);
            i++;
        }
        if (collisioned.name == "P5")
        {
            sharedTime = raceTime;
            mScoreManager.ChangeScore("P5", "Time", sharedTime);
            i++;
        }
        if (collisioned.name == "P6")
        {
            sharedTime = raceTime;
            mScoreManager.ChangeScore("P6", "Time", sharedTime);
            i++;
        }

        if (i == 6)
        {
            scoreBoard.gameObject.SetActive(true);
            OnJimmyWon();
        }
    }
}
