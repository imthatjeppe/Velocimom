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

    public float inventoryScore;
    public int inventoryCount;
    public int inventoryMax;
    public bool isInventoryFull;

    public Vector3 unstableDropSpot;

    private Vector3 yAxisPlus = new Vector3(0, 0.5f, 0);
    private PlayerMovement playerMovement;
    private VelocimomBehaviour velocimomBehaviour;
    private bool unstableDrop;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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

        item.layer = 5;
        item.transform.position = itemDropper.transform.position;
        item.transform.SetParent(canvas.transform, false);
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
        objectToDrop.transform.SetParent(foodItems.transform);

        if (unstableDrop)
        {
            objectToDrop.transform.position = player.transform.position + yAxisPlus;
            objectToDrop.transform.DOJump(unstableDropSpot, 0.75f, 1, 1.5f, false);
        }
        else
        {
            objectToDrop.transform.position = player.transform.position;
        }

        if (velocimomBehaviour.playerIsDead)
        {
            Destroy(objectToDrop);
        }

        unstableDrop = false;

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
                unstableDrop = true;
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
