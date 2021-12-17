using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerDecption : MonoBehaviour
{
    public bool enemyLure;
    public bool inRange;

    public Transform[] moveSpotsDeception;
    public GameObject velocimomGameObject;

    private VelocimomBehaviour velocimom;
    private AIDestinationSetter setDestination;
    private AIPath aIPath;
    private PlayerAudioHandler audioHandler;

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
        if (inRange)
        {
            TurnOn();
        }
    }

    public void Resume()
    {
        Debug.Log("Resuming");
        enemyLure = false;

        velocimom.SelectNewDestination();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    private void TurnOn()
    {
        if (Input.GetKeyDown(KeyCode.E) && velocimom.patrol)
        {
            enemyLure = true;
            StartLure();
        }
    }

    void StartLure()
    {
        aIPath.maxSpeed = 0;
        Invoke(nameof(Luring), 3);
    }

    void Luring()
    {
        setDestination.target = moveSpotsDeception[0];
        aIPath.maxSpeed = 1;

        if (Vector2.Distance(velocimom.transform.position, moveSpotsDeception[0].position) < 5f)
        {
            Debug.Log("Should be resuming");
            Invoke(nameof(Resume), 2);
        }
    }

}