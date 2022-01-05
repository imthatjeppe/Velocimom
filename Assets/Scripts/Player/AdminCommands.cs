using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdminCommands : MonoBehaviour
{
    Score scoreHandler;

    // Start is called before the first frame update
    void Start()
    {
        scoreHandler = GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            scoreHandler.score += 500;
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
            PlayerHealth.playerHealth--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayerHealth.playerHealth++;
        }
    }
}
