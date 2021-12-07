using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //this one for you jeppe
    public Stack<GameObject> inventory;
    public GameObject player;
    public int inventoryCount;

    private Rigidbody2D rb;
    private float movometer;
    private float startMovometer = 2f;
    private bool isMoving;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        inventory = new Stack<GameObject>();
        movometer = startMovometer;
    }

    private void Update()
    {
        inventoryCount = inventory.Count;


        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Vertical") > 0)
        {
            isMoving = true;
            movometer -= Time.deltaTime;
            Debug.Log("moving");
        }
        else
        {
            isMoving = false;
            Debug.Log("Not moving");
        }

        if (movometer <= 0)
        {
            UnstableStack();
            movometer = startMovometer;
        }
    }

    public void AddItem(GameObject item)
    {
        inventory.Push(item);
        Debug.Log("added" + item.name);
    }

    public void DropItem()
    {
        if (inventory.Count == 0) return;

        GameObject objectToDrop = inventory.Pop();

        objectToDrop.transform.position = player.transform.position;
        objectToDrop.SetActive(true);

        Debug.Log("dropped" + objectToDrop.name);
        Debug.Log("Items in inventory: " + inventory.Count);
    }

    public void UnstableStack()
    {
        if (inventory.Count <= 3)
        {
            return;
        }
        else if (inventory.Count >= 4 && isMoving)
        {
            Invoke("DropItem", Random.Range(4f, 7f));
        }
    }
}
