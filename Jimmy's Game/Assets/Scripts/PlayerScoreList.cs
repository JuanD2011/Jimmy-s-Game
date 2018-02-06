using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreList : MonoBehaviour
{
    public GameObject playerScoreEntryPrefab;
    ScoreManager scoreManager;

    int lastChangeCounter;

	void Start ()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();

        lastChangeCounter = scoreManager.GetChangeCounter();
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

        foreach (string name in names)
        {
            GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
            go.transform.SetParent(this.transform);
            go.transform.Find("TextUsername").GetComponent<Text>().text = name;
            go.transform.Find("TextTime").GetComponent<Text>().text = scoreManager.GetScore(name, "Time").ToString();
        }
    }
}
