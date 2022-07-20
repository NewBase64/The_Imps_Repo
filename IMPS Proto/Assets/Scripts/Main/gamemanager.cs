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
    public List<weapon> weapons;
    [HideInInspector] public playerCamera cameraScript;

    [Header("----Menus----")]
    public GameObject pauseMenu;
    public GameObject playerDeadMenu;
    public GameObject playerDamageFlash;
    public GameObject winGameMenu;
    [Header("----UI Stuff----")]
    public Image HPBar;
    public TMP_Text enemyDead;
    public TMP_Text enemyTotal;
    [Header("----Crosshairs----")]
    [SerializeField] public GameObject CurrCrosshair;
    [SerializeField] GameObject NoCrosshair;
    [SerializeField] GameObject Crosshair1;
    [SerializeField] GameObject Crosshair2;
    [SerializeField] GameObject Crosshair3;
    [Header("----Numbers----")]
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

        // Set the camera Script from the main camera
        cameraScript = Camera.main.GetComponent<playerCamera>();

        // Set the current crosshair
        CurrCrosshair = NoCrosshair;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !gameOver)
        {
            if(!paused && !menuCurrentlyOpen)
            {
                paused = true;
                CurrCrosshair.SetActive(false);
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
        CurrCrosshair.SetActive(true);
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
        CurrCrosshair.SetActive(false);
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

    public void ChangeCrosshair(int crossnum)
    {
        switch (crossnum)
        { 
            case 0:
                CurrCrosshair.SetActive(false);
                break;
            case 1:
                CurrCrosshair.SetActive(false);
                CurrCrosshair = Crosshair1;
                CurrCrosshair.SetActive(true);
                break;
            case 2:
                CurrCrosshair.SetActive(false);
                CurrCrosshair = Crosshair1;
                CurrCrosshair.SetActive(true);
                break;
            case 3:
                CurrCrosshair.SetActive(false);
                CurrCrosshair = Crosshair1;
                CurrCrosshair.SetActive(true);
                break;
        }
    }
    public weapon RandomWeapon()
    {
        int index = Random.Range(0, weapons.Count);
        return weapons[index];
    }
}
