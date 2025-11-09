using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    void Start()
    {
        musicSource.loop = true;
        musicSource.Play();
    }
}
