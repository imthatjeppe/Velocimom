using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerDecption : MonoBehaviour
{
    public bool enemyLure;
    public Transform[] moveSpotsDeception;

    private VelocimomBehaviour velocimom;
    private AIDestinationSetter setDestination;
    // Start is called before the first frame update
    void Start()
    {
        enemyLure = false;
        velocimom = GameObject.FindGameObjectWithTag("Enemy").GetComponent<VelocimomBehaviour>();
        setDestination = GameObject.FindGameObjectWithTag("Enemy").GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyLure)
        {
            Invoke(nameof(Luring), 3);
            velocimom.patrol = false;

            if (Vector3.Distance(velocimom.transform.position, moveSpotsDeception[0].position) < 0.2f)
            {
                Invoke(nameof(Resume), 1);
            }
        }
    }
    public void Resume()
    {
        enemyLure = false;
        velocimom.patrol = true;
        setDestination.target = velocimom.moveSpots[velocimom.randomDestinationSpot];
    }
    void Luring()
    {
        setDestination.target = moveSpotsDeception[0];
    }
    private void TurnOn()
    {
        Debug.Log("Hello");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && velocimom.patrol)
        {
            TurnOn();
            enemyLure = true;
        }


    }
}
