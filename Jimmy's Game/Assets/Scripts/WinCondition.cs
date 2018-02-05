using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    public float t = 0f;
    public float time = 0f;

    Dictionary<string, float> playerScores;

    private void Start()
    {
        
    }
    private void Update()
    {
        t += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collisioned = other.gameObject;

        if (collisioned.name == "P1")
        {
            time = t;
        }
    }
}
