using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigmorAudioHandler : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSoruce;

    public AudioClip[] audioClips;
    /*
     Audio clip list:
        0. RigmorVeloScream
     */
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayRigmorDetectionRoarSFX()
    {
        audioSoruce.clip = audioClips[0];
        audioSoruce.volume = 1f;
        audioSoruce.pitch = Random.Range(0.8f, 1.2f);
        audioSoruce.Play();

    }
}
