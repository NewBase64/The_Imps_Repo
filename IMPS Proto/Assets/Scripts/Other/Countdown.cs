using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] int minutes;
    [SerializeField] int seconds;
    [SerializeField] TMP_Text timer;

    private void Start()
    {
        setUpUi();
        StartCoroutine(countdown());
    }

    public void StartCountdown()
    {
        StartCoroutine(countdown());
    }

    IEnumerator countdown()
    {
        while (minutes != 0 && seconds != 0)
        {
            yield return new WaitForSeconds(1);
            if (seconds > 0)
            {
                seconds--;
            }
            if (minutes != 0 && seconds == 0)
            {
                seconds = 59;
                minutes--;
            }
            setUpUi();
        }
        gamemanager.instance.playerDead();
    }

    void setUpUi()
    {
        if (seconds < 10)
        {
            timer.text = minutes + ":0" + seconds;
        }
        else
        {
            timer.text = minutes + ":" + seconds;
        }
    }
}
