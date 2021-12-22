using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public bool interacting;

    bool canInteract;
    IInteractable interactable;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract && Input.GetKeyDown(KeyCode.E) && !GetInteracting())
        {
            interactable.Interact();
        }
    }
    public bool GetInteracting()
    {
        if(interactable == null)
        {
            return false;
        }
        else
        {
            return interactable.isInteracting();
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
        }
    }
    private void OnTriggerExit2D()
    {
        canInteract = false;
    }
}
