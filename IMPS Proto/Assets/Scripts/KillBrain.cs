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
    void Start()
    {
        countdownCanvas.SetActive(false);
    }

    private void OnDestroy()
    {
        roomText.text = "Escape";
        if (countdownCanvas != null)
        {
            countdownCanvas.SetActive(true);
            if (count.gameObject.activeSelf)
            {
                count.StartCountdown();
                lightFlickerer.GetComponent<LightFlicker>().StartFlicker();
                AudioManager.instance.sfx.loop = true;
                AudioManager.instance.sfx.clip = AudioManager.instance.alarm;
                AudioManager.instance.sfx.Play();
                AudioManager.instance.sfx.PlayOneShot(AudioManager.instance.danger);
            }
        }
    }
}
