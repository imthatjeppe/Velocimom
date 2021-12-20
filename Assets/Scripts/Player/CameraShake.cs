using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject player;
    
    public float ShakeFrequancy = default;



    private void Start()
    {
       
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.R)) {
            Shake();
        }
    }

    private void Shake()
    {
      player.transform.position   =  Random.insideUnitSphere * ShakeFrequancy;
       
    }

    
}
