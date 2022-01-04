using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Inventory : MonoBehaviour
{
    public Stack<GameObject> inventory;
    public GameObject itemDropper;
    public GameObject foodItems;
    public GameObject canvas;
    public GameObject player;
    public GameObject velocimom;
    public Text inventoryScoreText;
    
    public Vector3 unstableDropSpot;

    public float inventoryScore;
    public int inventoryCount;
    public int inventoryMax;
    public bool isInventoryFull;

    private Vector3 yAxisPlus = new Vector3(0, 0.5f, 0);
    private PlayerMovement playerMovement;
    private VelocimomBehaviour velocimomBehaviour;
    private bool unstableDrop;
    private PlayerAudioHandler audiohandler;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audiohandler = player.GetComponentInChildren<PlayerAudioHandler>();
        inventory = new Stack<GameObject>();
        playerMovement = GetComponent<PlayerMovement>();
        velocimomBehaviour = velocimom.GetComponent<VelocimomBehaviour>();

        InvokeRepeating(nameof(SavePlayerPosition), 3f, 0.75f);
        InvokeRepeating(nameof(UnstableStack) , 3f, 2.5f);
    }

    private void Update()
    {
        inventoryCount = inventory.Count;
        inventoryScoreText.text = "" + inventoryScore;

        InventoryFullChecker();
    }

    public void AddItem(GameObject item)
    {
        if (isInventoryFull) return;
 
        inventoryScore += item.GetComponent<FoodItem>().points;
        item.GetComponent<FoodItem>().itemToDestroy = Instantiate(item.GetComponent<FoodItem>().canvasFoodItem);

        audiohandler.PlayFoodPickUpSFX();
        inventory.Push(item);

        item.GetComponent<FoodItem>().DeactivateItem();
    }

    public void DropItem()
    {
        if (inventory.Count == 0) return;

        GameObject objectToDrop = inventory.Pop();

        Destroy(objectToDrop.GetComponent<FoodItem>().itemToDestroy);

        if (unstableDrop)
        {
            inventoryScore -= objectToDrop.GetComponent<FoodItem>().points;
            objectToDrop.GetComponent<FoodItem>().hasBeenDropped = true;
            objectToDrop.transform.position = player.transform.position + yAxisPlus;
            objectToDrop.transform.DOJump(unstableDropSpot, 0.75f, 1, 1.5f, false);
        }
        else
        {
            inventoryScore -= objectToDrop.GetComponent<FoodItem>().points;
            objectToDrop.transform.position = player.transform.position - yAxisPlus;
        }

        unstableDrop = false;

        if (velocimomBehaviour.playerIsDead)
        {
            Destroy(objectToDrop);
        }

        objectToDrop.GetComponent<FoodItem>().ActivateItem();
    }

    public void InventoryFullChecker()
    {
        if (inventoryCount < inventoryMax)
        {
            isInventoryFull = false;
        }
        else if (inventoryCount >= inventoryMax)
        {
            isInventoryFull = true;
        }
    }

    public void UnstableStack()
    {
        if (!playerMovement.isRunning) return;
        
        if (inventory.Count > 0 && playerMovement.GetPlayerVelocity().magnitude != 0)
        {
            int diceRoll = Random.Range(1, 3);

            if (diceRoll == 1 || diceRoll == 2)
            {
                unstableDrop = true;
                audiohandler.PlayFoodSlipSFX();
                DropItem();
            }
            else
            {
                return;
            }
        }
    }

    public void SavePlayerPosition()
    {
        unstableDropSpot = new Vector3(player.transform.position.x, player.transform.position.y);
    }
}
