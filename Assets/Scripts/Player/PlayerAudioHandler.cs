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
        4. FoodPickUp
        5. FoodSlip
        6. HugoAlright
        7. DropFood
     */

    public void PlayHugoFootstepSFX()
    {
        AudioSource instancedAudio = Instantiate(sfxAudioSource, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[Random.Range(0,2)];
        instancedAudio.volume = 0.3f;
        instancedAudio.pitch = Random.Range(0.8f,1.2f);
        instancedAudio.Play();
    }

    public void PlayHugoInhaleSFX()
    {
        AudioSource instancedAudio = Instantiate(sfxAudioSource, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[2];
        instancedAudio.volume = 0.05f;
        instancedAudio.pitch = Random.Range(0.9f, 1.1f);
        instancedAudio.Play();
    }

    public void PlayHugoExhaleSFX()
    {
        AudioSource instancedAudio = Instantiate(sfxAudioSource, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[3];
        instancedAudio.volume = 0.05f;
        instancedAudio.pitch = Random.Range(0.9f, 1.1f);
        instancedAudio.Play();
    }
    public void PlayFoodPickUpSFX()
    {
        AudioSource instancedAudio = Instantiate(sfxAudioSource, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[4];
        instancedAudio.volume = 0.2f;
        instancedAudio.pitch = Random.Range(0.9f, 1.1f);
        instancedAudio.Play();
    }
    public void PlayFoodSlipSFX()
    {
        AudioSource instancedAudio = Instantiate(sfxAudioSource, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[5];
        instancedAudio.volume = 0.1f;
        instancedAudio.pitch = Random.Range(0.8f, 1.2f);
        instancedAudio.Play();
    }
    public void PlayHugoAlrightSFX()
    {
        AudioSource instancedAudio = Instantiate(sfxAudioSource, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[6];
        instancedAudio.volume = 0.1f;
        instancedAudio.Play();
    }
    public void PlayDropFoodSFX()
    {
        AudioSource instancedAudio = Instantiate(sfxAudioSource, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[7];
        instancedAudio.volume = 0.1f;
        instancedAudio.Play();
    }
}