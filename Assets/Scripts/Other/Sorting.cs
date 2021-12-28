using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]

public class Sorting : MonoBehaviour
{
    public Transform sortingPoint;
    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float y = sortingPoint != null ? sortingPoint.position.y : transform.position.y;
        rend.sortingOrder = 10000 - (int)(y * 3.14f);
    }
}
