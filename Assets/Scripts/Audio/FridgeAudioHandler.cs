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
     * 2. BubbleIncrease
     * 3. BubblePop
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
        AudioSource instancedAudio = Instantiate(audioSourcePreFab, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[0];
        instancedAudio.volume = 0.3f;
        instancedAudio.Play();
    }
    public void PlayCloseFridgeSFX()
    {
        AudioSource instancedAudio = Instantiate(audioSourcePreFab, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[1];
        instancedAudio.volume = 0.3f;
        instancedAudio.Play();
    }
    public void PlayBubbleIncreaseSFX()
    {
        AudioSource instancedAudio = Instantiate(audioSourcePreFab, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[2];
        instancedAudio.pitch = Random.Range(0.9f, 1.1f);
        instancedAudio.volume = 0.5f;
        instancedAudio.Play();
    }
    public void PlayBubblePopSFX()
    {
        AudioSource instancedAudio = Instantiate(audioSourcePreFab, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[3];
        instancedAudio.volume = 0.5f;
        instancedAudio.Play();
    }
}
