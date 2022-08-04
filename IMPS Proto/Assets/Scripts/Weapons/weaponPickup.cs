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

    [SerializeField] bool Pick;

    // Start is called before the first frame update
    void Start()
    {
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
        else
        {
            if (stats == null)
            {
                stats = gamemanager.instance.RandomWeapon();
            }
            ammo = stats.ammo;
            ammoReserve = stats.ammoReserve;
            modle = stats.model;
            cloneMod = Instantiate(modle, transform.position, transform.rotation);
            cloneMod.transform.parent = transform;
            cloneMod.SetActive(true);
            modle.SetActive(true);
            prompt.SetActive(false);
        }
    }

    private void Update()
    {
        if (Pick)
        {
            if (Input.GetButtonDown("Pickup"))
            {
                if (gamemanager.instance.weaponHandler.primary.model != stats.model && gamemanager.instance.weaponHandler.secondary.model != stats.model)
                {
                    gamemanager.instance.weaponHandler.AddGun(stats, ammo, ammoReserve);
                    PickedUp();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<weaponHandler>();
            if (player.primary == null)
            {
                player.AddGun(stats, ammo, ammoReserve);
                PickedUp();
            }
            else if (player.secondary == null)
            {
                player.AddGun(stats, ammo, ammoReserve);
                PickedUp();
            }
            else
            {
                Pick = true;
                prompt.SetActive(true);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            prompt.SetActive(false);
            Pick = false;
        }
    }

    public void PickedUp()
    {
        modle.SetActive(false);
        //Destroy(cloneMod);
        Destroy(gameObject);
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        if (Input.GetButtonDown("Pickup"))
    //        {
    //            Debug.Log("Called");
    //            gamemanager.instance.weaponHandler.AddGun(stats);
    //            PickedUp();
    //        }
    //    }
    //}
}
