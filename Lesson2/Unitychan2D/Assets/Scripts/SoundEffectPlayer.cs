using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectPlayer : MonoBehaviour {

    private static SoundEffectPlayer instance;

    public static SoundEffectPlayer GetInstance()
    {
        if(!instance)
        {
            instance =
                new GameObject("Sound Effect Player")
                .AddComponent<SoundEffectPlayer>();
        }
        return instance;
    }

    public void PlaySFX(AudioClip soundEffect)
    {
        Debug.Log(soundEffect);
        GetComponent<AudioSource>().PlayOneShot(soundEffect);
    }
}
