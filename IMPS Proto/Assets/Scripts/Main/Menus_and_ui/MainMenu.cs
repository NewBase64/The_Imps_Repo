using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public static MainMenu instance;
    //public Image loadingBar;
    //public Image loadingBackground;

    public Animator startButton;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
        AudioManager.instance.music.PlayOneShot(AudioManager.instance.mainMenuMusic);
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (settingsMenu.activeSelf)
            {
                AudioManager.instance.sfx.Stop();
                AudioManager.instance.sfx.PlayOneShot(AudioManager.instance.confirm);
                settingsMenu.SetActive(false);
            }
        }
    }
}
