using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunctions : MonoBehaviour
{
    public void resume()
    {
        gamemanager.instance.resume();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void givePlayerHP(int amount)
    {
        gamemanager.instance.GiveHP(amount);
    }

    public void Respawn()
    {
        gamemanager.instance.playerScript.respawn();
        gamemanager.instance.restart();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gamemanager.instance.restart();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
}
