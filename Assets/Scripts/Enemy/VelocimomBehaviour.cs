using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class VelocimomBehaviour : MonoBehaviour
{
    public int speed = 3;
    public int randomDestinationSpot;

    public float startWaitTime;
    public float startStaringTime;
    public float startInvincibleTime;
    public float distance = 0.2f;
    public float chasingSpeed;

    public Transform[] moveSpots;
    AIDestinationSetter setDestination;

    public bool patrol;

    private PlayerMovement player;
    private Transform target;
    private AIPath pathFinder;

    private float staringTime;
    private float waitTime;
    public float invincibleTime;

    private bool detected;
    private bool playerInvincible;

    // Start is called before the first frame update
    void Start()
    {
        patrol = true;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        staringTime = startStaringTime;
        waitTime = startWaitTime;
        invincibleTime = startInvincibleTime;

        randomDestinationSpot = Random.Range(0, moveSpots.Length);

        setDestination = GetComponent<AIDestinationSetter>();
        setDestination.target = moveSpots[randomDestinationSpot];

        pathFinder = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        SearchForPlayer();

        if (!playerInvincible)
        {
            ChasePlayer();
        }
    }

    void Patrol()
    {
        if (patrol)
        {
            if (Vector2.Distance(transform.position, moveSpots[randomDestinationSpot].position) < distance)
            {
                if (waitTime <= 0)
                {
                    waitTime = startWaitTime;
                    randomDestinationSpot = Random.Range(0, moveSpots.Length);
                    setDestination.target = moveSpots[randomDestinationSpot];
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }

    void SearchForPlayer()
    {
        if (!detected)
        {
            RaycastHit2D sightHit = Physics2D.Raycast(transform.position, transform.right, 10);

            var dist = Vector3.Distance(sightHit.point, transform.position);
            Debug.DrawRay(transform.position, transform.right * dist, Color.red);

            if (sightHit)
            {
                //Debug.Log(sightHit.collider.name);

                if (sightHit.collider.CompareTag("Player"))
                {
                    detected = true;
                    patrol = false;
                    staringTime = startStaringTime;
                }
            }
        }

        if (playerInvincible)
        {
            if (invincibleTime <= 0)
            {
                invincibleTime = startInvincibleTime;
                playerInvincible = false;
            }
            else
            {
                invincibleTime -= Time.deltaTime;
            }
        }
    }

    private void ChasePlayer()
    {
        if (detected)
        {
            if (!player.hidden)
            {
                setDestination.target = player.transform;
            }
            else
            {
                if (player.releasedStaminaKey)
                {
                    staringTime = startStaringTime;
                }
                else
                {
                    staringTime -= Time.deltaTime;
                }
            }

            if (Vector2.Distance(transform.position, player.transform.position) < 2)
            {
                pathFinder.maxSpeed = 0;
            }
            else
            {
                pathFinder.maxSpeed = 6;
            }

            if (staringTime <= 0)
            {
                pathFinder.maxSpeed = 1;

                patrol = true;
                playerInvincible = true;
                detected = false;

                setDestination.target = moveSpots[randomDestinationSpot];
            }

        }
    }
}