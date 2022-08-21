using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jetpackPickup : MonoBehaviour
{
    [SerializeField] GameObject prompt;
    bool Picky;
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip jetpackPickupSound;

    private void Update()
    {
        // if I can/should be picked up
        if (Picky)
        {
            // if the player inputs
            if (Input.GetButtonDown("Pickup"))
            {
                gamemanager.instance.playerScript.jetpack = true; // give player my stuff
                aud.PlayOneShot(jetpackPickupSound);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            prompt.SetActive(true);
            Picky = true;
        }
    }

        private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            prompt.SetActive(false);
            Picky = false;
        }
    }
}
