using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject currentInterObj = null;
    public InteractionFood currentInterObjScript = null;
    public Inventory inventory;
    public Score score;
    public bool canNotDie;

    private bool inDropZone;

    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentInterObj && inventory.isInventoryFull == false)
        {
            if (currentInterObjScript)
            {
                inventory.AddItem(currentInterObj);
            }
            //currentInterObj.SendMessage("DoInteraction");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inDropZone)
            {
                dropAllItems();
                score.ScoreBounce();
            }
            else
            {
                inventory.DropItem();
            }
        }
    }
    public void dropAllItems()
    {
        for (int i = 0; i < inventory.inventoryCount; inventory.inventoryCount--)
        {
            inventory.DropItem();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            currentInterObj = other.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<InteractionFood>();
        }

        if (other.CompareTag("DropOff"))
        {
            inDropZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            if (other.gameObject == currentInterObj)
            {
                currentInterObj = null;
            }
        }

        if (other.CompareTag("DropOff"))
        {
            inDropZone = false;
        }
    }
}
