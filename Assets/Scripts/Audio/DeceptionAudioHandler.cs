using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeceptionAudioHandler : MonoBehaviour
{
    public AudioClip[] audioClips;
    public float startVolume;
    public float endVolume;


    private AudioSource audioSource;
    GameObject rigmor;
    private bool inDeceptionRange = false;
    private bool stopDeceptionSoundCanBePlayed = false;
    GameObject deceptionMoveSpot;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigmor = GameObject.FindGameObjectWithTag("Enemy");
        deceptionMoveSpot = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (inDeceptionRange && Input.GetKeyDown(KeyCode.E) && !audioSource.isPlaying)
        {
            PlayDeceptionStartSFX();
            stopDeceptionSoundCanBePlayed = true;
        }
        //Turning of the tap sfx when rigmor is in range of the tap
        if (Vector2.Distance(rigmor.transform.position, deceptionMoveSpot.transform.position) < 1 && stopDeceptionSoundCanBePlayed)
        {
            PlayDeceptionEndSFX();
            stopDeceptionSoundCanBePlayed = false;
        }
    }
    public void PlayDeceptionStartSFX()
    {
        audioSource.clip = audioClips[0];
        audioSource.volume = startVolume;
        audioSource.Play();
    }
    public void PlayDeceptionEndSFX()
    {
        audioSource.clip = audioClips[1];
        audioSource.volume = endVolume;
        audioSource.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inDeceptionRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inDeceptionRange = false;
        }
    }
}
