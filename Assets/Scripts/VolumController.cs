using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumController : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    private const string MusicKey = "MusicVolume";
    private const string SFXKey = "SFXVolume";

    void Start()
    {
        // cargamos los valores guardados a los sliders
        float musicValue = PlayerPrefs.GetFloat(MusicKey, 1f);
        float sfxValue = PlayerPrefs.GetFloat(SFXKey, 1f);

        musicSlider.value = musicValue;
        sfxSlider.value = sfxValue;

        SetMusicVolume(musicValue);
        SetSFXVolume(sfxValue);

        // hacemos un listener para los cambios en tiempo real
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }
    //modificamos el sonido de la musica
    public void SetMusicVolume(float value)
    {
        // con esto hacemos que podamos modificar los db, que van de 0 a -80
        //esta operacion nos cambia los valores del slider a db
        //lo ponemos a 0.0001 ya que a 0 da error
        mixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f);
        //guardamos los datos en playerprefs
        PlayerPrefs.SetFloat(MusicKey, value);
    }
    //modificamos el sonido de los efectos especiales
    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f);
        //guardamos los datos en playerprefs
        PlayerPrefs.SetFloat(SFXKey, value);
    }
}
