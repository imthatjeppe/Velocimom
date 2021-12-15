using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public float score;
    
    public Text totalScore;
    public Text scoreNeeded;

    void Update()
    {
        totalScore.text = "Score: " + score;
    }
    
    public void AddScore(float points)
    {
        score += points;
    }
}