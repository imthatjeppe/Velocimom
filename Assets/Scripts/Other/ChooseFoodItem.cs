using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseFoodItem : MonoBehaviour
{
    public Material outline;
    public Material noOutline;

    SearchFood searchFood;
    int atBubblePos = 0;
    void Start()
    {
        searchFood = GetComponent<SearchFood>();

        searchFood.bubbles[0].GetComponent<Image>().material = outline;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void MoveSelection()
    {

    }
}
