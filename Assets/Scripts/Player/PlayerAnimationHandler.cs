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
        PlayerRunningAnimation();
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
    void PlayerRunningAnimation()
    {
            animator.SetBool("IsRunning",playerMovement.isRunning);

    }
    void FlipSprite()
    {
        if (Input.GetAxisRaw("Horizontal")>0.01f)
        {
            sprite.flipX = true;
        }
        else if(Input.GetAxisRaw("Horizontal") < -0.01f)
        {
            sprite.flipX = false;
        }
    }
}
