using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject FollowPlayer;

    public float followSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float interpolation = followSpeed * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, FollowPlayer.transform.position.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, FollowPlayer.transform.position.x, interpolation);
        
        this.transform.position = position;
    }
}
