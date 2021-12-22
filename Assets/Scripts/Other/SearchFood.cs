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
    int[] rarityRankAtPos;
    //Gameobjects in Dictonary<bubble, fooditem>
    Dictionary<GameObject,GameObject> bubbleFoodDic;
    ChooseFoodItem chooseFood;
    private int atBubblePosInList = 0;
    int alreadyCheckedPos = 0;
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
            Debug.Log("Cancel search...");
            chooseFood.enabled = false;
            playerMovement.speed = playerMovement.maxSpeed;
            CancelInvoke(nameof(SearchingFridge));
            ResetSearchUI();
        }
    }
    public void Interact()
    {
        chooseFood.enabled = true;
        searchingForFoodPanel.SetActive(true);
        playerMovement.speed = 0;
        InvokeRepeating(nameof(SearchingFridge),0,1);
    }
    void SearchingFridge()
    {
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
                rarityRankAtPos[atBubblePosInList] = 3;
            }
            else if (procentageCalc <= glitterRank2ChanceProcentage)
            {
                SetRarityOverlayAnimation("IsGlitterRank2", true, atBubblePosInList);
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
        GameObject randomFoodItem = foodItems[Random.Range(0, foodItems.Length)];
        Debug.Log(randomFoodItem.name);
        return randomFoodItem;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
    }
}
