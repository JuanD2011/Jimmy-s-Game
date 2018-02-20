﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreList : MonoBehaviour
{
    public GameObject playerScoreEntryPrefab;
    ScoreManager scoreManager;

    int lastChangeCounter;

    GameObject win;
    GameObject gameOver;

    GameObject winButton;
    GameObject gameOverButton;

    void Start ()
    {
        gameOver = GameObject.Find("GameOver");
        gameOver.gameObject.SetActive(false);

        win = GameObject.Find("WinText");
        win.gameObject.SetActive(false);

        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        lastChangeCounter = scoreManager.GetChangeCounter();

        winButton = GameObject.Find("WinButton");
        winButton.gameObject.SetActive(false);

        gameOverButton = GameObject.Find("GameOverButton");
        gameOverButton.gameObject.SetActive(false);
    }

    void Update ()
    {
        if (scoreManager == null)
        {
            Debug.LogError("You forgot to add the score manager component to a game object!");
            return;
        }

        if (scoreManager.GetChangeCounter() == lastChangeCounter)
        {
            return;
        }

        lastChangeCounter = scoreManager.GetChangeCounter();

        while (this.transform.childCount > 0)
        {
            Transform c = this.transform.GetChild(0);
            c.SetParent(null); 

            Destroy(c.gameObject);
        }

        string[] names = scoreManager.GetPlayerNames("Time");

        for (int i = 0; i < names.Length; i++)
        {
            GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
            go.transform.SetParent(this.transform);
            go.transform.Find("TextUsername").GetComponent<Text>().text = names[i];
            go.transform.Find("TextTime").GetComponent<Text>().text = scoreManager.GetScore(names[i], "Time").ToString("0.00");

            if (i == 5)
            {
                if (names[0] == "Jimmy")
                {
                    win.gameObject.SetActive(true);
                    winButton.gameObject.SetActive(true);
                }
                else
                {
                    gameOver.gameObject.SetActive(true);
                    gameOverButton.gameObject.SetActive(true);
                }   
            }
        }
    }
}
