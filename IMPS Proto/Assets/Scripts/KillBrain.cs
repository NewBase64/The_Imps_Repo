using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillBrain : MonoBehaviour
{
    public GameObject countdownCanvas;
    public TMP_Text roomText;
    public Countdown count;
    void Start()
    {
        countdownCanvas.SetActive(false);
    }

    private void OnDestroy()
    {
        roomText.text = "Escape";
        countdownCanvas.SetActive(true);
        count.StartCountdown();
    }
}
