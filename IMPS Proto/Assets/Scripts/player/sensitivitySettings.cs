using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class sensitivitySettings : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    private void Start()
    {
        if (gameObject.transform.parent.name == "Horizontal Sensitivity")
        {
            gameObject.GetComponent<Slider>().value = (PlayerPrefs.GetInt(playerCamera.HorizontalSensitibility, 100));
            text.text = PlayerPrefs.GetInt(playerCamera.HorizontalSensitibility).ToString();
        }
        else
        {
            gameObject.GetComponent<Slider>().value = (PlayerPrefs.GetInt(playerCamera.VerticalSensitibility, 400));
            text.text = PlayerPrefs.GetInt(playerCamera.VerticalSensitibility).ToString();
        }
    }

    private void OnDisable()
    {
        if (gameObject.transform.parent.name == "Horizontal Sensitivity")
        {
            PlayerPrefs.SetInt(playerCamera.HorizontalSensitibility, (int)gameObject.GetComponent<Slider>().value);
        }
        else
        {
            PlayerPrefs.SetInt(playerCamera.VerticalSensitibility, (int)gameObject.GetComponent<Slider>().value);
        }
    }

    public void SetHorizontalSensitivity(float sliderValue)
    {
        gamemanager.instance.cameraScript.sensHori = (int)sliderValue;
        text.text = gamemanager.instance.cameraScript.sensHori.ToString();
    }

    public void SetVerticalSensitivity(float sliderValue)
    {
        gamemanager.instance.cameraScript.sensVert = (int)sliderValue;
        text.text = gamemanager.instance.cameraScript.sensVert.ToString();
    }
}
