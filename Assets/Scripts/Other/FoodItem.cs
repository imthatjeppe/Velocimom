using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    [Header("Float Variables")]
    public float points;
    public float weight;

    [Header("Bools")]
    public bool normalQuality;
    public bool glitterQuality;
    public bool shinyRareQuality;
    public bool hasBeenDropped = false;
    public bool isPickupable = true;


    private float timerToPickupable;
    private float timerReset = 1.75f;

    private void Start()
    {
        if (normalQuality)
        {
            return;
        }
        else if (glitterQuality)
        {
            points *= 1.25f;
        }
        else if (shinyRareQuality)
        {
            points *= 2f;
        }

        timerToPickupable = timerReset;
    }

    private void Update()
    {
        if (hasBeenDropped)
        {
            isPickupable = false;
            timerToPickupable -= Time.deltaTime;

            if (timerToPickupable <= 0)
            {
                isPickupable = true;
                hasBeenDropped = false;
                timerToPickupable = timerReset;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DropOff"))
        {
            Debug.Log("I got destroyed by dropoffzone!");
            GameObject.FindGameObjectWithTag("DropOff").GetComponent<Score>().AddScore(points);
            Destroy(gameObject);
        }
    }

}
