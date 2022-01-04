using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFoodItem : MonoBehaviour
{
    private GameObject itemDropper;
    private GameObject foodItemCanvas;

    void Start()
    {
        foodItemCanvas = GameObject.Find("FoodItemCanvas");
        itemDropper = GameObject.FindGameObjectWithTag("ItemDropper");
        transform.SetParent(foodItemCanvas.transform, true);
        transform.position = itemDropper.transform.position;
    }
}