using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScoreList : MonoBehaviour
{
    public GameObject playerScoreEntryPrefab;
    ScoreManager scoreManager;

    int lastChangeCounter;

    GameObject win;
    GameObject gameOver;

    void Start ()
    {

        gameOver = GameObject.Find("GameOver");
        gameOver.gameObject.SetActive(false);

        win = GameObject.Find("WinText");
        win.gameObject.SetActive(false);

        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        lastChangeCounter = scoreManager.GetChangeCounter();

        RaceTime.OnJimmyWon += Win;
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
            go.transform.Find("TextTime").GetComponent<Text>().text = scoreManager.GetScore(names[i], "Time").ToString();

            if (i == 5)
            {
                if (names[0] == "Jimmy")
                {
                    Win();
                }
                else
                {
                    Loose();
                    win.gameObject.SetActive(false);
                }   
            }
        }
    }

    public void Win()
    {
         win.gameObject.SetActive(true);
         Debug.Log("Yeiii");
    }

    public void Loose()
    {
        gameOver.gameObject.SetActive(true);

        if (Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
