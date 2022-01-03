using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigmorAudioHandler : MonoBehaviour
{
    public AudioClip[] audioClips;

    [SerializeField]
    private AudioSource sfxAudioSourcePrefab;
    /*
     Audio clip list:
        0. RigmorVeloScream
        1. RigmorFootSteps
     */
    void Start()
    {

    }

    void Update()
    {

    }
    public void PlayRigmorDetectionRoarSFX()
    {
        AudioSource instancedAudio = Instantiate(sfxAudioSourcePrefab, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[0];
        instancedAudio.volume = 0.7f;
        instancedAudio.pitch = Random.Range(0.9f, 1.1f);
        instancedAudio.spatialBlend = 0.9f;
        instancedAudio.Play();

    }
    public void PlayRigmorFootstepsSFX()
    {
        AudioSource instancedAudio = Instantiate(sfxAudioSourcePrefab,transform.position,Quaternion.identity).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[1];
        instancedAudio.volume = 0.2f;
        instancedAudio.pitch = Random.Range(0.8f, 1.2f);
        instancedAudio.spatialBlend = 0.9f;
        instancedAudio.Play();

    }
}
