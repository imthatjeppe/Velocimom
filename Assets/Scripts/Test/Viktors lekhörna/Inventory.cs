using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Stack<GameObject> inventory;


    private void Start()
    {
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
        Debug.Log("dropped" + objectToDrop.name);
    }

}
