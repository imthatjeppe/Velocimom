using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerDecption : MonoBehaviour
{
    public bool enemyLure;
    public Transform[] moveSpotsDeception;
    public GameObject velocimomGameObject;

    private VelocimomBehaviour velocimom;
    private AIDestinationSetter setDestination;
    private AIPath aIPath;
    private PlayerAudioHandler audioHandler;
    private bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        enemyLure = false;
        velocimom = GameObject.FindGameObjectWithTag("Enemy").GetComponent<VelocimomBehaviour>();
        setDestination = GameObject.FindGameObjectWithTag("Enemy").GetComponent<AIDestinationSetter>();
        aIPath = velocimomGameObject.GetComponent<AIPath>();
        audioHandler = GetComponent<PlayerAudioHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfCloseToLure();

        if (inRange)
        {
            TurnOn();
        }
    }
    public void Resume()
    {
        
        enemyLure = false;
        velocimom.patrol = true;
        setDestination.target = velocimom.moveSpots[velocimom.randomDestinationSpot];
    }

    void CheckIfCloseToLure()
    {
        if (enemyLure)
        {
            if (Vector3.Distance(velocimom.transform.position, moveSpotsDeception[0].position) < 0.2f)
            {
                Invoke(nameof(Resume), 1);
            }
        }
    }

    void startLure()
    {
        if (!enemyLure)
        {
            velocimom.patrol = true;
        }
        else
        {
            aIPath.maxSpeed = 0;
            Invoke(nameof(Luring), 3);
        }
    }
    void Luring()
    {
        setDestination.target = moveSpotsDeception[0];
        aIPath.maxSpeed = 1;
    }
    private void TurnOn()
    {
        if (Input.GetKeyDown(KeyCode.E) && velocimom.patrol)
        {
            enemyLure = true;
            startLure();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}