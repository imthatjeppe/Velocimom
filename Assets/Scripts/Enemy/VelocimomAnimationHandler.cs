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
    private float offsetGraphicsY = 0;
    private float offsetGraphicsX = 0;

    Vector3 oldPosition;

    void Start()
    {
        velocimom = GameObject.FindGameObjectWithTag("Enemy");
        pathFinder = GameObject.FindGameObjectWithTag("Enemy").GetComponent<AIPath>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        distance = 0.2f;
        oldPosition = velocimom.transform.position;
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
        position.y = velocimom.transform.position.y + offsetGraphicsY;
        position.x = velocimom.transform.position.x + offsetGraphicsX;

        transform.position = position;
       
        UpdateGraphicOffset();
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
        if (velocimom.transform.position.y > oldPosition.y + distance)
        {
            oldPosition.y = velocimom.transform.position.y;
            animator.SetBool("GoingUp", true);
            goingUp = true;
            
        }
        else if (velocimom.transform.position.y < oldPosition.y - distance)
        {
            oldPosition.y = velocimom.transform.position.y;
            animator.SetBool("GoingUp", false);
            goingUp = false;

        }
    }

    void FlipSprite()
    {
        if (!goingUp)
        {
            if (velocimom.transform.position.x > oldPosition.x + distance)
            {
                oldPosition.x = velocimom.transform.position.x;
                sprite.flipX = true;
            }
            else if (velocimom.transform.position.x > oldPosition.x - distance)
            {
                oldPosition.x = velocimom.transform.position.x;
                sprite.flipX = false;
            }
        }
        else
        {
            if (velocimom.transform.position.x > oldPosition.x + distance)
            {
                oldPosition.x = velocimom.transform.position.x;
                sprite.flipX = false;
            }
            else if (velocimom.transform.position.x < oldPosition.x - distance)
            {
                oldPosition.x = velocimom.transform.position.x;
                sprite.flipX = true;
            }
        }
    }
    void UpdateGraphicOffset()
    {
        if (goingUp && sprite.flipX) 
        {
            offsetGraphicsY = 1.2f;
            offsetGraphicsX = 0.2f;
        }else if(goingUp && !sprite.flipX)
        {
            offsetGraphicsY = 1.2f;
            offsetGraphicsX = -0.2f;
        }else if (!goingUp)
        {
            offsetGraphicsY = 1.2f;
            offsetGraphicsX = -0.8f;
        }
    }
}
