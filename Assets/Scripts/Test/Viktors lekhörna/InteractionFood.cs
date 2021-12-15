using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionFood : MonoBehaviour
{
    public bool itemInHands;
    public bool InventoryBool;
    public GameObject player;

    private Inventory inventory;

    private void Start()
    {
        inventory = player.GetComponent<Inventory>();
    }

    public void DoInteraction()
    {
        //gameObject.SetActive(false);
    }
}
