using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionFood : MonoBehaviour
{
    public bool itemInHands;
    public bool InventoryBool;
    public GameObject player;

    public float yPos = -3f;
    public float xPos = -9f;

    private Vector3 offsetPosition;
    private Inventory inventory;

    private void Start()
    {
        inventory = player.GetComponent<Inventory>();
    }

    private void Update()
    {

        for (int i = 0; i < inventory.inventoryCount; i++)
        {
            yPos += 1f;
        }

        offsetPosition = new Vector3(xPos, yPos, 10);

        if (itemInHands)
        {
            gameObject.transform.position = Camera.main.transform.position + offsetPosition;
        }
    }

    public void DoInteraction()
    {
        gameObject.SetActive(false);
        //gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //itemInHands = true;
    }
}
