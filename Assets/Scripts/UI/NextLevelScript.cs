using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevelScript : MonoBehaviour
{
    public float scoreForNextLevel;
    public GameObject NextLevel;

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
