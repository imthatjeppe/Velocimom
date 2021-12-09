using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public GameObject player;
    public Text timerText;
    public int timeLeft;
    public bool isTimeTicking = true;

    private float timer;
    private PlayerMovement playerMove;
    private NextLevelScript nextLevelScriptObj;
    private Score scoreRef;

    private void Start()
    {
        playerMove = player.GetComponent<PlayerMovement>();
        nextLevelScriptObj = gameObject.GetComponent<NextLevelScript>();
        scoreRef = gameObject.GetComponent<Score>();
        timer = timeLeft;
    }

    private void Update()
    {
        if (isTimeTicking)
        {
            timer -= Time.deltaTime;
        }

        timerText.text = "Time left: " + (int)timer;
        TimeRunOut();
    }

    public void TimeRunOut()
    {
        if (timer <= 0)
        {
            if (scoreRef.score >= nextLevelScriptObj.scoreForNextLevel)
            {
                nextLevelScriptObj.ContinueToNextLevel();
            }

            if (scoreRef.score < nextLevelScriptObj.scoreForNextLevel)
            {
                playerMove.GameOver();
            }
        }
    }
}
