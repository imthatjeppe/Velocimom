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

    private float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        randomDestinationSpot = Random.Range(0, moveSpots.Length);

        setDestination = GetComponent<AIDestinationSetter>();

        setDestination.target = moveSpots[randomDestinationSpot];
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        //TODO: Chase-script inkl. isHidden
    }

    void Patrol()
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
