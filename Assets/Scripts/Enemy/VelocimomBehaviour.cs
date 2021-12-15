using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class VelocimomBehaviour : MonoBehaviour
{
    [Header("Int Variables")]
    public int speed = 3;
    public int randomDestinationSpot;

    [Header("Float Variables")]
    public float startWaitTime;
    public float startStaringTime;
    public float startInvincibleTime;
    public float distance = 0.2f;
    public float chasingSpeed;
    public float patrolSpeed;
    public float invincibleTime;

    [Header("Destinations")]
    public Transform[] moveSpots;
    public Transform spawnPoint;

    [Header("Bools")]
    public bool patrol;

    [Header("GameObjects")]
    public GameObject playerObject;
    public GameObject playerDetection;
    public GameObject playerPathSpots;

    private DetectPlayerInRange detectPlayerInRange;
    private RigmorAudioHandler audioHandler;

    private PlayerMovement player;
    private PlayerManager playerManager;
    private Transform target;
    private AIPath pathFinder;
    private AIDestinationSetter setDestination;

    private float staringTime;
    private float waitTime;
    
    private int losPathAt = 0;

    private bool detected;
    private bool playerInvincible;
    private bool lostLineOfSight;

    private List<GameObject> playerSpotsToFollow;

    // Start is called before the first frame update
    void Start()
    {
        patrol = true;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerManager = playerObject.GetComponent<PlayerManager>();
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
        audioHandler = GetComponent<RigmorAudioHandler>();
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
        Debug.DrawRay(transform.position, playerObject.transform.position - transform.position, Color.red);
        if (!detected && detectPlayerInRange.playerInRange && !player.inSafeRoom)
        {
            RaycastHit2D sightHit = Physics2D.Raycast(transform.position, playerObject.transform.position - transform.position, 10);

            if (sightHit)
            {

                if (sightHit.collider.CompareTag("Player"))
                {
                    //Just to be sure to play this sound once when you are detected
                    if (patrol && !playerInvincible)
                    {
                        audioHandler.PlayRigmorDetectionRoarSFX();
                    }
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
            }
            else if (lostLineOfSight)
            {
                if (playerSpotsToFollow.Count > 0)
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

                pathFinder.maxSpeed = patrolSpeed;

                if (!playerManager.canNotDie)
                {
                    player.transform.position = spawnPoint.position;
                    PlayerHealth.playerHealth -= 1;
                }

                detected = false;
                patrol = true;
                setDestination.target = moveSpots[randomDestinationSpot];

            }
            else
            {
                pathFinder.maxSpeed = chasingSpeed;
            }

            if (staringTime <= 0 || player.inSafeRoom)
            {
                pathFinder.maxSpeed = patrolSpeed;

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
            if (!sightHit.collider.CompareTag("Player") && !sightHit.collider.CompareTag("Furniture"))
            {
                //if line of sigt is lost, the player will lay out paths for velocimom to follow in order, she is "guessing" where player went
                lostLineOfSight = true;
                if (!IsInvoking(nameof(AddPlayerPathSpots)))
                {
                    InvokeRepeating(nameof(AddPlayerPathSpots), 0, 0.5f);
                }
                //if line of sight returns, clear the temporary path list and stop adding more spots
            }
            else if (sightHit.collider.CompareTag("Player") || sightHit.collider.CompareTag("Furniture") && playerSpotsToFollow.Count > 0)
            {
                CancelInvoke(nameof(AddPlayerPathSpots));
                lostLineOfSight = false;

                clearPlayerPathSpots();
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

    public void clearPlayerPathSpots()
    {
        losPathAt = 0;
        foreach (GameObject spots in playerSpotsToFollow)
        {
            Destroy(spots);
        }
        playerSpotsToFollow.Clear();
    }

}