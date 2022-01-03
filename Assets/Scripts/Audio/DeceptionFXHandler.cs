using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeceptionFXHandler : MonoBehaviour
{
    public AudioClip[] audioClips;
    public float startVolume;
    public float endVolume;
    public GameObject sfxAudioSource;

    static bool oneDeceptionAlreadyActive = false;
    float oldVolume;
    bool audioIsPlaying = false;
    private AudioSource audioSource;
    AudioSource instancedAudio;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (inDeceptionRange && Input.GetKeyDown(KeyCode.E) && !audioIsPlaying && !oneDeceptionAlreadyActive)
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
        instancedAudio = Instantiate(sfxAudioSource, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        instancedAudio.clip = audioClips[0];
        instancedAudio.volume = startVolume;
        instancedAudio.Play();
        audioIsPlaying = true;
    }
    public void PlayDeceptionEndSFX()
    {
        instancedAudio.clip = audioClips[1];
        instancedAudio.volume = endVolume;
        instancedAudio.Play();
        audioIsPlaying = false;
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
