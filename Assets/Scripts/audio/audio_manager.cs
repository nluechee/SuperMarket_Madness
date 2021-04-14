using UnityEngine.Audio;
using UnityEngine;
using System;

public class audio_manager : MonoBehaviour
{
    public Sound[] sounds;
    public static audio_manager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        SetMusicVolume(PlayerPrefs.GetFloat("musicVol", 1));
        SetSFXVolume(PlayerPrefs.GetFloat("sfxVol", 1));
    }

    private void Start()
    {
        Play("music");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Stop();
    }

    public void SetMusicVolume(float volume) {
        Sound music = Array.Find(sounds, sound => sound.name == "music");
        if (music == null)
        {
            return;
        }
        music.source.volume = volume;
        PlayerPrefs.SetFloat("musicVol", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        foreach (Sound s in sounds)
        { 
            if (s.name != "music")
            {
                s.source.volume = volume;
            }
        }
        PlayerPrefs.SetFloat("sfxVol", volume);
        PlayerPrefs.Save();
    }

    public bool isPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return false;
        }
        if (s.source== null)
        {
            return false;
        }
        return s.source.isPlaying;
    }
}
