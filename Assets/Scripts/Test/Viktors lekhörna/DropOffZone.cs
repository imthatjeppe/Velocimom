using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffZone : MonoBehaviour
{
    public FoodItem food;

    private void Start()
    {
        food = GameObject.FindGameObjectWithTag("Food").GetComponent<FoodItem>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Debug.Log("food collided");
            food.AddScore();
        }
    }


}
