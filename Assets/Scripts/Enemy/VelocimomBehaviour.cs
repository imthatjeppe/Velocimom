using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class VelocimomBehaviour : MonoBehaviour
{
    [Header("Int Variables")]
    public int speed = 3;

    [Header("Float Variables")]
    public float waitTime;
    public float startStaringTime;
    public float invincibleTime;
    public float distance = 0.2f;
    public float chasingSpeed;
    public float patrolSpeed;

    [Header("Destinations")]
    public Transform[] moveSpots;
    public Transform spawnPoint;

    [Header("Bools")]
    public bool patrol;

    [Header("GameObjects")]
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

    private int randomDestinationSpot;
    private int losPathAt = 0;

    private bool detected;
    private bool lostLineOfSight;

    private List<GameObject> playerSpotsToFollow;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerManager = player.GetComponent<PlayerManager>();
        target = player.transform;

        staringTime = startStaringTime;

        setDestination = GetComponent<AIDestinationSetter>();

        pathFinder = GetComponent<AIPath>();
        SelectNewDestination();

        detectPlayerInRange = playerDetection.GetComponent<DetectPlayerInRange>();
        playerSpotsToFollow = new List<GameObject>();
        audioHandler = GetComponent<RigmorAudioHandler>();
    }

    public void SelectNewDestination()
    {
        detected = false;

        pathFinder.maxSpeed = patrolSpeed;
        Debug.Log("New point selected");

        //Select a new random point, avoid the one in use.
        int newRandomDestination = Random.Range(0, moveSpots.Length);
        while (randomDestinationSpot == newRandomDestination)
        {
            newRandomDestination = Random.Range(0, moveSpots.Length);
        }
        randomDestinationSpot = newRandomDestination;
        setDestination.target = moveSpots[randomDestinationSpot];
        patrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        SearchForPlayer();
        CheckPlayerHidden();

        if (detected)
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
                Debug.Log("Waiting to select new point");
                Invoke(nameof(SelectNewDestination), waitTime);
                patrol = false;
            }
        }
    }

    void SearchForPlayer()
    {
        Debug.DrawRay(transform.position, target.position - transform.position, Color.red);
        if (detectPlayerInRange.playerInRange && !detected && !player.inSafeRoom)
        {
            RaycastHit2D sightHit = Physics2D.Raycast(transform.position, target.position - transform.position, 10);

            if (sightHit)
            {

                if (sightHit.collider.CompareTag("Player"))
                {
                    Debug.Log("Player detected");
                    //Just to be sure to play this sound once when you are detected
                    if (patrol || IsInvoking(nameof(SelectNewDestination)))
                    {
                        Debug.Log("Waiting for reaction time");
                        CancelInvoke(nameof(SelectNewDestination));
                        Invoke(nameof(ReactionTime), invincibleTime);
                        audioHandler.PlayRigmorDetectionRoarSFX();
                    }

                    patrol = false;
                }
            }
        }
    }

    void ReactionTime()
    {
        detected = true;
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

    private void ChasePlayer()
    {
        CheckPlayerLineOfSight();

        if (!player.hidden && !lostLineOfSight)
        {
            setDestination.target = target;
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

        if (!playerManager.canNotDie)
        {
            if (detected && Vector2.Distance(transform.position, target.position) < 1)
            {
                //player has died
                //TODO: remove player controls
                //TODO: play death animation or such
                Debug.Log("I dieded");
                detected = false;
                //Invoke(nameof(Death), 0.5f);
            }
        }
        else
        {
            pathFinder.maxSpeed = chasingSpeed;
        }

        if (staringTime <= 0 || player.inSafeRoom)
        {
            Debug.Log("Back to patrol");

            SelectNewDestination();
        }
    }

    void Death()
    {
        //TODO: turn on player controls again.
        target.position = spawnPoint.position;
        PlayerHealth.playerHealth -= 1;

        SelectNewDestination();
    }

    void CheckPlayerLineOfSight()
    {
        //cast a ray to see if velocimom has line of sight to player
        RaycastHit2D sightHit = Physics2D.Raycast(transform.position, target.position - transform.position, 10);

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

                ClearPlayerPathSpots();
            }
        }
    }

    void AddPlayerPathSpots()
    {
        GameObject instancedPath = Instantiate(playerPathSpots);
        instancedPath.transform.position = target.position;
        playerSpotsToFollow.Add(instancedPath);
    }

    public void ClearPlayerPathSpots()
    {
        losPathAt = 0;
        foreach (GameObject spots in playerSpotsToFollow)
        {
            Destroy(spots);
        }
        playerSpotsToFollow.Clear();
    }
}