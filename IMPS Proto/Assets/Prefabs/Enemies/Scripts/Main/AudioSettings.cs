//CREATED

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
            gameObject.GetComponent<Slider>().value = AudioManager.instance.userSettings.SFXVolume;
            volume.text = Mathf.RoundToInt(AudioManager.instance.userSettings.SFXVolume * 100) + "%";
        }
        else
        {
            gameObject.GetComponent<Slider>().value = AudioManager.instance.userSettings.MusicVolume;
            volume.text = Mathf.RoundToInt(AudioManager.instance.userSettings.MusicVolume * 100) + "%";
        }
    }

    public void SetMusicVolume(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        volume.text = Mathf.RoundToInt(sliderValue * 100) + "%";
        AudioManager.instance.userSettings.MusicVolume = sliderValue;
    }

    public void SetSFXVolume(float sliderValue)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        volume.text = Mathf.RoundToInt(sliderValue * 100) + "%";
        AudioManager.instance.userSettings.SFXVolume = sliderValue;
    }
}
