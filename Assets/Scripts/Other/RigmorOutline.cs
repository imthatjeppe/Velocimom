using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigmorOutline : MonoBehaviour
{

    public Material redOutline;
    public Material yellowOutline;
    public Material greenOutline;

    public GameObject player;
    public SpriteRenderer rigmorSprite;

    private VelocimomBehaviour velocimom;
    // Start is called before the first frame update
    void Start()
    {
        rigmorSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(velocimom.transform.position, player.transform.position) < 25f)
        {
            rigmorSprite.material = yellowOutline;
        }

        if (Vector3.Distance(velocimom.transform.position, player.transform.position) < 10f)
        {
            rigmorSprite.material = redOutline;
        }

        else
        {
            rigmorSprite.material = greenOutline;
        }

    }
}
