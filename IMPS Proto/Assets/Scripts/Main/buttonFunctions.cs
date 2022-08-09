using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonFunctions : MonoBehaviour
{
    public void resume()
    {
        AudioManager.instance.sfx.PlayOneShot(AudioManager.instance.confirm);
        gamemanager.instance.resume();
    }

    public void Quit()
    {
        AudioManager.instance.sfx.PlayOneShot(AudioManager.instance.confirm);
        Application.Quit();
    }

    public void givePlayerHP(int amount)
    {
        AudioManager.instance.sfx.PlayOneShot(AudioManager.instance.confirm);
        gamemanager.instance.GiveHP(amount);
    }

    public void Respawn()
    {
        AudioManager.instance.sfx.PlayOneShot(AudioManager.instance.confirm);
        gamemanager.instance.playerScript.respawn();
        gamemanager.instance.restart();
    }

    public void Restart()
    {
        AudioManager.instance.sfx.PlayOneShot(AudioManager.instance.confirm);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gamemanager.instance.restart();
    }

    public void StartGame(LevelLoader levelLoader)
    {
        MainMenu.instance.startButton.SetTrigger("Start");
        AudioManager.instance.sfx.PlayOneShot(AudioManager.instance.confirm);
        AudioManager.instance.music.Stop();
        AudioManager.instance.sfx.PlayOneShot(AudioManager.instance.startGame);
        levelLoader.StartCoroutine(levelLoader.Load(1));
    }

    public void CauseEvent(int causedEvent)
    {
        gamemanager.instance.CauseEvent(causedEvent);
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SettingsMenu()
    {
        AudioManager.instance.sfx.PlayOneShot(AudioManager.instance.confirm);
        gamemanager.instance.OpenSettings();
    }

    public void SettingsMenu2()
    {
        AudioManager.instance.sfx.PlayOneShot(AudioManager.instance.confirm);
        MainMenu.instance.settingsMenu.SetActive(true);
    }
}
