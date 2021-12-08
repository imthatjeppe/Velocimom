using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapAudioHandler : MonoBehaviour
{
    public AudioClip[] audioClips;


    [SerializeField]
    private AudioSource audioSource;
    GameObject rigmor;
    private bool inTapRange = false;
    private bool stopTapSoundCanBePlayed = false;
    /*
     Audio clip list:
        1. StartRunningTap
        2. RunningTapEnd
     */
    void Start()
    {
        rigmor = GetComponent<PlayerDecption>().velocimomGameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(inTapRange && Input.GetKeyDown(KeyCode.E) && !audioSource.isPlaying)
        {
            PlayTapStartSFX();
            stopTapSoundCanBePlayed = true;
        }
        //Turning of the tap sfx when rigmor is in range of the tap
        if(Vector2.Distance(rigmor.transform.position, transform.position) < 0.2 && stopTapSoundCanBePlayed)
        {
            PlayTapEndSFX();
            stopTapSoundCanBePlayed = false;
        }
    }
    public void PlayTapStartSFX()
    {
        audioSource.clip = audioClips[0];
        audioSource.volume = 0.5f;
        audioSource.Play();

    }
    public void PlayTapEndSFX()
    {
        audioSource.clip = audioClips[1];
        audioSource.volume = 0.5f;
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
