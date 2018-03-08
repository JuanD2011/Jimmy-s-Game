using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceTime : MonoBehaviour
{
    //Race time
    GameObject countDown;
    GameObject goText;
    public Text timerText;
    public float timeCountDown = 3f;
    public float raceTime = 0f;
    public float sharedTime = 0f;

    GameObject lvlPresent;

    //Score
    ScoreManager mScoreManager;
    GameObject scoreBoard;

    //PlayerCount
    int playerCount = 0;
   
    //Events 

    public delegate void ColliderDeactivated();
    public static event ColliderDeactivated OnRowBots;

    public delegate void FinishLine();
    public static event FinishLine OnJimmyFinish;

    public delegate void EnemiesFinish();
    public static event EnemiesFinish OnEnemiesFinish;

    public delegate void RaceStart();
    public static event RaceStart OnRaceStart;


    //Scenes
    Scene mScene;
    int sceneCount = 0;

    //Audio
    AudioSource countDownAudio;
    AudioSource finishing;
    AudioSource music;
    AudioSource lose;

    int audioCount = 0;

    bool canvasGOActive = false;
    bool wonLevel = false;
    GameObject gameOverBg;

    GameObject pressSpacebar;

    int timesUpCount;

    private void Start()
    {
        goText = GameObject.Find("TextGO");
        goText.gameObject.SetActive(false);

        countDown = GameObject.Find("TextCountDown");
        countDown.gameObject.SetActive(false);

        pressSpacebar = GameObject.Find("PressSpacebarToContinue");
        pressSpacebar.gameObject.SetActive(false);

        mScene = SceneManager.GetActiveScene();
        lvlPresent = GameObject.Find("LevelPresentation");

        gameOverBg = GameObject.Find("GameOverBG");
        gameOverBg.gameObject.SetActive(false);
        
        mScoreManager = GetComponent<ScoreManager>();

        scoreBoard = GameObject.Find("CanvasScoreBoard");
        scoreBoard.gameObject.SetActive(false);

        countDownAudio = GameObject.Find("CountDownAudio").GetComponent<AudioSource>();
        finishing = GameObject.Find("Finishing").GetComponent<AudioSource>();
        music = GameObject.Find("Music").GetComponent<AudioSource>();
        lose = GameObject.Find("Lose").GetComponent<AudioSource>();
        
        PlayerScoreList.OnGameOver += GameOver;
        PlayerScoreList.OnWinLevel += WinLevel;

        timesUpCount = 0;
    }

    private void Update()
    {
        TimesUp();
        AcitvateTheGOImage();
        NextLevel();
    }

    public void TimesUp()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            audioCount += 1;
            lvlPresent.gameObject.SetActive(false);

            if (audioCount <= 1)
            {
                countDownAudio.Play();
                music.Play();
            }
        }

        if (lvlPresent.activeInHierarchy == false)
        {
            countDown.gameObject.SetActive(true);
            if (timeCountDown < 4f && timeCountDown > 0f)
            {
                timeCountDown -= Time.deltaTime;
                timerText.text = timeCountDown.ToString("0");
            }
            if (timeCountDown < 0.5f)
            {
                timesUpCount += 1;

                timerText.gameObject.SetActive(false);

                if (timesUpCount == 1)
                {
                    OnRaceStart();
                    OnRowBots();
                }

                raceTime += Time.deltaTime;
            }
            if (timeCountDown < 0.5f && timeCountDown > 0f)
            {
                goText.gameObject.SetActive(true);
            }
            else
            {
                goText.gameObject.SetActive(false);
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
            playerCount++;

        }
        if (collisioned.name == "JimmyIddle")
        {
            sharedTime = raceTime;
            mScoreManager.ChangeScore("Jimmy", "Time", sharedTime);
            playerCount++;
            OnJimmyFinish();
        }
        if (collisioned.name == "P2")
        {
            sharedTime = raceTime;
            mScoreManager.ChangeScore("P2", "Time", sharedTime);
            playerCount++;
        }
        if (collisioned.name == "P3")
        {
            sharedTime = raceTime;
            mScoreManager.ChangeScore("P3", "Time", sharedTime);
            playerCount++;
        }
        if (collisioned.name == "P5")
        {
            sharedTime = raceTime;
            mScoreManager.ChangeScore("P5", "Time", sharedTime);
            playerCount++;
        }
        if (collisioned.name == "P6")
        {
            sharedTime = raceTime;
            mScoreManager.ChangeScore("P6", "Time", sharedTime);
            playerCount++;
        }
        if (playerCount == 6)
        {
            OnEnemiesFinish();
            scoreBoard.gameObject.SetActive(true);
            pressSpacebar.gameObject.SetActive(true);
            music.Stop();
        }
    }

    public void GameOver()
    {
        canvasGOActive = true;
    }

    public void WinLevel()
    {
        wonLevel = true;
    }

    public void AcitvateTheGOImage()
    {
        if (canvasGOActive == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                sceneCount ++;
                gameOverBg.gameObject.SetActive(true);
                if (sceneCount == 1)
                {
                    lose.Play();
                }
                if (sceneCount == 2)
                {
                    SceneManager.LoadScene("Menu");
                }
            }
        }
    }

    public void NextLevel()
    {
        if (wonLevel == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (mScene.name == "HoodLevel")
                {
                    SceneManager.LoadScene("NationalLevel");
                }
                if (mScene.name == "NationalLevel")
                {
                    SceneManager.LoadScene("WorldLevel");
                }
                if (mScene.name == "WorldLevel")
                {
                    SceneManager.LoadScene("Milkyway");
                }
                if (mScene.name == "Milkyway")
                {
                    SceneManager.LoadScene("AnimFinal");
                }
            }
        }
    }

}
