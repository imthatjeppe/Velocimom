using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class NextLevelScript : MonoBehaviour
{
    public float scoreForNextLevel;

    public bool NextLevelPaused;

    public GameObject NextLevel;
    public Text scoreNeeded;

    private Score nextLevelScore;
    private StarSystem stars;

    void Start()
    {
        Time.timeScale = 1f;
        NextLevel.SetActive(false);
        nextLevelScore = GetComponent<Score>();
    }

    void Update()
    {
        scoreNeeded.text = "Score Needed for next level: " + scoreForNextLevel;
    }

    public void ContinueToNextLevel()
    {
        NextLevel.SetActive(true);
        NextLevelPaused = (true);
       
    }

    public void startNextLevel()
    {
        NextLevel.SetActive(false);
        NextLevelPaused = (false);

        //TODO: Change to next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
