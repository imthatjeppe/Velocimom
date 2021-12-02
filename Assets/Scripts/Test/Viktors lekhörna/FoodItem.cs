using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public float points;
    public float weight;

    private Score score;

    private void Start()
    {
        score = GetComponent<Score>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DropOff"))
        {
            Debug.Log("food dropped off");
            GameObject.FindGameObjectWithTag("DropOff").GetComponent<Score>().AddScore(points);
        }
    }
}
