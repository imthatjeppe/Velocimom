using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSoruce;

    public AudioClip[] audioClips;
    /*
     Audio clip list:
        0. HugoFootstep2
        1. HugoFootstep3
        2. HugoInhale
        3. HugoExhale
     */
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
    public void PlayHugoInhaleSFX()
    {
        audioSoruce.clip = audioClips[2];
        audioSoruce.volume = 0.03f;
        audioSoruce.pitch = Random.Range(0.9f, 1.1f);
        audioSoruce.Play();
    }
    public void PlayHugoExhaleSFX()
    {
        audioSoruce.clip = audioClips[3];
        audioSoruce.volume = 0.03f;
        audioSoruce.pitch = Random.Range(0.9f, 1.1f);
        audioSoruce.Play();
    }
}
