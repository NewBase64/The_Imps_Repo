using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class gamemanager : MonoBehaviour
{
    [HideInInspector] public static gamemanager instance;
    [HideInInspector] public GameObject player;
    [HideInInspector] public playerController playerScript;
    [HideInInspector] public weaponHandler weaponHandler;
    [HideInInspector] public playerCamera cameraScript;

    [Header("----Menus----")]
    public GameObject pauseMenu;
    public GameObject playerDeadMenu;
    public GameObject playerDamageFlash;
    public GameObject winGameMenu;
    [Header("----Stuff----")]
    public Image HPBar;
    public TMP_Text enemyDead;
    public TMP_Text enemyTotal;
    [Header("----Numbers----")]
    public int numGuns;
    public int enemyKillGoal;
    int enemiesKilled;

    [HideInInspector] public bool paused = false;
    [HideInInspector] public GameObject menuCurrentlyOpen;
    [HideInInspector] public bool gameOver;

    // Start is called before the first frame update
    void Awake()
    {
        // Set the public game manager for other scripts to access to this game manager
        instance = this;

        // Set the public player object to the player object in the scene
        player = GameObject.FindGameObjectWithTag("Player");

        // Set the public player script to the players playerController
        playerScript = player.GetComponent<playerController>();

        // Set the Weapons Handler script to the players WeaponsHandler
        weaponHandler = player.GetComponent<weaponHandler>();

        cameraScript = Camera.main.GetComponent<playerCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !gameOver)
        {
            if(!paused && !menuCurrentlyOpen)
            {
                paused = true;
                weaponHandler.CurrCrosshair.SetActive(false);
                menuCurrentlyOpen = pauseMenu;
                menuCurrentlyOpen.SetActive(true);
                ConLockCursor();
            }
            else
            {
                resume();
            }
        }
    }

    public void resume()
    {
        weaponHandler.CurrCrosshair.SetActive(true);
        paused = false;
        menuCurrentlyOpen.SetActive(false);
        menuCurrentlyOpen = null;
        LockCursor();
    }

    public void restart()
    {
        gameOver = false;
        menuCurrentlyOpen.SetActive(false); ;
        menuCurrentlyOpen = null;
        LockCursor();
    }

    public void playerDead()
    {
        gameOver = true;
        weaponHandler.CurrCrosshair.SetActive(false);
        menuCurrentlyOpen = playerDeadMenu;
        menuCurrentlyOpen.SetActive(true);
        ConLockCursor();
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
            ConLockCursor();
        }
    }

    public void GiveHP(int health)
    {
        playerScript.GiveHP(health);
    }

    public void ConLockCursor()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void LockCursor()
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
