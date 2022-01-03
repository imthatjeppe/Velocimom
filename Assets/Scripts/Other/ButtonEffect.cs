using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonEffect : MonoBehaviour
{
    public float endPoint;
    public string choosePlayerDeception;
    public string chooseRoom;
    public GameObject interactableObject;

    SpriteRenderer spritePicture;
    PlayerDecption playerDeception;
    DarkeningEffect darkeningEffect;
    IInteractable interactable;
    
    void Start()
    {
        if (choosePlayerDeception.Equals(""))
        {
            interactable = interactableObject.GetComponent<IInteractable>();
        }
        else
        {
            playerDeception = GameObject.FindGameObjectWithTag(choosePlayerDeception).GetComponent<PlayerDecption>();
        }
        darkeningEffect = GameObject.FindGameObjectWithTag(chooseRoom).GetComponent<DarkeningEffect>();
        spritePicture = GetComponent<SpriteRenderer>();

        transform.DOMoveY(endPoint, 1.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    void Update()
    {
        if (CheckEnemyLure() && darkeningEffect.playerInRoom || CheckInteracting() && darkeningEffect.playerInRoom)
        {
            spritePicture.enabled = true;
        }
        else
        {
            spritePicture.enabled = false;
        }
    }
    bool CheckEnemyLure()
    {
        if(playerDeception == null)
        {
            return false;
        }
        return !playerDeception.enemyLure;
    }
    bool CheckInteracting()
    {
        if(interactable == null)
        {
            return false;
        }
        return !interactable.isInteracting();
    }
}
