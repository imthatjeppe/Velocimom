using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class VelocimomBehaviour : MonoBehaviour
{
    public int speed = 3;

    public float startWaitTime;
    public float distance = 0.2f;

    public Transform[] moveSpots;
    AIDestinationSetter setDestination;
    private int randomDestinationSpot;

<<<<<<< HEAD
    private float waitUntilNextPointTime;
    private float staringTime;

    private PlayerMovement player;
    private Transform target;
=======
    private float waitTime;
>>>>>>> parent of a391062 (welp)

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        patrol = true;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        waitUntilNextPointTime = startWaitUntilNextPointTime;
        staringTime = startStaringTime;
=======
        waitTime = startWaitTime;
>>>>>>> parent of a391062 (welp)
        randomDestinationSpot = Random.Range(0, moveSpots.Length);

        setDestination = GetComponent<AIDestinationSetter>();

        setDestination.target = moveSpots[randomDestinationSpot];
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
<<<<<<< HEAD
        SearchForPlayer();
=======
        //TODO: Chase-script inkl. isHidden
>>>>>>> parent of a391062 (welp)
    }

    void Patrol()
    {
<<<<<<< HEAD
        if (patrol)
        {
            if (Vector2.Distance(transform.position, moveSpots[randomDestinationSpot].position) < distance)
=======
        if (Vector2.Distance(transform.position, moveSpots[randomDestinationSpot].position) < distance)
        {
            if (waitTime <= 0)
>>>>>>> parent of a391062 (welp)
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
<<<<<<< HEAD


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
=======
}
>>>>>>> parent of a391062 (welp)
