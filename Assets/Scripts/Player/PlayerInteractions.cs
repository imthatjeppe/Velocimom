using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    bool canInteract;
    IInteractable interactable;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract && Input.GetKeyDown(KeyCode.E))
        {
            interactable.Interact();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Furniture"))
        {
            if(collision.GetComponent<IInteractable>() != null)
            {
                canInteract = true;
                interactable = collision.GetComponent<IInteractable>();
            }
            else
            {
                canInteract = false;
            }
        }
    }
}
