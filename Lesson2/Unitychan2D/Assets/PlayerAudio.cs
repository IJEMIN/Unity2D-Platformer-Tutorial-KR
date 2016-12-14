using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudio : MonoBehaviour {
    public AudioClip damageVoice;
    public AudioClip jumpVoice;
    public AudioClip deathVoice;
    private AudioSource m_audioSource;
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    void OnDamage(int damageAmout)
    {
        PlayVoice(damageVoice);
    }

    void OnDeath()
    {
        PlayVoice(deathVoice);
    }

    void PlayVoice(AudioClip voice)
    {
        m_audioSource.clip = voice;
        m_audioSource.Play();
        Debug.Log(voice + "Played");
    }

}
