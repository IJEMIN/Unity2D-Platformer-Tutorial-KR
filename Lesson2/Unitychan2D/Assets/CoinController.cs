using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

    public AudioClip coinSfx;

    void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(false);
        SoundEffectPlayer.GetInstance().PlaySFX(coinSfx);

        ScoreManager.GetInstance().score++;
    }
}
