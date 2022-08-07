//CREATED

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    //public GameObject audioMenu;
    public Volume userSettings;
    public AudioSource music;
    public AudioSource sfx;

    public AudioClip mainMenuMusic;
    public AudioClip confirm;
    public AudioClip startGame;
    public AudioClip scrolling;

    public AudioMixer mixer;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(userSettings.SFXVolume) * 20);
        mixer.SetFloat("MusicVolume", Mathf.Log10(userSettings.MusicVolume) * 20);
    }
}
