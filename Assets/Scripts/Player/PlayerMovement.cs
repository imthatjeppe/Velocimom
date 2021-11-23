using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 10;
    float maxDistanceToMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
        maxDistanceToMove = speed * Time.deltaTime;
        Vector3 InputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 movementVector = InputVector * maxDistanceToMove;
        Vector3 newPosition = transform.position + movementVector;

    }
}
