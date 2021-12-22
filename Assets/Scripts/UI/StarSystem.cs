using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystem : MonoBehaviour
{
    public GameObject star, star2, star3;
    public float star1Score, star2Score, star3Score;

    private Score scoreNeeded;
    private NextLevelScript nextLevelScript;

    // Start is called before the first frame update
    void Start()
    {
        scoreNeeded = GetComponent<Score>();

        star.gameObject.SetActive(false);
        star2.gameObject.SetActive(false);
        star3.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        starscore();
    }

    public void starscore()
    {
        if (scoreNeeded.score >= star1Score)
        {
            star.gameObject.SetActive(true);
        }
        if (scoreNeeded.score >= star2Score)
        {
            star2.gameObject.SetActive(true);
        }
        if (scoreNeeded.score >= star3Score)
        {
            star3.gameObject.SetActive(true);
        }
    }
}
