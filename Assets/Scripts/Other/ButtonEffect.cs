using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonEffect : MonoBehaviour
{
    public float endPoint;
    public string choosePlayerDeception;
    public string chooseRoom;

    SpriteRenderer spritePicture;
    PlayerDecption playerDeception;
    DarkeningEffect darkeningEffect;
    
    void Start()
    {
        playerDeception = GameObject.FindGameObjectWithTag(choosePlayerDeception).GetComponent<PlayerDecption>();
        darkeningEffect = GameObject.FindGameObjectWithTag(chooseRoom).GetComponent<DarkeningEffect>();
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
