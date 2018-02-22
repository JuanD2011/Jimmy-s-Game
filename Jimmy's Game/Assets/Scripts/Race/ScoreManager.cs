using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    Dictionary<string, Dictionary<string,float>> playerScores;

    int changeCounter = 0;

    private void Start()
    {
        SetScore("P1", "Time", 0f);
        SetScore("P2", "Time", 0f);
        SetScore("P3", "Time", 0f);
        SetScore("Jimmy", "Time", 0f);
        SetScore("P5", "Time", 0f);
        SetScore("P6", "Time", 0f);
    }

    void Init()
    {
        if (playerScores != null)
            return;

        playerScores = new Dictionary<string, Dictionary<string, float>>();
    }

    public float GetScore(string _username, string _scoreType)
    {
        Init();

        if (playerScores.ContainsKey(_username) == false)
        {
            return 0;
        }

        if (playerScores[_username].ContainsKey(_scoreType)== false)
        {
            return 0;
        }

        return playerScores[_username][_scoreType];
    }

    public void SetScore(string _username, string _scoreType, float value)
    {
        Init();

        changeCounter++;

        if (playerScores.ContainsKey(_username) == false)
        {
            playerScores[_username] = new Dictionary<string, float>();
        }

        playerScores[_username][_scoreType] = value;
    }

    public void ChangeScore(string _username, string _scoreType, float amount)
    {
        Init();
        float currScore = GetScore(_username, _scoreType);
        SetScore(_username, _scoreType, currScore + amount); 
    }

    public string[] GetPlayerNames()
    {
        Init();
        return playerScores.Keys.ToArray();
    }

    public string[] GetPlayerNames(string sortingScoreType)
    {
        Init();

        return playerScores.Keys.OrderBy(n => GetScore(n, sortingScoreType)).ToArray();

    }

    public int GetChangeCounter()
    {
        return changeCounter;
    }

}
