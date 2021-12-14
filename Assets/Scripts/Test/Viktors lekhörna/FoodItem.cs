using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public float points;
    public float weight;
    public Inventory inventory;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("DropOff"))
        {
                
            GameObject.FindGameObjectWithTag("DropOff").GetComponent<Score>().AddScore(points);
            Destroy(gameObject);
        }
    }
}
