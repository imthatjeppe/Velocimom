using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(x, y).normalized * Time.deltaTime * speed;

        transform.Translate(movement);

       /* if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed = 0;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            speed = 25;
        }
    }


}
