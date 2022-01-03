using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class NextLevelScript : MonoBehaviour
{
    public float scoreForNextLevel;

    public bool NextLevelPaused;

    public GameObject hands1, hands2;
    public GameObject NextLevel;
    public Text scoreNeeded;

    private Score nextLevelScore;
    private StarSystem stars;

    void Start()
    {
        Time.timeScale = 1f;
        NextLevel.SetActive(false);
        nextLevelScore = GetComponent<Score>();
        hands1.SetActive(true);
        hands2.SetActive(true);
    }

    void Update()
    {
        scoreNeeded.text = "Score Needed for next level: " + scoreForNextLevel;
    }

    public void ContinueToNextLevel()
    {
        NextLevel.SetActive(true);
        NextLevelPaused = (true);
        hands1.SetActive(false);
        hands2.SetActive(false);
        Time.timeScale = 0f;
       
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
