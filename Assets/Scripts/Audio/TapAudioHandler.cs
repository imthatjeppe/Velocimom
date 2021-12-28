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
        rigmor = GameObject.FindGameObjectWithTag("Enemy");
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
        if(Vector2.Distance(rigmor.transform.position, transform.position) < 1 && stopTapSoundCanBePlayed)
        {
            PlayTapEndSFX();
            stopTapSoundCanBePlayed = false;
        }
    }
    public void PlayTapStartSFX()
    {
        AudioSource instancedAudio = Instantiate(audioSource, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[0];
        instancedAudio.volume = 0.05f;
        instancedAudio.Play();
    }
    public void PlayTapEndSFX()
    {
        AudioSource instancedAudio = Instantiate(audioSource, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[1];
        instancedAudio.volume = 0.05f;
        instancedAudio.Play();
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
