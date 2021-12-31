using System.Collections;
using System.Collections.Generic;
using UnityEngine.U2D;
using UnityEngine;
using DG.Tweening;

public class DarkeningEffect : MonoBehaviour
{
    public bool playerInRoom;

    public Color darkenedColor;
    public Color normalColor;

    SpriteRenderer[] furniture;
    LightAudioHandler lightSFX;
    SpriteShapeRenderer darkRoomSprite;

    private void Start()
    {
        lightSFX = GetComponent<LightAudioHandler>();
        darkRoomSprite = GetComponentInParent<SpriteShapeRenderer>();
        furniture = GetComponentsInChildren<SpriteRenderer>();

        darkenedColor = new Color(0.5f, 0.5f, 0.5f);
        normalColor = new Color(1, 1, 1);
    }
    private void Update()
    {
        PutLightScaleOnFurniture();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            lightSFX.PlayLightTurnOnSFX();
            darkRoomSprite.transform.DOMoveZ(-10, 0.01f);
            playerInRoom = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            darkRoomSprite.transform.DOMoveZ(0, 0.01f);
            playerInRoom = false;
        }
    }
    private void PutLightScaleOnFurniture()
    {
        if (playerInRoom)
        {
            foreach (SpriteRenderer furniture in furniture)
            {
                furniture.color = normalColor;
            }
        }
        else
        {
            foreach (SpriteRenderer furniture in furniture)
            {
                furniture.color = darkenedColor;
            }
        }
    }
}
