using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineEffect : MonoBehaviour
{
    public Material outline;
    public Material noOutline;

    SpriteRenderer[] spriteRenderer;
    bool inInteractiveRange = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = new SpriteRenderer[2];
        spriteRenderer[0] = GetComponent<SpriteRenderer>();
        if (transform.GetChild(0).GetComponentInChildren<SpriteRenderer>() != null)
        {
            spriteRenderer[1] = transform.GetChild(0).GetComponentInChildren<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShutOffOutlineIfInteracting();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer[0].material = outline;
            if (spriteRenderer[1] != null)
                spriteRenderer[1].material = outline;

            inInteractiveRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            spriteRenderer[0].material = noOutline;
            if (spriteRenderer[1] != null)
                spriteRenderer[1].material = noOutline;

            inInteractiveRange = false;
        }
    }
    void ShutOffOutlineIfInteracting()
    {
        if (Input.GetKeyDown(KeyCode.E) && inInteractiveRange)
        {
            spriteRenderer[0].material = noOutline;
            if (spriteRenderer[1] != null)
                spriteRenderer[1].material = noOutline;
        }
        else if (Input.GetKeyUp(KeyCode.E) && inInteractiveRange)
        {
            spriteRenderer[0].material = outline;
            if (spriteRenderer[1] != null)
                spriteRenderer[1].material = outline;
        }
    }
}
