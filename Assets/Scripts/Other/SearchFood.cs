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

    Animator[] rarityOverlayAnimators;
    Image[] foodItemImages;
    //Gameojbects in Dictonary<bubble, fooditem>
    Dictionary<GameObject,GameObject> bubbleFoodDic;

    private int atBubblePosInList = 0;
    void Start()
    {
        bubbleFoodDic = new Dictionary<GameObject, GameObject>();
        rarityOverlayAnimators = new Animator[bubbles.Length];
        foodItemImages = new Image[bubbles.Length];
        for (int i = 0; i < bubbles.Length; i++)
        {
            foodItemImages[i] = bubbles[i].transform.GetChild(0).GetComponent<Image>();
            rarityOverlayAnimators[i] = bubbles[i].transform.GetChild(1).GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("Cancel search...");
            CancelInvoke(nameof(SearchingFridge));
            ResetSearchUI();
        }
    }
    public void Interact()
    {
        searchingForFoodPanel.SetActive(true);
        InvokeRepeating(nameof(SearchingFridge),0,1);
    }
    void SearchingFridge()
    {
        GameObject randomFoodItem;
        if(atBubblePosInList <= 7)
        {
            bubbles[atBubblePosInList].SetActive(true);
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
        Debug.Log("Procentage: " + procentageCalc);
        CheckIfAnimatorIsActive(atBubblePosInList);

            if (procentageCalc <= shinyRareRank3ChanceProcentage)
            {
                Debug.Log("SHINYRARE!!");
                SetRarityOverlayAnimation("IsShinyRareRank3", true, atBubblePosInList);
            }
            else if (procentageCalc <= glitterRank2ChanceProcentage)
            {
                Debug.Log("GLITTER!!");
                SetRarityOverlayAnimation("IsGlitterRank2", true, atBubblePosInList);
            }
    }
    bool CheckIfAnimatorIsActive(int atPos)
    {
        if (!rarityOverlayAnimators[atPos].gameObject.activeSelf)
        {
            rarityOverlayAnimators[atPos].gameObject.SetActive(true);
        }
        return true;
    }
    void SetRarityOverlayAnimation(string boolName, bool activeAnimation, int atPos)
    {
        rarityOverlayAnimators[atPos].SetBool(boolName, activeAnimation);
        Debug.Log(boolName);
    }
    GameObject GetRandomFoodItem()
    {
        GameObject randomFoodItem = foodItems[Random.Range(0, foodItems.Length)];
        Debug.Log(randomFoodItem.name);
        return randomFoodItem;
    }
}
