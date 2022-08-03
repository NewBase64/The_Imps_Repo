using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPickup : MonoBehaviour
{
    [Range(1, 50)][SerializeField] int AmmoAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            weaponHandler gun = other.GetComponent<weaponHandler>();
            if (gun.primary.ammo == gun.primary.ammoMax)
            {
                gamemanager.instance.playerScript.GiveHP(AmmoAmount);
                Destroy(gameObject);
            }
        }
    }
}
