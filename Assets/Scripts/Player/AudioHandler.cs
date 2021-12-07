using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSoruce;

    public AudioClip[] audioClips;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayHugoFootstepSFX()
    {
        audioSoruce.clip = audioClips[Random.Range(0,2)];
        audioSoruce.volume = 0.1f;
        audioSoruce.pitch = Random.Range(0.8f,1.2f);
        audioSoruce.Play();
    }
}
