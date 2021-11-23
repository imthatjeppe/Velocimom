using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocimomBehaviour : MonoBehaviour
{
    public int speed = 3;

    public float startWaitTime;

    public Transform[] moveSpots;

    private int randomSpot;

    private float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();

        //TODO: Chase-script inkl. isHidden
    }

    void Patrol()
    {
        //TODO: Ta hänsyn till väggar/items
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                waitTime = startWaitTime;
                randomSpot = Random.Range(0, moveSpots.Length);
            }

            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
