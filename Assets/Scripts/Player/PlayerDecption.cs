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

    private bool alreadyTurnedOn = false;

    void Start()
    {
        enemyLure = false;

        velocimom = GameObject.FindGameObjectWithTag("Enemy").GetComponent<VelocimomBehaviour>();
        setDestination = GameObject.FindGameObjectWithTag("Enemy").GetComponent<AIDestinationSetter>();
        aIPath = velocimomGameObject.GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            TurnOn();
        }else if(!velocimom.GetDetected() && alreadyTurnedOn)
        {
            TurnOn();
        }
    }

    public void Resume()
    {
        enemyLure = false;
        velocimom.SetEnemyLure(false);
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
        if (velocimom.patrol)
        {
            velocimom.patrol = false;
            enemyLure = true;
            velocimom.SetEnemyLure(true);
            alreadyTurnedOn = true;
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
        if (!velocimom.GetDetected())
        {
            setDestination.target = moveSpotsDeception[0];
            aIPath.maxSpeed = 1;
            Debug.Log("Luring");
            InvokeRepeating(nameof(ReachedDeception), 0f, 0.5f);
        }
        else
        {
            CancelInvoke(nameof(ReachedDeception));
        }

    }

    void ReachedDeception()
    {
        if (Vector2.Distance(velocimom.transform.position, moveSpotsDeception[0].position) < 0.2f)
        {
            Debug.Log("Should be resuming");
            alreadyTurnedOn = false;
            Invoke(nameof(Resume), 2);
            CancelInvoke(nameof(ReachedDeception));
        }
    }
}