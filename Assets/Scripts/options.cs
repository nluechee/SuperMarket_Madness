using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class options : MonoBehaviour
{
    public Slider music;
    public Slider sfx;

    private void Awake()
    {
        music.value = PlayerPrefs.GetFloat("musicVol", 1);
        sfx.value = PlayerPrefs.GetFloat("sfxVol", 1);
    }

    private void Update()
    {

        if (Input.GetButtonDown("pause"))
        {
            gameObject.SetActive(false);
        }
    }
    public void Back()
    {
        gameObject.SetActive(false);
    }

    public void SetMusicVolume(float volume) 
    {
        FindObjectOfType<audio_manager>().SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        FindObjectOfType<audio_manager>().SetSFXVolume(volume);
    }
}
