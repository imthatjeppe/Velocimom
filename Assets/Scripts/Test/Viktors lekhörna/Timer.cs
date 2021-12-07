using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public int timeLeft;
    public Text timerText;
 
    private float timer;

    private void Start()
    {
        timer = timeLeft;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        timerText.text = "Time left: " + timer;
    }

    public void TimeRunOut() 
    {
        if (timer <= 0)
        {
            Debug.Log("boo");
        }
    }

}
