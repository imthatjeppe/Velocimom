using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    PlayerMovement playerMovement;
    Inventory playerInventory;
    Animator animator;
    SpriteRenderer sprite;
    PlayerAudioHandler audioHandler;

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerInventory= GetComponentInParent<Inventory>();
        audioHandler = GetComponentInParent<PlayerAudioHandler>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationSequencePlayer();
        FlipSprite();
    }
    void AnimationSequencePlayer()
    {
        PlayerWalkingAnimation();
        CheckInventory();
        CheckHidden();
    }
    void PlayerWalkingAnimation()
    {
        animator.SetFloat("Movement", playerMovement.GetPlayerVelcoity().magnitude);
    }
    void CheckInventory()
    {
        if (playerInventory.inventoryCount != 0)
        {
            animator.SetBool("HoldingFood", true);
        }
        else
        {
            animator.SetBool("HoldingFood", false);
        }
    }
    void CheckHidden()
    {
        if (playerMovement.hidden)
        {
            animator.SetBool("Freeze", true);
        }
        else
        {
            animator.SetBool("Freeze", false);
        }
    }
    void FlipSprite()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            sprite.flipX = true;
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            sprite.flipX = false;
        }
    }
}
