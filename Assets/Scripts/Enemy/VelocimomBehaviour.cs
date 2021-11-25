using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocimomBehaviour : MonoBehaviour
{
    public int speed = 3;

    public float startWaitTime;
    public float distance = 0.2f;

    public Transform[] moveSpots;

    private int randomDestinationSpot;

    private float waitTime;

    private Vector2 deBugPosition;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        randomDestinationSpot = Random.Range(0, moveSpots.Length);

        deBugPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Random.Range(0 + 200f, Screen.height - 200f)));
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        //TODO: Chase-script inkl. isHidden
    }

    void Patrol()
    {
        RaycastHit2D sightHit = Physics2D.Raycast(transform.position, transform.right, distance);

        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomDestinationSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[randomDestinationSpot].position) < distance)
        {

            if (waitTime <= 0)
            {
                waitTime = startWaitTime;
                randomDestinationSpot = Random.Range(0, moveSpots.Length);
            }

            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
