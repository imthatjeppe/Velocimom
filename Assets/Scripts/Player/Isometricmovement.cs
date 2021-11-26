using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Isometricmovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveH, moveY;
    [SerializeField] private float movespeed = 1.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        moveH = Input.GetAxis("Horizontal") * movespeed;
        moveY = Input.GetAxis("Vertical") * movespeed;
        rb.velocity = new Vector2(moveH, moveY);
     }

    


}
