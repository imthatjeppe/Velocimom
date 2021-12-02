using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public int points;
    public float weight;

    //public Score ScoreScript;

    private void Start()
    {
        //ScoreScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Score>();
    }
    public void AddScore()
    {
        //ScoreScript.score += points;
    }          
}
