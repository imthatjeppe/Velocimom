using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStackBehaviour : MonoBehaviour
{
    public Sprite[] stackSprites;

    Inventory inventory;
    SpriteRenderer spriteRenderer;
    bool flipped = false;
    int lastWalkedVertical = 0;
    int oldInventoryCount = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        inventory = transform.parent.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFoodStack();
        FlipSprite();
        ChangeOrderInLayerUpWards();
    }
    void UpdateFoodStack()
    {
        if(oldInventoryCount != inventory.inventoryCount)
        {
            oldInventoryCount = inventory.inventoryCount;
            spriteRenderer.sprite = stackSprites[inventory.inventoryCount];
        }
    }
    void FlipSprite()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.01f)
        {
            spriteRenderer.flipX = true;
            
            if (!flipped)
            {
                transform.localPosition = new Vector2(0.26f, transform.localPosition.y);
                flipped = true;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < -0.01f)
        {
            spriteRenderer.flipX = false;
            if (flipped)
            {
                transform.localPosition = new Vector2(-0.97f, transform.localPosition.y);
                flipped = false;
            }
        }
    }
    void ChangeOrderInLayerUpWards()
    {
        if(Input.GetAxisRaw("Horizontal") != 0)
            spriteRenderer.sortingOrder = lastWalkedVertical;

        if (Input.GetAxisRaw("Vertical") > 0.01f)
        {
            spriteRenderer.sortingOrder = -1;
            lastWalkedVertical = -1;
        }
        else if (Input.GetAxisRaw("Vertical") < -0.01f)
        {
            lastWalkedVertical = 1;
            spriteRenderer.sortingOrder = 1;
        }else if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            spriteRenderer.sortingOrder = 1;
        }
    }
}
