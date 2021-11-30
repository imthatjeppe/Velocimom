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

    private int randomDestinationSpot;

    private PlayerMovement player;
    private Transform target;

    private float staringTime;
    private float waitTime;

    private bool patrol;


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
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        SearchForPlayer();
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
        RaycastHit2D sightHit = Physics2D.Raycast(transform.position, transform.right, distance);
        Debug.DrawRay(transform.position, transform.right, Color.green);

        if (sightHit.collider.CompareTag("Player"))
        {
            patrol = false;

            if (!player.hidden)
            {
                Chase();
            }

            if (staringTime <= 0)
            {
                patrol = true;
            }

            else if (staringTime > 0)
            {
                staringTime -= Time.deltaTime;
            }
        }

    }

    void Chase()
    {
        if (Vector2.Distance(transform.position, target.position) > 3)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, chasingSpeed * Time.deltaTime);
        }
    }

}