using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class VelocimomAnimationHandler : MonoBehaviour
{
    private bool goingUp;

    private AIPath pathFinder;
    private SpriteRenderer sprite;
    private Animator animator;
    private VelocimomBehaviour velocimom;

    Vector3 oldPosition;

    void Start()
    {
        pathFinder = GetComponentInParent<AIPath>();
        velocimom = GetComponentInParent<VelocimomBehaviour>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        AnimationSequencePlayer();
        FlipSprite();

        oldPosition = transform.position;
    }

    void AnimationSequencePlayer()
    {
        GoingUp();
        RigmorWalking();
    }

    void RigmorWalking()
    {
        animator.SetFloat("Movement", pathFinder.maxSpeed);
    }

    void GoingUp()
    {
        if (transform.position.y > oldPosition.y)
        {
            animator.SetBool("GoingUp", true);
            goingUp = true;
        }
        else if (transform.position.y < oldPosition.y)
        {
            animator.SetBool("GoingUp", false);
            goingUp = false;
        }
    }

    void FlipSprite()
    {
        //TODO: Flip för goingUp skall vara tvärtom
        if (transform.position.x > oldPosition.x)
        {
            sprite.flipX = true;
        }
        else if (transform.position.x < oldPosition.x)
        {
            sprite.flipX = false;
        }
    }

}
