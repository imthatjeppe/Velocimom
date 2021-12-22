using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseFoodItem : MonoBehaviour
{
    public Material outline;
    public Material noOutline;

    SearchFood searchFood;
    Image[] bubbles;
    int atBubblePos = 0;
    int previousbubblePos = 0;
    void Start()
    {
        searchFood = GetComponent<SearchFood>();
        bubbles = new Image[searchFood.bubbles.Length];
        int i = 0;
        foreach(GameObject bubble in searchFood.bubbles)
        {
            bubbles[i] = bubble.GetComponent<Image>();
            i++;
        }

        searchFood.bubbles[0].GetComponent<Image>().material = outline;
    }

    void Update()
    {
        GetInput();
    }
    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveSelection(SelectionInput.Left);
        }else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveSelection(SelectionInput.Up);
        }else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveSelection(SelectionInput.Right);
        }else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveSelection(SelectionInput.Down);
        }
    }
    void MoveSelection(SelectionInput input)
    {
        switch (input)
        {
            case SelectionInput.Left:
                if(atBubblePos <= 3)
                {
                    atBubblePos--;
                }
                else if(atBubblePos >= 4){
                    atBubblePos++;
                }
                break;
            case SelectionInput.Up:
                {
                    if(atBubblePos >= 6 || atBubblePos <= 1)
                    {
                        atBubblePos++;
                        
                    }
                    else
                    {
                        atBubblePos--;
                    }
                }
                break;
            case SelectionInput.Right:
                if (atBubblePos <= 3)
                {
                    atBubblePos++;
                }
                else if (atBubblePos >= 4)
                {
                    atBubblePos--;
                }
                break;
            case SelectionInput.Down:
                if (atBubblePos >= 6 || atBubblePos <= 1)
                {
                    atBubblePos--;
                }
                else
                {
                    atBubblePos++;
                }
                break;
        }
        CheckBubblePositonBoundries();
        if (bubbles[atBubblePos].material != outline)
        {
            bubbles[atBubblePos].material = outline;
        }

        if(previousbubblePos != atBubblePos)
        {
            bubbles[previousbubblePos].material = noOutline;
            previousbubblePos = atBubblePos;
        }


    }
    enum SelectionInput
    {
        Left,
        Up,
        Right,
        Down,
    }
    void CheckBubblePositonBoundries()
    {
        if(atBubblePos < 0)
        {
            atBubblePos = bubbles.Length -1;
        }else if(atBubblePos > bubbles.Length-1)
        {
            atBubblePos = 0;
        }
    }
    void SelectFoodItem()
    {

    }
    void AddFoodItemToInventory()
    {

    }
}
