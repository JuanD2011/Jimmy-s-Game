using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string levelToLoad = " ";


    public void Play()
    {
        SceneManager.LoadScene(levelToLoad);
    }

}
