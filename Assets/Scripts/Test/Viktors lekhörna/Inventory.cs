using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Stack<GameObject> inventory;
    public GameObject player;
    public Text inventoryScoreText;
    public int inventoryCount;
    public int inventoryMax;
    public float inventoryScore;
    public bool isInventoryFull;

    private PlayerMovement playerMovement;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = new Stack<GameObject>();
        playerMovement = GetComponent<PlayerMovement>();

        InvokeRepeating("UnstableStack",4f, 2f);
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

        inventory.Push(item);
        Debug.Log("added" + item.name);
    }

    public void DropItem()
    {
        if (inventory.Count == 0) return;

        GameObject objectToDrop = inventory.Pop();

        inventoryScore -= objectToDrop.GetComponent<FoodItem>().points;
        objectToDrop.transform.position = player.transform.position;
        objectToDrop.SetActive(true);

        Debug.Log("dropped" + objectToDrop.name);
        Debug.Log("Items in inventory: " + inventory.Count);
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
        
        if (playerMovement.isRunning && playerMovement.GetPlayerVelcoity().magnitude != 0)
        {
            int diceRoll = Random.Range(1, 3);

            if (diceRoll == 1 || diceRoll == 2)
            {
                DropItem();
            }
            else
            {
                return;
            }
        }
    }
}
