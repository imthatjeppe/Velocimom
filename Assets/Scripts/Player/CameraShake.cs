using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject player;
    public GameObject velocimom;

    private float ShakeFrequancy = 0.5f;

    private float ShakeTimeRemaining;

    private void Start()
    {
       
    }
    void Update()
    {
       
        if (Vector2.Distance(velocimom.transform.position, player.transform.position) < 6)
        {
            StartShake(0.2f, 0.007f);
            ShakeFrequancy += 0.01f * Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if(ShakeTimeRemaining > 0)
        {
            ShakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1) * ShakeFrequancy; 
            float yAmount = Random.Range(-1f, 1) * ShakeFrequancy;

            transform.position += new Vector3(xAmount, yAmount, 0);
        }

    }
    public void StartShake(float length, float power)
    {
        ShakeTimeRemaining = length;
        ShakeFrequancy = power;
    }


    
}
