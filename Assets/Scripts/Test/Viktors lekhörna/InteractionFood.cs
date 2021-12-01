using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionFood : MonoBehaviour
{
    public bool Inventory;
    public bool itemInInventory;
    public GameObject player;

    private SpriteRenderer spr;

    Vector2 pos;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 pos = new Vector2(player.transform.position.x, player.transform.position.y);

        if (itemInInventory)
        {
            transform.position = pos;
        }
        else
        {
            spr.enabled = true;
        }
    }

    public void DoInteraction()
    {
        itemInInventory = true;
        spr.enabled = false;
    }
}
