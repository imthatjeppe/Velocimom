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
    public float invincibleTime;


    public Transform[] moveSpots;
    AIDestinationSetter setDestination;

    public bool patrol;

    public GameObject playerObject;
    public GameObject playerDetection;
    public GameObject playerPathSpots;

    private DetectPlayerInRange detectPlayerInRange;

    private PlayerMovement player;
    private Transform target;
    private AIPath pathFinder;

    private float staringTime;
    private float waitTime;
    int losPathAt = 0;

    private bool detected;
    private bool playerInvincible;
    private bool lostLineOfSight;

    private List<GameObject> playerSpotsToFollow;

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

        detectPlayerInRange = playerDetection.GetComponent<DetectPlayerInRange>();
        playerSpotsToFollow = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        SearchForPlayer();

        if (!playerInvincible /*&& !player.hidden*/)
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
        Debug.DrawRay(transform.position, playerObject.transform.position - transform.position, Color.red);
        if (!detected && detectPlayerInRange.playerInRange && !player.inSafeRoom)
        {
            RaycastHit2D sightHit = Physics2D.Raycast(transform.position, playerObject.transform.position - transform.position, 10);

            if (sightHit)
            {

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

            CheckPlayerLineOfSight();

            if (!player.hidden && !lostLineOfSight)
            {
                setDestination.target = player.transform;
            } else if (lostLineOfSight)
            {
                if(playerSpotsToFollow.Count > 0)
                {
                    setDestination.target = playerSpotsToFollow[losPathAt].transform;
                    if (Vector2.Distance(transform.position, playerSpotsToFollow[losPathAt].transform.position) < 0.2f && losPathAt < playerSpotsToFollow.Count)
                    {
                        losPathAt++;
                    }
                }
            }

            CheckPlayerHidden();

            if (Vector2.Distance(transform.position, player.transform.position) < 2)
            {
                pathFinder.maxSpeed = 0;
            }
            else
            {
                pathFinder.maxSpeed = 3;
            }

            if (staringTime <= 0 || player.inSafeRoom)
            {
                pathFinder.maxSpeed = 1;

                patrol = true;
                playerInvincible = true;
                detected = false;

                setDestination.target = moveSpots[randomDestinationSpot];
            }

        }
    }
    void AddPlayerPathSpots()
    {
        GameObject instancedPath = Instantiate(playerPathSpots);
        instancedPath.transform.position = playerObject.transform.position;
        playerSpotsToFollow.Add(instancedPath);
    }
    void CheckPlayerLineOfSight()
    {
        //cast a ray to see if velocimom has line of sight to player
        RaycastHit2D sightHit = Physics2D.Raycast(transform.position, playerObject.transform.position - transform.position, 10);

        if (sightHit)
        {

            if (!sightHit.collider.CompareTag("Player"))
            {
                //if line of sigt is lost, the player will lay out paths for velocimom to follow in order, she is "guessing" where player went
                lostLineOfSight = true;
                if (!IsInvoking(nameof(AddPlayerPathSpots)))
                {
                    InvokeRepeating(nameof(AddPlayerPathSpots), 0, 0.5f);
                }
                //if line of sight returns, clear the temporary path list and stop adding more spots
            }
            else if (sightHit.collider.CompareTag("Player") && playerSpotsToFollow.Count > 0)
            {
                CancelInvoke(nameof(AddPlayerPathSpots));
                lostLineOfSight = false;
                losPathAt = 0;
                foreach (GameObject spots in playerSpotsToFollow)
                {
                    Destroy(spots);
                }
                playerSpotsToFollow.Clear();
            }
        }
    }
    void CheckPlayerHidden()
    {
        if (player.hidden)
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
    }
}