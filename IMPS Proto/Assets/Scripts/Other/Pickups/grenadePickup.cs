using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadePickup : MonoBehaviour
{
    private int gren;
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip grenPickSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gren = gamemanager.instance.weaponHandler.GiveGrenade(1);
            if (gren == 0)
            {
                aud.PlayOneShot(grenPickSound);
                Destroy(gameObject);
            }
        }
    }
}
