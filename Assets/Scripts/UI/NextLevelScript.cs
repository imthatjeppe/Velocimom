using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class NextLevelScript : MonoBehaviour
{
    public float scoreForNextLevel;
    public GameObject NextLevel;
    public Text scoreNeeded;

    public static bool NextLevelPaused;


    private Score nextLevelScore;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        NextLevel.SetActive(false);
        nextLevelScore = GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreNeeded.text = "Score Needed for next level: " + scoreForNextLevel;

        if (nextLevelScore.score >= scoreForNextLevel)
        {
            ContinueToNextLevel();
        }

        
        
    }

    void ContinueToNextLevel()
    {
        NextLevel.SetActive(true);
        NextLevelPaused = (true);
        
        

    }
    public void startNextLevel()
    {
        NextLevel.SetActive(false);
        NextLevelPaused = (false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;

    }
}
