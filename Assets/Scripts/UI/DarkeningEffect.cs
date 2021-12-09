using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DarkeningEffect : MonoBehaviour
{
    LightAudioHandler lightSFX;
    private void Start()
    {
        lightSFX = GetComponent<LightAudioHandler>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            lightSFX.PlayLightTurnOnSFX();
            transform.DOMoveZ(-12, 1).SetEase(Ease.OutBounce);
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
