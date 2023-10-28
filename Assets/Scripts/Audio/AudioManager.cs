using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Sound Clips")]
    public AudioClipDatabase musicDatabase;
    public AudioClipDatabase sfxDatabase;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private void Awake()
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

        PlayMusic(AudioClipType.AmbientMusic);
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(AudioClipType clipType, int sfxIndex = -1)
    {
        var clip = sfxDatabase.GetClip(clipType, sfxIndex);

        if(clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayMusic(AudioClipType clipType, int musicIndex = -1)
    {
        var clip = musicDatabase.GetClip(clipType, musicIndex);

        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public AudioClip GetSFXClip(AudioClipType clipType, int sfxIndex = -1)
    {
        return sfxDatabase.GetClip(clipType, sfxIndex);
    }

    [ContextMenu("TestSFX")]
    public void TestSFX()
    {
        PlaySFX(AudioClipType.SFX);
    }

    [ContextMenu("TestMusic")]
    public void TestMusic()
    {
        PlayMusic(AudioClipType.BossMusic);
    }
}
