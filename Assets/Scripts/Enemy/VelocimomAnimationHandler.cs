using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class VelocimomAnimationHandler : MonoBehaviour
{
    private float distance;

    private bool goingUp;

    private AIPath pathFinder;
    private SpriteRenderer sprite;
    private Animator animator;
    private GameObject velocimom;

    Vector3 oldPosition;

    void Start()
    {
        velocimom = GameObject.FindGameObjectWithTag("Enemy");
        pathFinder = GameObject.FindGameObjectWithTag("Enemy").GetComponent<AIPath>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        distance = 0.2f;
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FlipSprite();
        AnimationSequencePlayer();
        KeepGraphicsOnVelocimom();
    }

    void KeepGraphicsOnVelocimom()
    {
        Vector3 position = transform.position;
        position.y = velocimom.transform.position.y;
        position.x = velocimom.transform.position.x;

        transform.position = position;
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
        if (transform.position.y > oldPosition.y + distance)
        {
            oldPosition.y = transform.position.y;
            animator.SetBool("GoingUp", true);
            goingUp = true;
        }
        else if (transform.position.y < oldPosition.y - distance)
        {
            oldPosition.y = transform.position.y;
            animator.SetBool("GoingUp", false);
            goingUp = false;
        }
    }

    void FlipSprite()
    {
        if (!goingUp)
        {
            if (transform.position.x > oldPosition.x + distance)
            {
                oldPosition.x = transform.position.x;
                sprite.flipX = true;
            }
            else if (transform.position.x < oldPosition.x - distance)
            {
                oldPosition.x = transform.position.x;
                sprite.flipX = false;
            }
        }
        else
        {
            if (transform.position.x > oldPosition.x + distance)
            {
                oldPosition.x = transform.position.x;
                sprite.flipX = false;
            }
            else if (transform.position.x < oldPosition.x - distance)
            {
                oldPosition.x = transform.position.x;
                sprite.flipX = true;
            }
        }
    }

}
