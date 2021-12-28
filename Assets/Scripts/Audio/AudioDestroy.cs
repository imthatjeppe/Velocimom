using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDestroy : MonoBehaviour
{
    float oldVolume;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        oldVolume = audioSource.volume;
        audioSource.volume *= Settings.volumeMagnitude;
    }
    void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying)
            Destroy(gameObject);

        if(oldVolume*Settings.volumeMagnitude != audioSource.volume)
        {
            audioSource.volume = oldVolume * Settings.volumeMagnitude;
        }
    }
}
