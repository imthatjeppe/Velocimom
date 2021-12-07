using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public float score;
    public Text foodScore;

    void Update()
    {
        foodScore.text = "Score: " + score;

        //Admin Commands
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            score += 10000;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerMovement.playerHealth--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayerMovement.playerHealth++;
        }

    }

    public void AddScore(float points)
    {
        score += points;
    }
}
