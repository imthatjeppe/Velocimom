using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseFoodItem : MonoBehaviour
{
    public Material outline;
    public Material noOutline;

    SearchFood searchFood;
    Image[] bubbles;
    Inventory inventory;
    FridgeAudioHandler audioHandler;
    List<int> alreadyPickedPos;
    int atBubblePos = 0;
    int previousBubblePos = 0;
    public TextMeshProUGUI text;

    void Start()
    {
       // text = GameObject.Find("SearchingText").GetComponent<TextMeshProUGUI>();
        text.text = "Searching...";
        alreadyPickedPos = new List<int>();
        audioHandler = GetComponent<FridgeAudioHandler>();
        searchFood = GetComponent<SearchFood>();
        bubbles = new Image[searchFood.bubbles.Length];
        int i = 0;
        foreach(GameObject bubble in searchFood.bubbles)
        {
            bubbles[i] = bubble.GetComponent<Image>();
            i++;
        }
        searchFood.bubbles[0].GetComponent<Image>().material = outline;

        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    void Update()
    {
        GetInput();
        if (Input.GetKeyDown(KeyCode.E) && inventory.inventoryCount < inventory.inventoryMax)
        {
            AddFoodItemToInventory(searchFood.GetFoodItemsDictionaryAtPos(atBubblePos));
            PlayBubblePopAnimation();
            audioHandler.PlayBubblePopSFX();
            DeactivateRarityOverlayUI();
            alreadyPickedPos.Add(atBubblePos);
            atBubblePos++;
            ChangeOutlineMaterials();
            if(alreadyPickedPos.Count == 8)
            {
                text.text = "Empty";
            }
        }
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
                    CheckAlreadyPickedPosLeft();
                }
                else if(atBubblePos >= 4){
                    atBubblePos++;
                    CheckAlreadyPickedPosRight();
                }
                break;
            case SelectionInput.Up:
                {
                    if(atBubblePos >= 6 || atBubblePos <= 1)
                    {
                        atBubblePos++;
                        CheckAlreadyPickedPosRight();
                    }
                    else
                    {
                        atBubblePos--;
                        CheckAlreadyPickedPosLeft();
                    }
                }
                break;
            case SelectionInput.Right:
                if (atBubblePos <= 3)
                {
                    atBubblePos++;
                    CheckAlreadyPickedPosRight();
                }
                else if (atBubblePos >= 4)
                {
                    atBubblePos--;
                    CheckAlreadyPickedPosLeft();
                }
                break;
            case SelectionInput.Down:
                if (atBubblePos >= 6 || atBubblePos <= 1)
                {
                    atBubblePos--;
                    CheckAlreadyPickedPosLeft();
                }
                else
                {
                    atBubblePos++;
                    CheckAlreadyPickedPosRight();
                }
                break;
        }
        CheckBubblePositionBoundries();
        ChangeOutlineMaterials();


    }
    enum SelectionInput
    {
        Left,
        Up,
        Right,
        Down,
    }
    void CheckBubblePositionBoundries()
    {
        if(atBubblePos < 0)
        {
            atBubblePos = bubbles.Length -1;
        }else if(atBubblePos > bubbles.Length-1)
        {
            atBubblePos = 0;
        }
    }
    void AddFoodItemToInventory(GameObject foodItem)
    {
        if(foodItem != null)
            inventory.AddItem(foodItem);
    }
    void PlayBubblePopAnimation()
    {
        searchFood.bubbles[atBubblePos].GetComponent<Animator>().SetBool("PopBubble", true);
    }
    void DeactivateRarityOverlayUI()
    {
        searchFood.bubbles[atBubblePos].transform.GetChild(1).GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }
    void ChangeOutlineMaterials()
    {
        if (bubbles[atBubblePos].material != outline)
        {
            bubbles[atBubblePos].material = outline;
        }

        if (previousBubblePos != atBubblePos)
        {
            bubbles[previousBubblePos].material = noOutline;
            previousBubblePos = atBubblePos;
        }
    }
    void CheckAlreadyPickedPosLeft()
    {
        foreach (int pickedPos in alreadyPickedPos)
        {
            if (pickedPos == atBubblePos)
                atBubblePos--;
        }
    }
    void CheckAlreadyPickedPosRight()
    {
        foreach (int pickedPos in alreadyPickedPos)
        {
            if (pickedPos == atBubblePos)
                atBubblePos++;
        }
    }
}
