using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeAudioHandler : MonoBehaviour
{
    public AudioClip[] audioClips;

    public GameObject audioSourcePreFab;

    /*
     * Audio Clips:
     * 0. OpenFridge
     * 1. CloseFridge
     */
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayOpenFridgeSFX()
    {
        AudioSource instancedAudio = Instantiate(audioSourcePreFab).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[0];
        instancedAudio.volume = 0.7f * Settings.volumeMagnitude;
        instancedAudio.Play();
    }
    public void PlayCloseFridgeSFX()
    {
        AudioSource instancedAudio = Instantiate(audioSourcePreFab).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[1];
        instancedAudio.volume = 0.7f * Settings.volumeMagnitude;
        instancedAudio.Play();
    }
}
