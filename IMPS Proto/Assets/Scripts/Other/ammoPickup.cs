using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPickup : MonoBehaviour
{
    //[Range(1, 50)][SerializeField] int AmmoAmount;
    [SerializeField] bool fillAmmo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            weaponHandler gun = other.GetComponent<weaponHandler>();
            if (gun.primary != null)
            {
                if (fillAmmo)
                {
                    gamemanager.instance.weaponHandler.FillAmmo();
                }
                else 
                {
                    if (gun.primary.ammo != gun.primary.ammoMax)
                    {
                        gamemanager.instance.weaponHandler.GiveAmmo(1);
                        Destroy(gameObject);
                    }
                    if (gun.secondary != null)
                    {
                        if (gun.secondary.ammo != gun.secondary.ammoMax)
                        {
                            gamemanager.instance.weaponHandler.GiveAmmo(2);
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
    }
}
