//EDITED

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] int minutes;
    [SerializeField] int seconds;
    [SerializeField] TMP_Text timer;

    int currentMins;
    int currentSecs;

    public bool active = false;

    public void StartCountdown()
    {
        currentMins = minutes;
        currentSecs = seconds;
        //setUpUi();
        StartCoroutine(countdown());
    }

    IEnumerator countdown()
    {
        while (currentMins != 0 || currentSecs != 0)
        {
            setUpUi();
            yield return new WaitForSeconds(1);
            if (currentSecs > 0)
            {
                currentSecs--;
            }
            if (currentMins != 0 && currentSecs == 0)
            {
                currentSecs = 59;
                currentMins--;
            }
        }
        gamemanager.instance.playerDead();
        StopAllCoroutines();
        StartCountdown();
    }

    void setUpUi()
    {
        if (currentSecs < 10)
        {
            timer.text = currentMins + ":0" + currentSecs;
        }
        else
        {
            timer.text = currentMins + ":" + currentSecs;
        }
    }
}
