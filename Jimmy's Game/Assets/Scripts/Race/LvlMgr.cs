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

    }

    private void Update()
    {
    }

    public void ChangeWinLevel()
    {
        if (mScene.name == "Hood Level 1")
        {
            SceneManager.LoadScene("WorldLevel");
        }
        if (mScene.name == "WorldLevel")
        {
            SceneManager.LoadScene("Milkyway");
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
