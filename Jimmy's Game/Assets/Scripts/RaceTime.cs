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
    //ScoreManager mScoreManager;

    public GameObject scoreBoard;

    private void Start()
    {
    }

    private void Update()
    {
        TimesUp();
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

            raceTime += Time.deltaTime;
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        
        GameObject collisioned = other.gameObject;

        if (collisioned.tag == "Player")
        {
            sharedTime = raceTime;
            mScoreManager.ChangeScore("Jimmy", "Time", sharedTime);
        }
     }*/

}
