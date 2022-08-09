using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPickup : MonoBehaviour
{
    [SerializeField] GameObject prompt;
    [SerializeField] int causedEvent;
    bool Picky;

    void Update()
    {
        if (Picky)
        {
            if (Input.GetButtonDown("Pickup"))
            {
                gamemanager.instance.CauseEvent(causedEvent);
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
