using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Stack<GameObject> inventory;
    public GameObject player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = new Stack<GameObject>();
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

}
