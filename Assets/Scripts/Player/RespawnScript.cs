using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{

    private VelocimomBehaviour velocimom;
    private 
    // Start is called before the first frame update
    void Start()
    {
        velocimom = GameObject.FindGameObjectWithTag("Enemy").GetComponent<VelocimomBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
