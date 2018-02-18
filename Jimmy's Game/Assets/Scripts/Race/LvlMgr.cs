using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlMgr : MonoBehaviour
{

    Scene mScene;

    private void Start()
    {
        mScene = SceneManager.GetActiveScene();
        PlayerScoreList.OnWin += ChangeWinLevel;
        PlayerScoreList.OnLoose += ChangeLooseLevel;

    }

    private void Update()
    {
    }

    public void ChangeLooseLevel()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Fui cambiao de level");
            SceneManager.LoadScene("Menu");
        }
    }

    public void ChangeWinLevel()
    {
        Debug.Log("Yeiii");
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Fui cambiao de level");
            SceneManager.LoadScene("Milkyway");
        }
    }

}
