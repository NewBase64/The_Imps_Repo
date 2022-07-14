using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class gamemanager : MonoBehaviour
{
    [HideInInspector] public static gamemanager instance;
    public GameObject player;
    public playerController playerScript;


    public GameObject pauseMenu;
    public GameObject playerDeadMenu;
    public GameObject playerDamageFlash;
    public GameObject winGameMenu;
    public Image HPBar;
    public TMP_Text enemyDead;
    public TMP_Text enemyTotal;


    public int enemyKillGoal;
    int enemiesKilled;

    public bool paused = false;
    public GameObject menuCurrentlyOpen;
    [HideInInspector] public bool gameOver;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !gameOver)
        {

            if(!paused && !menuCurrentlyOpen)
            {
                paused = true;
                menuCurrentlyOpen = pauseMenu;
                menuCurrentlyOpen.SetActive(true);
                lockCursorPause();
            }
            else
            {
                resume();
            }
        }
    }

    public void resume()
    {
        paused = false;
        menuCurrentlyOpen.SetActive(false);
        menuCurrentlyOpen = null;
        unlockCursorUnpause();
    }

    public void restart()
    {
        gameOver = false;
        menuCurrentlyOpen.SetActive(false); ;
        menuCurrentlyOpen = null;
        unlockCursorUnpause();
    }

    public void playerDead()
    {
        gameOver = true;
        menuCurrentlyOpen = playerDeadMenu;
        menuCurrentlyOpen.SetActive(true);
        lockCursorPause();
    }
    public void checkEnemyKills()
    {
        enemiesKilled++;

        enemyDead.text = enemiesKilled.ToString("F0");

        if (enemiesKilled >= enemyKillGoal)
        {
            menuCurrentlyOpen = winGameMenu;
            menuCurrentlyOpen.SetActive(true);
            gameOver = true;
            lockCursorPause();
        }
    }

    void lockCursorPause()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    void unlockCursorUnpause()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void updateEnemyNumber()
    {
        enemyKillGoal++;
        enemyTotal.text = enemyKillGoal.ToString("F0");

    }
}
