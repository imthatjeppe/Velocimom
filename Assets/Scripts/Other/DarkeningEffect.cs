using System.Collections;
using System.Collections.Generic;
using UnityEngine.U2D;
using UnityEngine;
using DG.Tweening;

public class DarkeningEffect : MonoBehaviour
{
    public bool playerInRoom;

    LightAudioHandler lightSFX;
    SpriteShapeRenderer darkRoomSprite;

    private void Start()
    {
        lightSFX = GetComponent<LightAudioHandler>();
        darkRoomSprite = GetComponentInParent<SpriteShapeRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            lightSFX.PlayLightTurnOnSFX();
            darkRoomSprite.transform.DOMoveZ(-10, 1).SetEase(Ease.OutBounce);
            playerInRoom = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            darkRoomSprite.transform.DOMoveZ(0, 1);
            playerInRoom = false;
        }
    }
}
