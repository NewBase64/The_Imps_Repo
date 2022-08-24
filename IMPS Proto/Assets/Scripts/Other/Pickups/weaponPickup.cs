using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickup : MonoBehaviour
{
    public int ammo;
    public int ammoReserve;
    public weapon stats;
    [HideInInspector] public GameObject modle = null;
    [HideInInspector] public GameObject cloneMod = null;
    weaponHandler player;
    [SerializeField] GameObject prompt;
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip ammoPickupSound;

    bool Pick;

    // Start is called before the first frame update
    void Start()
    {   
        // if spawning from player, get the players info
        if (gamemanager.instance.weaponHandler.catchMe)
        {
            gamemanager.instance.weaponHandler.catchMe = false;
            stats = gamemanager.instance.weaponHandler.pubHolder;
            ammo = gamemanager.instance.weaponHandler.pubammo;
            ammoReserve = gamemanager.instance.weaponHandler.pubammoRes;
            modle = stats.model;
            cloneMod = Instantiate(modle, transform.position, transform.rotation);
            cloneMod.transform.parent = transform;
            cloneMod.SetActive(true);
            modle.SetActive(true);
            prompt.SetActive(false);
        }
    
        else // if not spawning from a player
        {
            if (stats == null) // if I'm not specified to be a certain gun 
                stats = gamemanager.instance.RandomWeapon(); // Randomise me

            if (ammo == 0) // if I don't have a specified amount of ammo   
                ammo = stats.ammo; // default ammo      
            if (ammoReserve == 0)  //repeat for reserve  
                ammoReserve = stats.ammoReserve;

            //set defaults
            modle = stats.model;
            cloneMod = Instantiate(modle, transform.position, transform.rotation);
            cloneMod.transform.parent = transform;
            cloneMod.SetActive(true);
            modle.SetActive(true);
            prompt.SetActive(false);
        }
        aud = AudioManager.instance.sfx;
    }

    private void Update()
    {
        // if I can/should be picked up
        if (Pick)
        {
            // if the player inputs
            if (Input.GetButtonDown("Pickup"))
            {
                gamemanager.instance.weaponHandler.AddGun(stats, ammo, ammoReserve); // give player my stuff
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // if player enters me
        {
            player = other.GetComponent<weaponHandler>(); // grab players weapon stuff
            weapon primary = player.GetPrimary();
            weapon secondary = player.GetSecondary();
            // if players has no weapon
            if (primary == null)
            {
                player.AddGun(stats, ammo, ammoReserve); // give him my stuff
                aud.PlayOneShot(ammoPickupSound);
                Destroy(gameObject);
            }
            // if players has no secondary weapon
            else if (secondary == null)
            {
                player.AddGun(stats, ammo, ammoReserve); // give him my stuff
                aud.PlayOneShot(ammoPickupSound);
                Destroy(gameObject);
            }
            // if player is holding the same gun
            else if (primary.model == modle || secondary.model == modle)
            {
                if (primary.model == modle)
                {
                    // if players weapons need ammo
                    if (player.GetAmmoReserve() != primary.ammoMax)
                    {
                        // give the player all my ammo and wait to get the ammount of ammo they took
                        int subammo = player.GiveAmmo(1, ammo + ammoReserve);
                        aud.PlayOneShot(ammoPickupSound);
                        // if I got ammo back
                        if (subammo != 0)
                        {
                            // if I take away all ammo from reserve
                            if (ammoReserve - subammo < 0)
                            {
                                subammo -= ammoReserve; // subtract the reserve
                                ammoReserve = 0; // remove the reserve 
                                ammo -= subammo; // take from ammo. if math is right, ammo should never be 0 or less 
                            }
                            else
                                ammoReserve -= subammo;
                        }
                        else // I didn't get ammo back, so I'm out of ammo                        
                            Destroy(gameObject);
                    }
                }
                else
                {
                    if (player.GetSecondaryAmmoReserve() != secondary.ammoMax) // repeat for secondary
                    {
                        int subammo = player.GiveAmmo(2, ammo + ammoReserve);
                        aud.PlayOneShot(ammoPickupSound);
                        if (subammo != 0)
                        {
                            if (ammoReserve - subammo < 0)
                            {
                                subammo -= ammoReserve;
                                ammoReserve = 0;
                                ammo -= subammo;
                            }
                            else
                                ammoReserve -= subammo;
                        }
                        else
                            Destroy(gameObject);
                    }
                }
            }
            else // player is allowed to pick me up
            {
                Pick = true;
                prompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // if player leaves me
        if (other.CompareTag("Player"))
        {
            prompt.SetActive(false);
            Pick = false;
        }
    }
}