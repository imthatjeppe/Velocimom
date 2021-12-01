using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class VelocimomBehaviour : MonoBehaviour
{
    public int speed = 3;

    public float startWaitTime;
    public float startStaringTime;
    public float distance = 0.2f;
    public float chasingSpeed;

    public Transform[] moveSpots;
    AIDestinationSetter setDestination;

    public bool patrol;
    
    private int randomDestinationSpot;

    private PlayerMovement player;
    private Transform target;
    private AIPath pathFinder;

    private float staringTime;
    private float waitTime;

    bool detected;

    // Start is called before the first frame update
    void Start()
    {
        patrol = true;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        staringTime = startStaringTime;
        waitTime = startWaitTime;

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
        ChasePlayer();
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
                staringTime -= Time.deltaTime;
            }

            if (Vector2.Distance(transform.position, player.transform.position) < 2)
            {
                pathFinder.maxSpeed = 0;
                //Debug.Log("nära");
            }
            else
            {
                pathFinder.maxSpeed = 6;
            }

            if (staringTime <= 0)
            {
                pathFinder.maxSpeed = 1;

                patrol = true;
                detected = false;

                setDestination.target = moveSpots[randomDestinationSpot];
            }
        }
    }
}