using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerDecption : MonoBehaviour
{
    public bool waterTap;
    public Transform[] moveSpotsDeception;
    private VelocimomBehaviour velocimom;
    private AIDestinationSetter setDestination;
    // Start is called before the first frame update
    void Start()
    {
        waterTap = false;
        velocimom = GameObject.FindGameObjectWithTag("Enemy").GetComponent<VelocimomBehaviour>();
        setDestination = GameObject.FindGameObjectWithTag("Enemy").GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waterTap)
        {
            velocimom.patrol = false;
            setDestination.target = moveSpotsDeception[0];

           if(velocimom.transform.position == moveSpotsDeception[0].position)
            {
                waterTap = false;
                velocimom.patrol = true;
                setDestination.target = velocimom.moveSpots[velocimom.randomDestinationSpot];
            }
        }
        
    }


    private void TurnOn()
    {
 
            Debug.Log("Hello");
            
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TurnOn();
            waterTap = true;
        }
    }
}
