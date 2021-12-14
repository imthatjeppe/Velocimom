using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineEffect : MonoBehaviour
{
    public Material outline;
    public Material noOutline;

    SpriteRenderer spriteRenderer;
    PlayerDecption playerDeception;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerDeception = GameObject.FindGameObjectWithTag("Tap").GetComponent<PlayerDecption>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDeception.inRange && !playerDeception.enemyLure)
        {
            spriteRenderer.material = outline;
        }
        else
        {
            spriteRenderer.material = noOutline;
        }
    }
}
