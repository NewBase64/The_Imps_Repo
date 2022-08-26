using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillBrain : MonoBehaviour
{
    public GameObject countdownCanvas;
    public TMP_Text roomText;
    public Countdown count;
    public GameObject lightFlickerer;

    public void StartDestruction()
    {
        roomText.text = "Escape";
        GameObject canvas = Instantiate(countdownCanvas);
        count = canvas.transform.GetChild(0).GetComponent<Countdown>();
        count.StartCountdown();
        lightFlickerer.GetComponent<LightFlicker>().StartFlicker();
        AudioManager.instance.sfx.loop = true;
        AudioManager.instance.sfx.clip = AudioManager.instance.alarm;
        AudioManager.instance.sfx.Play();
        AudioManager.instance.sfx.PlayOneShot(AudioManager.instance.danger);
        GameObject.Find("Door9").SetActive(false);

    }
}
