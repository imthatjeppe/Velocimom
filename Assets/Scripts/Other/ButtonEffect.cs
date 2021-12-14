using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonEffect : MonoBehaviour
{
    SpriteRenderer spritePicture;
    PlayerDecption playerDeception;
    DarkeningEffect darkeningEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        playerDeception = GameObject.FindGameObjectWithTag("Tap").GetComponent<PlayerDecption>();
        darkeningEffect = GameObject.FindGameObjectWithTag("DarkRoom").GetComponent<DarkeningEffect>();
        spritePicture = GetComponent<SpriteRenderer>();

        transform.DOMoveY(0.4f, 1.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
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
