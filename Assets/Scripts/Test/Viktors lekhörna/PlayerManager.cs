using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject currentInterObj = null;
    public InteractionFood currentInterObjScript = null;
    public Inventory inventory;
    public bool canNotDie;
<<<<<<< HEAD

    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private bool inDropZone;
=======
    
>>>>>>> parent of b20a97f (Spelaren droppar allt på samma gång)
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && currentInterObj && inventory.isInventoryFull == false)
        {
            if(currentInterObjScript/*.InventoryBool*/)
            {
                inventory.AddItem(currentInterObj);
            }
            currentInterObj.SendMessage("DoInteraction");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            inventory.DropItem();
        }
<<<<<<< HEAD



    }
    private void dropAllItems()
    {
            for (int i = 0; i < inventory.inventoryCount; inventory.inventoryCount--)
            {
                inventory.DropItem();
            }
=======
>>>>>>> parent of b20a97f (Spelaren droppar allt på samma gång)
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Food"))
        {
            currentInterObj = other.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<InteractionFood>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Food"))
        {
            if(other.gameObject == currentInterObj)
            {
                currentInterObj = null;
            }
        }
<<<<<<< HEAD

        if (other.CompareTag("DropOff"))
        {
            inDropZone = false;
        }
=======
>>>>>>> parent of b20a97f (Spelaren droppar allt på samma gång)
    }
}
