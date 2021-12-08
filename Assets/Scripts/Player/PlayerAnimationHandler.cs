using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    PlayerMovement playerMovement;
    Animator animator;
    SpriteRenderer sprite;
    PlayerAudioHandler audioHandler;
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
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
        PlayerWalkingAimation();
    }
    void PlayerWalkingAimation()
    {
        animator.SetFloat("Movement", playerMovement.GetPlayerVelcoity().magnitude);
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
