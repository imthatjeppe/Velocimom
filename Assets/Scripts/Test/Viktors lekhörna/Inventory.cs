using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Inventory : MonoBehaviour
{
    public Stack<GameObject> inventory;
    public GameObject player;

    private Rigidbody2D rb;
    private float dropInterval;
    private bool isMoving;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        inventory = new Stack<GameObject>();
        dropInterval = Random.Range(4f, 7f);
      
        InvokeRepeating("UnstableStack", 5f, dropInterval);

    }

    private void Update()
    {

        //Debug.Log(rb.velocity);
        //Debug.Log(isMoving);
        //while (rb.velocity == Vector2.zero)
        //{
        //    isMoving = true;
        //}


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
    }

    public void UnstableStack()
    {
        if (inventory.Count <= 3)
        {
            return;
        }
        else if (inventory.Count >= 4 && rb.velocity != Vector2.zero)
        {
            DropItem();
            Debug.Log("Items in inventory: " + inventory.Count);
        }
    }
}
