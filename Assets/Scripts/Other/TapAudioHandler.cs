using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapAudioHandler : MonoBehaviour
{
    public AudioClip[] audioClips;

    [SerializeField]
    private AudioSource audioSource;
    private bool inTapRange = false;
    /*
     Audio clip list:
        1. StartRunningTap
        2. RunningTapEnd
     */
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inTapRange && Input.GetKeyDown(KeyCode.E) && !audioSource.isPlaying)
        {
            PlayTapStartSFX();
        }
    }
    public void PlayTapStartSFX()
    {
        audioSource.clip = audioClips[0];
        audioSource.volume = 1f;
        audioSource.Play();

    }
    public void PlayTapEndSFX()
    {
        audioSource.clip = audioClips[1];
        audioSource.volume = 1f;
        audioSource.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            inTapRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTapRange = false;
        }
    }
}
