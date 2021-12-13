using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAudioHandler : MonoBehaviour
{
    public GameObject sfxAudioSource;

    public AudioClip[] audioClips;
    /*
    Audio clip list:
       0. LightTurnOn
    */
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayLightTurnOnSFX()
    {
        AudioSource instancedAudio = Instantiate(sfxAudioSource).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[0];
        instancedAudio.volume = 0.2f * Settings.volumeMagnitude;
        instancedAudio.pitch = Random.Range(0.9f, 1.1f);
        instancedAudio.Play();
    }
}
