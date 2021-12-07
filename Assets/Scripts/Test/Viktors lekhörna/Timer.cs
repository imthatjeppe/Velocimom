using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    public GameObject dropOffZone;
    public Text timerText;
    public int timeLeft;

    private float timer;
    private Score score;

    private void Start()
    {
        score = dropOffZone.GetComponent<Score>();

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
            
        }
    }

}
