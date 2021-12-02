using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DarkeningEffect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.DOMoveZ(-10, 1).SetEase(Ease.OutBounce);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.DOMoveZ(0, 1);
        }
    }
}
