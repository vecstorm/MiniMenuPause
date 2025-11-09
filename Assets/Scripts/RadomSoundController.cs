using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadomSoundController : MonoBehaviour
{
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioClip[] clickSounds;

    public void PlayRandomClick()
    {
        if (clickSounds.Length == 0) return;

        int index = Random.Range(0, clickSounds.Length);
        sfxSource.PlayOneShot(clickSounds[index]);
    }
}
