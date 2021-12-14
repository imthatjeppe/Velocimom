using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Stack<GameObject> inventory;
    public GameObject player;
    public int inventoryCount;
    public int inventoryMax;
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

        InventoryFullChecker();
    }

    public void AddItem(GameObject item)
    {
        if (isInventoryFull) return;
        
        inventory.Push(item);
        Debug.Log("added" + item.name);

    }

    public void DropItem()
    {
        if (inventory.Count == 0) return;

        GameObject objectToDrop = inventory.Pop();

        objectToDrop.transform.position = player.transform.position;
        objectToDrop.GetComponent<BoxCollider2D>().enabled = true;
        objectToDrop.GetComponent<InteractionFood>().itemInHands = false;

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
        
        if (playerMovement.isRunning)
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
