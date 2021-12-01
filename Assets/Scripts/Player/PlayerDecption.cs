using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDecption : MonoBehaviour
{
    private bool WaterTap = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void TurnOn()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            WaterTap = true;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TurnOn();
    }
}
