using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadePickup : MonoBehaviour
{
    private int gren;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gren = gamemanager.instance.weaponHandler.GiveGrenade(1);
            if (gren == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
