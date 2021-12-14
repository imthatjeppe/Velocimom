using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject currentInterObj = null;
    public InteractionFood currentInterObjScript = null;
    public Inventory inventory;

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
    }
}
