using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPickup : MonoBehaviour
{
    //[Range(1, 50)][SerializeField] int AmmoAmount;
    [SerializeField] bool fillAmmo;
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip ammoPickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            weaponHandler player = other.GetComponent<weaponHandler>();
            weapon primary = player.GetPrimary();
            weapon secondary = player.GetSecondary();
            if (primary != null)
            {
                if (fillAmmo)
                {
                    gamemanager.instance.weaponHandler.FillAmmo();
                    Destroy(gameObject);
                }
                else 
                {
                    if (player.GetAmmoReserve() != primary.ammoMax)                    
                        player.GiveAmmo(1);                        
                    
                    if (secondary != null)
                    {
                        if (player.GetSecondaryAmmoReserve() != secondary.ammoMax)                        
                            player.GiveAmmo(2);                                                    
                    }
                    gamemanager.instance.weaponHandler.GiveGrenade(2);
                    aud.PlayOneShot(ammoPickupSound);
                    Destroy(gameObject);
                }
            }
        }
    }
}