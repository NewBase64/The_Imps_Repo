using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickup : MonoBehaviour
{
    public weapon stats;
    public GameObject mod;
    [SerializeField] GameObject prompt;

    // Start is called before the first frame update
    void Start()
    {
        if (stats == null)
        {
            stats = gamemanager.instance.RandomWeapon();
        }

        mod = stats.model;
        Instantiate(mod, transform.position, transform.rotation);
        mod.SetActive(true);
        prompt.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickMeUp();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Pickup"))
            {
                gamemanager.instance.weaponHandler.AddGun(stats);
                PickedUp();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PutMeDown();
        }
    }

    public void PickMeUp()
    {
        prompt.SetActive(true);
    }

    public void PutMeDown()
    {
        prompt.SetActive(false);
    }

    public void PickedUp()
    {
        mod.SetActive(false);
        Destroy(this.gameObject);
    }
}
