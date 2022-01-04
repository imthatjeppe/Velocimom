using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static int playerHealth;
    public bool Death1Bool, Death2Bool;

    public GameObject Heart0, Heart1, Heart2;
    public GameObject Death1, Death2, Death3;


    void Start()
    {
        Death1Bool = false;
        Death2Bool = false;

        playerHealth = 3;

        Heart0.gameObject.SetActive(true);
        Heart1.gameObject.SetActive(true);
        Heart2.gameObject.SetActive(true);
    }

    void Update()
    {
        if (playerHealth <= 0)
        {
            GameOver();
        }

        SetHealth();
    }

    public void GameOver()
    {
        Death3.SetActive(true);
        Object.Destroy(Death3, 2.0f);
        Time.timeScale = 0f;
    }

    public void SetHealth()
    {
        if (playerHealth > 3)
        {
            playerHealth = 3;
        }

        Heart0.gameObject.SetActive(false);
        Heart1.gameObject.SetActive(false);
        Heart2.gameObject.SetActive(false);

        if (playerHealth > 0)
        {
            Heart0.gameObject.SetActive(true);
        }

        if (playerHealth > 1)
        {
            Heart1.gameObject.SetActive(true);

        }

        if (playerHealth > 2)
        {
            Heart2.gameObject.SetActive(true);
        }

            if (!Death1Bool)
            {
                if (playerHealth == 2)
                {
                    Death1.SetActive(true);
                    Object.Destroy(Death1, 2.0f);
                    Death1Bool = true;
                }
            }

       
            if (!Death2Bool)
            {
                if (playerHealth == 1)
                {
                    Death2.SetActive(true);
                    Object.Destroy(Death2, 2.0f);
                    Death2Bool = true;
                }
            }

    }
}
