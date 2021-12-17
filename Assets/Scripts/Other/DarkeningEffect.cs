using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DarkeningEffect : MonoBehaviour
{
    public bool playerInRoom;

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
            transform.DOMoveZ(-7, 1).SetEase(Ease.OutBounce);
            playerInRoom = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.DOMoveZ(0, 1);
            playerInRoom = false;
        }
    }
}
