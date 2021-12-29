using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonEffect : MonoBehaviour
{
    public float endPoint;

    SpriteRenderer spritePicture;
    PlayerDecption playerDeception;
    DarkeningEffect darkeningEffect;
    
    void Start()
    {
        playerDeception = GameObject.FindGameObjectWithTag("Tap").GetComponent<PlayerDecption>();
        darkeningEffect = GameObject.FindGameObjectWithTag("Bathroom").GetComponent<DarkeningEffect>();
        spritePicture = GetComponent<SpriteRenderer>();

        transform.DOMoveY(endPoint, 1.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    void Update()
    {
        if (!playerDeception.enemyLure && darkeningEffect.playerInRoom)
        {
            spritePicture.enabled = true;
        }
        else
        {
            spritePicture.enabled = false;
        }
    }
}
