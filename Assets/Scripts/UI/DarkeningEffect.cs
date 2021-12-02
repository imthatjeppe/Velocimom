using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkeningEffect : MonoBehaviour
{
    public float startShaderTimer;

    private float shaderTimer;

    private SpriteRenderer myPicture;

    private void Start()
    {
        myPicture = GetComponent<SpriteRenderer>();

        shaderTimer = startShaderTimer;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            myPicture.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            myPicture.enabled = true;
        }
    }
}
