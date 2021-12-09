using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public GameObject dropOffZone;
    private Score scoreRef;


    private void Start()
    {
       scoreRef = dropOffZone.GetComponent<Score>();

        //Hämta score component
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey("D"))
        {
            //if (scoreRef.score > /*scoreRef.scoreNeededToWin*)
            //{
            //    //Ladda scen
            //}
        }
    }
}
