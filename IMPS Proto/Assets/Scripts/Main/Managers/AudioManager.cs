using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource music;
    public AudioSource sfx;

    public AudioClip mainMenuMusic;
    public AudioClip confirm;
    public AudioClip startGame;
    public AudioClip scrolling;
    public AudioClip[] shieldDink;
    public AudioClip shieldRegen;
    public AudioClip danger;
    public AudioClip alarm;
    public AudioClip reloadVoice;

    public const string musicSettings = "MusicSettings";
    public const string sfxSettings = "SFXSettings";

    public AudioMixer mixer;
    private void Awake()
    {
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadVolume();
    }

    void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(musicSettings, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(sfxSettings, 1f);
        mixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
    }
}
