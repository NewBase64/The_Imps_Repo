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
    [HideInInspector] public GameObject mainCam;

    [Header("----Menus----")]
    public GameObject pauseMenu;
    public GameObject playerDeadMenu;
    public GameObject playerDamageFlash;
    public GameObject winGameMenu;
    public GameObject settingsMenu;
    [Header("----UI Stuff----")]
    public Image HPBar;
    public TMP_Text enemyDead;
    public TMP_Text enemyTotal;
    public Image ShieldBar; 
    public Image ShieldIndicator;
    public TMP_Text ammo;
    public TMP_Text ammoReserve;
    [Header("----Numbers----")]
    public int enemyKillGoal;
    int enemiesKilled;
    [Header("----Crosshairs----")]
    public GameObject currCrosshiar;
    public GameObject noCrosshiar;
    public GameObject Crosshiar1;
    public GameObject Crosshiar2;
    public GameObject Crosshiar3;
    public GameObject Crosshiar4;

    [HideInInspector] public LevelLoader levelLoader;
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

        mainCam = GameObject.FindGameObjectWithTag("MainCamera");

        // Set the public player script to the players playerController
        playerScript = player.GetComponent<playerController>();

        // Set the Weapons Handler script to the players WeaponsHandler
        weaponHandler = player.GetComponent<weaponHandler>();

        // Set the camera Script from the main camera
        cameraScript = Camera.main.GetComponent<playerCamera>();

        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !gameOver)
        {
            if (!paused && !menuCurrentlyOpen)
            {
                paused = true;
                weaponHandler.CurrCrosshair.SetActive(false);
                menuCurrentlyOpen = pauseMenu;
                menuCurrentlyOpen.SetActive(true);
                ConLockCursor();
            }
            else if (menuCurrentlyOpen == settingsMenu)
            {
                menuCurrentlyOpen.SetActive(false);
                menuCurrentlyOpen = pauseMenu;
                menuCurrentlyOpen.SetActive(true);
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
        menuCurrentlyOpen.SetActive(false);
        playerScript.shield.Restart();
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

    public void OpenSettings()
    {
        menuCurrentlyOpen.SetActive(false);
        menuCurrentlyOpen = settingsMenu;
        menuCurrentlyOpen.SetActive(true);
        ConLockCursor();
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

    public weapon RandomWeapon()
    {
        int index = Random.Range(1, weapons.Count);
        return weapons[index];
    }

    public void updateAmmoCount()
    {
        ammo.text = weaponHandler.GetAmmo().ToString();
        ammoReserve.text = weaponHandler.GetAmmoReserve().ToString();
    }

    public void changeCrosshair()
    {
        currCrosshiar.SetActive(false);
        currCrosshiar = weaponHandler.CurrCrosshair;
        currCrosshiar.SetActive(true);
    }

    public void CauseEvent(int causedEvent)
    {
        switch (causedEvent)
        {
            case 1:
               levelLoader.StartCoroutine(levelLoader.Load(2));
                break;
            case 2:
                levelLoader.StartCoroutine(levelLoader.Load(3));
                break;
            case 3:
                levelLoader.StartCoroutine(levelLoader.Load(4));
                break;
            case 4:
                restart();
                levelLoader.StartCoroutine(levelLoader.Load(5));
                break;
            default:
                break;
        }
    }
}
