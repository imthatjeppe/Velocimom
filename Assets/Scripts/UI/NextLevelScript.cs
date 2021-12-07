using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevelScript : MonoBehaviour
{
    public GameObject NextLevel;

    public static bool NextLevelPaused;

    private Score nextLevelScore;
    // Start is called before the first frame update
    void Start()
    {
        NextLevel.SetActive(false);
        nextLevelScore = GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nextLevelScore.score >= 1000)
        {
            ContinueToNextLevel();
        }
    }

    void ContinueToNextLevel()
    {
        NextLevel.SetActive(true);
        Time.timeScale = 0f;
        NextLevelPaused = (true);

    }
    public void startNextLevel()
    {
        NextLevel.SetActive(false);
        Time.timeScale = 1f;
        NextLevelPaused = (false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
