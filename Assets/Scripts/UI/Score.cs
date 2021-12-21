using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Score : MonoBehaviour
{
    public float score;
    public Text totalScore;

    void Update()
    {
        totalScore.text = "Score: " + score;
    }
    
    public void AddScore(float points)
    {
        score += points;
    }

    public void ScoreBounce()
    {
        totalScore.transform.DOPunchScale(new Vector3(0f, 0.3f, 0f), 1f, 10, 0f);
    }
}