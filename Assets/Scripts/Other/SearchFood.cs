using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchFood : MonoBehaviour, IInteractable
{
    public GameObject[] bubbles;
    public GameObject searchingForFoodPanel;
    public GameObject[] foodItems;
    public int glitterRank2ChanceProcentage;
    public int shinyRareRank3ChanceProcentage;


    PlayerMovement playerMovement;
    Animator[] rarityOverlayAnimators;
    Image[] foodItemImages;
    Image[] overlayUIImages;
    private int[] rarityRankAtPos;
    //Gameobjects in Dictonary<bubble, fooditem>
    Dictionary<GameObject,GameObject> bubbleFoodDic;
    ChooseFoodItem chooseFood;
    private int atBubblePosInList = 0;
    private int alreadyCheckedPos = 0;
    private bool interacting = false;
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        bubbleFoodDic = new Dictionary<GameObject, GameObject>();

        chooseFood = GetComponent<ChooseFoodItem>();
        chooseFood.enabled = false;

        rarityOverlayAnimators = new Animator[bubbles.Length];
        foodItemImages = new Image[bubbles.Length];
        overlayUIImages = new Image[bubbles.Length];
        rarityRankAtPos = new int[bubbles.Length];

        for (int i = 0; i < bubbles.Length; i++)
        {
            foodItemImages[i] = bubbles[i].transform.GetChild(0).GetComponent<Image>();
            rarityOverlayAnimators[i] = bubbles[i].transform.GetChild(1).GetComponent<Animator>();
            overlayUIImages[i] = bubbles[i].transform.GetChild(1).GetComponent<Image>();
            rarityRankAtPos[i] = 1;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && searchingForFoodPanel.activeSelf)
        {
            
            // Setting interacting false in delay so that Pause menu doesnt pop up when clicking escape
            Invoke(nameof(SetInteractingFalse), 0.5f);
            chooseFood.enabled = false;
            playerMovement.speed = playerMovement.maxSpeed;
            CancelInvoke(nameof(SearchingFridge));
            ResetSearchUI();
        }
    }
    public void Interact()
    {
        interacting = true;

        //Only activte chooseFood script if you are searching for food
        chooseFood.enabled = true;
        searchingForFoodPanel.SetActive(true);
        playerMovement.speed = 0;
        InvokeRepeating(nameof(SearchingFridge),0,1);
    }
    void SearchingFridge()
    {
        //if already searched and found items they appear immediately
        if (atBubblePosInList < alreadyCheckedPos)
        {
            for (int i = 0; i < alreadyCheckedPos; i++)
            {
                bubbles[atBubblePosInList].SetActive(true);
                if (rarityRankAtPos[atBubblePosInList] == 2)
                {
                    CheckIfAnimatorIsActive(atBubblePosInList);
                    SetRarityOverlayAnimation("IsGlitterRank2", true, atBubblePosInList);
                }
                else if (rarityRankAtPos[atBubblePosInList] == 3)
                {
                    CheckIfAnimatorIsActive(atBubblePosInList);
                    SetRarityOverlayAnimation("IsShinyRareRank3", true, atBubblePosInList);
                }
                atBubblePosInList++;
            }
        }

        GameObject randomFoodItem;
        //goes through all the UI and activates them and giving them a random food item
        if (atBubblePosInList <= bubbles.Length - 1)
        {
            bubbles[atBubblePosInList].SetActive(true);
            if (!bubbleFoodDic.ContainsKey(bubbles[atBubblePosInList]))
            {
                randomFoodItem = GetRandomFoodItem();
                bubbleFoodDic.Add(bubbles[atBubblePosInList], randomFoodItem);
                foodItemImages[atBubblePosInList].sprite = randomFoodItem.GetComponent<SpriteRenderer>().sprite;

                CalculateRarityChance();
                atBubblePosInList++;
            }
            else
            {
                CancelInvoke(nameof(SearchingFridge));
            }
        }

        if (atBubblePosInList > alreadyCheckedPos)
            alreadyCheckedPos = atBubblePosInList;
    }
    void ResetSearchUI()
    {
        foreach(GameObject bubble in bubbles)
        {
            bubble.transform.GetChild(0).gameObject.SetActive(false);
            bubble.transform.GetChild(1).GetComponent<Image>().enabled = false;
            bubble.transform.GetChild(1).GetComponent<Animator>().enabled = false;
            bubble.transform.GetChild(1).gameObject.SetActive(false);
            bubble.SetActive(false);
        }
        Debug.Log("Resetting search UI");
        searchingForFoodPanel.SetActive(false);
        atBubblePosInList = 0;
        
    }
    void CalculateRarityChance()
    {
        int procentageCalc = Random.Range(1, 101);
        CheckIfAnimatorIsActive(atBubblePosInList);
            if (procentageCalc <= shinyRareRank3ChanceProcentage)
            {
                SetRarityOverlayAnimation("IsShinyRareRank3", true, atBubblePosInList);
                CorrectShinyRareRank3UIPosition(atBubblePosInList);
                bubbleFoodDic[bubbles[atBubblePosInList]].GetComponent<FoodItem>().shinyRareQuality = true;
                bubbleFoodDic[bubbles[atBubblePosInList]].GetComponent<FoodItem>().normalQuality = false;
                rarityRankAtPos[atBubblePosInList] = 3;
            }
            else if (procentageCalc <= glitterRank2ChanceProcentage)
            {
                SetRarityOverlayAnimation("IsGlitterRank2", true, atBubblePosInList);
                bubbleFoodDic[bubbles[atBubblePosInList]].GetComponent<FoodItem>().glitterQuality = true;
                bubbleFoodDic[bubbles[atBubblePosInList]].GetComponent<FoodItem>().normalQuality = false;
                rarityRankAtPos[atBubblePosInList] = 2;
        }
    }
    void CheckIfAnimatorIsActive(int atPos)
    {
        if (!rarityOverlayAnimators[atPos].gameObject.activeSelf)
        {
            rarityOverlayAnimators[atPos].gameObject.SetActive(true);
        }
    }
    void SetRarityOverlayAnimation(string boolName, bool activeAnimation, int atPos)
    {
       
        rarityOverlayAnimators[atPos].SetBool(boolName, activeAnimation);

       
        Debug.Log(boolName);
    }
    void CorrectShinyRareRank3UIPosition(int atPos)
    {
        RectTransform thisRectTransform = bubbles[atPos].transform.GetChild(1).GetComponent<RectTransform>();
        thisRectTransform.sizeDelta = new Vector2(1256f, 1276f);
        thisRectTransform.position = new Vector3(thisRectTransform.position.x - 1f, thisRectTransform.position.y + 2.5f, 0);
    }
    GameObject GetRandomFoodItem()
    {
        GameObject randomFoodItem = Instantiate(foodItems[Random.Range(0, foodItems.Length)]);
        return randomFoodItem;
    }

    public bool isInteracting()
    {
        return interacting;
    }
    void SetInteractingFalse()
    {
        interacting = false;
    }
    //This is used when selecting food items to add them to the inventroy
    public GameObject GetFoodItemsDictionaryAtPos(int atPos)
    {
        GameObject foodItem = bubbleFoodDic[bubbles[atPos]];
        RemoveFoodItem(atPos);
        return foodItem;
    }
    void RemoveFoodItem(int atPos)
    {
        foodItemImages[atPos].color = new Color(0, 0, 0, 0);
        bubbleFoodDic.Remove(bubbleFoodDic[bubbles[atPos]]);
    }
}
