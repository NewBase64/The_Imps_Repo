using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        gamemanager.instance.playerScript.giveHP(amount);
    }

    public void Respawn()
    {
        gamemanager.instance.playerScript.respawn();
        gamemanager.instance.resume();
    }
}
