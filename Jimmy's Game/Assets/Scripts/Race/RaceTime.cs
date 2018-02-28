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
    GameObject goText;
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

    //PlayerCount
    int playerCount = 0;
   
    //Events 

    public delegate void ColliderDeactivated();
    public static event ColliderDeactivated OnRowBots;

    public delegate void FinishLine();
    public static event FinishLine OnJimmyFinish;

    public delegate void EnemiesFinish();
    public static event EnemiesFinish OnEnemiesFinish;


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


    private void Start()
    {
        gameOverBg = GameObject.Find("GameOverBG");
        gameOverBg.gameObject.SetActive(false);

        mScene = SceneManager.GetActiveScene();
        mScoreManager = GetComponent<ScoreManager>();

        scoreBoard = GameObject.Find("CanvasScoreBoard");
        scoreBoard.gameObject.SetActive(false);

        lvlPresent = GameObject.Find("LevelPresentation");

        countDown = GameObject.Find("TextCountDown");
        countDown.gameObject.SetActive(false);

        goText = GameObject.Find("TextGO");
        goText.gameObject.SetActive(false);

        countDownAudio = GameObject.Find("CountDownAudio").GetComponent<AudioSource>();
        finishing = GameObject.Find("Finishing").GetComponent<AudioSource>();
        music = GameObject.Find("Music").GetComponent<AudioSource>();
        lose = GameObject.Find("Lose").GetComponent<AudioSource>();
        
        PlayerScoreList.OnGameOver += GameOver;
        PlayerScoreList.OnWinLevel += WinLevel;
    }

    private void Update()
    {
        TimesUp();
        AcitvateTheGOImage();
        NextLevel();
    }

    public void TutorialComplete()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            audioCount += 1;

            if (audioCount == 1)
            {
                lvlPresent.gameObject.SetActive(false);
            }

            if (audioCount == 2)
            {
                countDownAudio.Play();
                music.Play();
            }
        }
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
                timerText.gameObject.SetActive(false);

                JimmyRow jimmyRow = GameObject.Find("JimmyIddle").GetComponent<JimmyRow>();
                jimmyRow.CanRow = true;
                JimmyJump jimmyJump = GameObject.Find("JimmyIddle").GetComponent<JimmyJump>();
                jimmyJump.CanJumpInit = true;

                OnRowBots();
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
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space))
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
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space))
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
                    SceneManager.LoadScene("Menu");
                }
            }
        }
    }

}
