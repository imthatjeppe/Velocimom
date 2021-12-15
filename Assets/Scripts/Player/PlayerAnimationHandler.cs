using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    PlayerMovement playerMovement;
    Inventory playerInventory;
    PlayerAudioHandler audioHandler;
    Animator animator;
    SpriteRenderer sprite;

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerInventory = GetComponentInParent<Inventory>();
        audioHandler = GetComponentInParent<PlayerAudioHandler>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        AnimationSequencePlayer();
        FlipSprite();
    }

    void AnimationSequencePlayer()
    {
        PlayerWalkingAnimation();
        PlayerRunningAnimation();
        CheckInventory();
        CheckHidden();
    }

    void FlipSprite()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.01f)
        {
            sprite.flipX = true;
        }
        else if (Input.GetAxisRaw("Horizontal") < -0.01f)
        {
            sprite.flipX = false;
        }
    }

    void PlayerWalkingAnimation()
    {
        animator.SetFloat("Movement", playerMovement.GetPlayerVelocity().magnitude);
    }

    void PlayerRunningAnimation()
    {
        animator.SetBool("IsRunning", playerMovement.isRunning);
    }

    void CheckInventory()
    {
        if (playerInventory.inventoryCount <= 0)
        {
            animator.SetBool("HoldingFood", false);
        }
        else
        {
            animator.SetBool("HoldingFood", true);
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

}
