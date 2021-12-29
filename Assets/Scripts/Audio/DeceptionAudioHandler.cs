using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeceptionAudioHandler : MonoBehaviour
{
    public AudioClip[] audioClips;
    public float startVolume;
    public float endVolume;

    static bool oneDeceptionAlreadyActive = false;
    float oldVolume;
    private AudioSource audioSource;
    GameObject rigmor;
    private bool inDeceptionRange = false;
    private bool stopDeceptionSoundCanBePlayed = false;
    GameObject deceptionMoveSpot;
    DeceptionVFX deceptionVFX;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigmor = GameObject.FindGameObjectWithTag("Enemy");
        deceptionMoveSpot = transform.GetChild(0).gameObject;

        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<DeceptionVFX>() != null)
                deceptionVFX = transform.GetChild(i).GetComponent<DeceptionVFX>();
        }

        oldVolume = audioSource.volume;
        audioSource.volume *= Settings.volumeMagnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if (inDeceptionRange && Input.GetKeyDown(KeyCode.E) && !audioSource.isPlaying && !oneDeceptionAlreadyActive)
        {
            PlayDeceptionStartSFX();
            stopDeceptionSoundCanBePlayed = true;
            oneDeceptionAlreadyActive = true;
            TurnOnOffDeceptionVFX(true);
        }
        //Turning of the tap sfx when rigmor is in range of the tap
        if (Vector2.Distance(rigmor.transform.position, deceptionMoveSpot.transform.position) < 1 && stopDeceptionSoundCanBePlayed)
        {
            PlayDeceptionEndSFX();
            stopDeceptionSoundCanBePlayed = false;
            oneDeceptionAlreadyActive = false;
            TurnOnOffDeceptionVFX(false);
        }

        if (oldVolume * Settings.volumeMagnitude != audioSource.volume)
        {
            audioSource.volume = oldVolume * Settings.volumeMagnitude;
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
    void TurnOnOffDeceptionVFX(bool trunOn)
    {
        if (deceptionVFX == null)
        {
            return;
        }

        if (trunOn)
        {
            deceptionVFX.ActivateEffect();
        }
        else
        {
            deceptionVFX.DeactivateEffect();
        }
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
