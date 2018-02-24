using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class RaceTime : MonoBehaviour
{
    //Race time
    GameObject countDown;
    public Text timerText;
    public float timeCountDown = 3f;
    public float raceTime = 0f;
    public float sharedTime = 0f;

    GameObject lvlPresent;

    GameObject goddy;
    GameObject goddyText;

    //Score
    ScoreManager mScoreManager;
    GameObject scoreBoard;

    //Tutorial
    VideoPlayer videoTut;

    //PlayerCount
    int i = 0;
   
    //Events 

    public delegate void ColliderDeactivated();
    public static event ColliderDeactivated OnRowBots;

    public delegate void FinishLine();
    public static event FinishLine OnJimmyFinish;

    public delegate void EnemiesFinish();
    public static event EnemiesFinish OnEnemiesFinish;


    //Scenes
    Scene mScene;

    //Audio
    AudioSource countDownAudio;
    AudioSource finishing;

    int audioCount = 0;

    private void Start()
    {
        mScene = SceneManager.GetActiveScene();
        mScoreManager = GetComponent<ScoreManager>();

        scoreBoard = GameObject.Find("CanvasScoreBoard");
        scoreBoard.gameObject.SetActive(false);

        if(mScene.name == "Hood Level 1")
            videoTut = GameObject.Find("VideoTutorial").GetComponent<VideoPlayer>();

        countDown = GameObject.Find("TextCountDown");
        countDown.gameObject.SetActive(false);

        countDownAudio = GameObject.Find("CountDownAudio").GetComponent<AudioSource>();
        finishing = GameObject.Find("Finishing").GetComponent<AudioSource>();

        lvlPresent = GameObject.Find("LevelPresentation");

        if (mScene.name == "GoddyLevel")
        {
            goddy = GameObject.Find("P1");
            goddyText = GameObject.Find("TextGoddy");

            goddyText.gameObject.SetActive(false);
        }

    }

    private void Update()
    {
        if (mScene.name == "Hood Level 1")
            TutorialComplete();

        TimesUp();

    }

    public void TutorialComplete()
    {
        
        videoTut.Play();

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            audioCount += 1;
            videoTut.gameObject.SetActive(false);

            if (audioCount <= 1)
            {
                countDownAudio.Play();
            }
        }
    }

    public void TimesUp()
    {
        if (mScene.name == "Hood Level 1")
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

                    JimmyRow jimmyRow = GameObject.Find("JimmyIddle").GetComponent<JimmyRow>();
                    jimmyRow.CanRow = true;
                    JimmyJump jimmyJump = GameObject.Find("JimmyIddle").GetComponent<JimmyJump>();
                    jimmyJump.CanJumpInit = true;

                    OnRowBots();

                    raceTime += Time.deltaTime;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space))
            {
                audioCount += 1;
                lvlPresent.gameObject.SetActive(false);

                if (audioCount <= 1)
                {
                    countDownAudio.Play();
                }
            }

            if (lvlPresent.activeInHierarchy == false)
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

                    JimmyRow jimmyRow = GameObject.Find("JimmyIddle").GetComponent<JimmyRow>();
                    jimmyRow.CanRow = true;
                    JimmyJump jimmyJump = GameObject.Find("JimmyIddle").GetComponent<JimmyJump>();
                    jimmyJump.CanJumpInit = true;

                    OnRowBots();
                    raceTime += Time.deltaTime;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        GameObject collisioned = other.gameObject;
        finishing.Play();

        if (collisioned.name == "P1")
        {
            sharedTime = raceTime;
            mScoreManager.ChangeScore("P1", "Time", sharedTime);
            i++;

            if (mScene.name == "GoddyLevel")
            {
                SceneManager.LoadScene("Menu");
            }

        }
        if (collisioned.name == "JimmyIddle")
        {
            sharedTime = raceTime;
            mScoreManager.ChangeScore("Jimmy", "Time", sharedTime);
            i++;
            OnJimmyFinish();

            if (mScene.name == "GoddyLevel")
            {
                goddy.gameObject.SetActive(false);
                goddyText.gameObject.SetActive(true);
            }
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
            OnEnemiesFinish();
            scoreBoard.gameObject.SetActive(true);
        }
    }

}
