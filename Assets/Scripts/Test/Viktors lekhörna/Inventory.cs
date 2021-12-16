using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Stack<GameObject> inventory;
    public GameObject itemDropper;
    public GameObject foodItems;
    public GameObject canvas;
    public GameObject player;
    public Text inventoryScoreText;

    public float inventoryScore;
    public int inventoryCount;
    public int inventoryMax;
    public bool isInventoryFull;

    //public Stack<GameObject> inventory;
    //public GameObject player;
    //public Text inventoryScoreText;

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

        item.layer = 5;
        item.transform.position = itemDropper.transform.position;
        item.transform.parent = canvas.transform;
        item.GetComponent<SpriteRenderer>().enabled = false;
        item.GetComponent<BoxCollider2D>().enabled = false;
        item.GetComponent<CircleCollider2D>().enabled = true;
        item.GetComponent<Image>().enabled = true;
        item.GetComponent<Image>().SetNativeSize();
        item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        inventory.Push(item);
        Debug.Log("added" + item.name);
    }

    public void DropItem()
    {
        if (inventory.Count == 0) return;

        GameObject objectToDrop = inventory.Pop();

        inventoryScore -= objectToDrop.GetComponent<FoodItem>().points;

        objectToDrop.layer = 2;
        objectToDrop.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        objectToDrop.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        objectToDrop.GetComponent<SpriteRenderer>().enabled = true;
        objectToDrop.GetComponent<BoxCollider2D>().enabled = true;
        objectToDrop.GetComponent<CircleCollider2D>().enabled = false;
        objectToDrop.GetComponent<Image>().enabled = false;
        objectToDrop.transform.parent = foodItems.transform;
        objectToDrop.transform.position = player.transform.position;

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
        
        if (playerMovement.isRunning && playerMovement.GetPlayerVelocity().magnitude != 0)
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
