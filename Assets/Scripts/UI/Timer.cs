using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Timer : MonoBehaviour
{
    public GameObject player;
    public Text timerText;
    public int timeLeft;
    public bool isTimeTicking = true;

    private bool timeToVFX;
    private float timer;
    private Vector2 timerStartPos;
    private PlayerHealth playerHealth;
    private NextLevelScript nextLevelScriptObj;
    private Score scoreRef;

    private Color32 greyColor = new Color32(50, 50, 50, 255);
    private Color32 redColor = new Color32(255, 0, 0, 255);

    private void Start()
    {
        timerStartPos = timerText.transform.position;
        playerHealth = player.GetComponent<PlayerHealth>();
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
        CheckIfTimeToVFX();
        TimeRunOut();

        if (Input.GetKey(KeyCode.K))
        {
            TimerVFX();
        }
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
                playerHealth.GameOver();
            }
        }
    }

    private void CheckIfTimeToVFX()
    {
        if (timer > 30.75f && timer < 31 || timer <= timeLeft / 2 && timer >= (timeLeft / 2) - 0.25f)
        {
            timeToVFX = true;

            if (timeToVFX)
            {
                TimerVFX();
                timeToVFX = false;
            }
        }
    }

    private void TimerVFX()
    {
        timerText.DOColor(redColor, 1f);
        timerText.transform.DOShakePosition(1f, 2.5f, 10, 45f, true, false);
        Invoke(nameof(resetTimerVFX), 1.25f);
    }

    private void resetTimerVFX()
    {
        timerText.DOColor(greyColor, 1f);
        timerText.transform.position = timerStartPos;
    }
}
