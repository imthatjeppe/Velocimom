using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRoom : MonoBehaviour
{
    public GameObject dropOffZone;

    public Timer timer;

    private void Start()
    {
        timer = dropOffZone.GetComponent<Timer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        timer.isTimeTicking = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        timer.isTimeTicking = true;
    }

}
