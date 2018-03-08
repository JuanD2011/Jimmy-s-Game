using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class FinalAnim : MonoBehaviour
{
    VideoPlayer animFinal;
    float t = 0f;

	void Start ()
    {
        animFinal = GameObject.Find("FinalAnimation").GetComponent<VideoPlayer>();
    }

    private void Update()
    {
        t += Time.deltaTime;
        Debug.Log(t);

        if (t > 25f)
        {
            SceneManager.LoadScene("Menu");
        }
    }

}
