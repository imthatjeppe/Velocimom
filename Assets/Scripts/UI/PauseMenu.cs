using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    
    public static bool GameIsPaused;

    PlayerInteractions playerInteraction;
    private void Start()
    {
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteractions>();
        PauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !playerInteraction.GetInteracting())
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameObject.Find("Finger1").SetActive(true);
        GameObject.Find("Finger2").SetActive(true);
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        GameObject.Find("Finger1").SetActive(false);
        GameObject.Find("Finger2").SetActive(false);
    }

}
