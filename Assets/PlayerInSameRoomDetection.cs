using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInSameRoomDetection : MonoBehaviour
{
    VelocimomBehaviour velocimomBehaviour;
    DarkeningEffect darkFX;
    // Start is called before the first frame update
    void Start()
    {
        velocimomBehaviour = GameObject.FindGameObjectWithTag("Enemy").GetComponent<VelocimomBehaviour>();
        darkFX = GetComponent<DarkeningEffect>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("in "+gameObject.name);
            InvokeRepeating(nameof(UpdatePlayerInRoom),0,0.5f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            CancelInvoke(nameof(UpdatePlayerInRoom));
        }
    }
    void UpdatePlayerInRoom()
    {
        velocimomBehaviour.SetPlayerInSameRoom(darkFX.playerInRoom);
    }
}