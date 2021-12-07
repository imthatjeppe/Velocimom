using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public int timeLeft;
 
    private float timer;

    private NextLevelScript nextLevelScriptObj;
    private Score scoreRef;

    private void Start()
    {
        nextLevelScriptObj = gameObject.GetComponent<NextLevelScript>();
        scoreRef = gameObject.GetComponent<Score>();
        timer = timeLeft;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = "Time left: " + (int)timer;

        TimeRunOut();
    }

    public void TimeRunOut()
    {
        if (timer <= 0)
        {
            if (scoreRef.score >= 1000)
            {
                Debug.Log("Win");
            }

            if (scoreRef.score <= 1000)
            {
                Debug.Log("Lose");
            }
        }
    }

}
