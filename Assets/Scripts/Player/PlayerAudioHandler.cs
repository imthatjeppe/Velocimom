using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{
    public GameObject sfxAudioSource;

    public AudioClip[] audioClips;
    /*
        Audio clip list:
        0. HugoFootstep2
        1. HugoFootstep3
        2. HugoInhale
        3. HugoExhale
     */

    public void PlayHugoFootstepSFX()
    {
        AudioSource instancedAudio = Instantiate(sfxAudioSource).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[Random.Range(0,2)];
        instancedAudio.volume = 0.7f * Settings.volumeMagnitude;
        instancedAudio.pitch = Random.Range(0.8f,1.2f);
        instancedAudio.Play();
    }

    public void PlayHugoInhaleSFX()
    {
        AudioSource instancedAudio = Instantiate(sfxAudioSource).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[2];
        instancedAudio.volume = 0.2f * Settings.volumeMagnitude;
        instancedAudio.pitch = Random.Range(0.9f, 1.1f);
        instancedAudio.Play();
    }

    public void PlayHugoExhaleSFX()
    {
        AudioSource instancedAudio = Instantiate(sfxAudioSource).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[3];
        instancedAudio.volume = 0.2f * Settings.volumeMagnitude;
        instancedAudio.pitch = Random.Range(0.9f, 1.1f);
        instancedAudio.Play();
    }

}