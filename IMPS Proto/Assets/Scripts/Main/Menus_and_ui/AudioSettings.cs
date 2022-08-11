using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] TMP_Text volume;

    private void Start()
    {
        mixer = AudioManager.instance.mixer;
        if (gameObject.transform.parent.name == "Sound Effects")
        {
            gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat(AudioManager.sfxSettings, 1f);
            volume.text = Mathf.RoundToInt(PlayerPrefs.GetFloat(AudioManager.sfxSettings, 1f) * 100) + "%";
        }
        else
        {
            gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat(AudioManager.musicSettings, 1f);
            volume.text = Mathf.RoundToInt(PlayerPrefs.GetFloat(AudioManager.musicSettings, 1f) * 100) + "%";
        }
    }

    private void OnDisable()
    {
        if (gameObject.transform.parent.name == "Sound Effects")
        {
            PlayerPrefs.SetFloat(AudioManager.sfxSettings, gameObject.GetComponent<Slider>().value);
        }
        else
        {
            PlayerPrefs.SetFloat(AudioManager.musicSettings, gameObject.GetComponent<Slider>().value);
        }
    }

    public void SetMusicVolume(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        volume.text = Mathf.RoundToInt(sliderValue * 100) + "%";
    }

    public void SetSFXVolume(float sliderValue)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        volume.text = Mathf.RoundToInt(sliderValue * 100) + "%";
    }
}
