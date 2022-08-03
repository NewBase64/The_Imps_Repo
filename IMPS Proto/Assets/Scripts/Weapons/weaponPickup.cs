using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickup : MonoBehaviour
{
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
            stats = gamemanager.instance.weaponHandler.tempHolder;
            gamemanager.instance.weaponHandler.catchMe = false;
            modle = stats.model;
            cloneMod = Instantiate(modle, transform.position, transform.rotation);
            cloneMod.transform.parent = transform;
            modle.transform.Rotate(0, 90, 90);
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
                gamemanager.instance.weaponHandler.AddGun(stats);
                PickedUp();
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
                player.AddGun(stats);
                PickedUp();
            }
            else if (player.secondary == null)
            {
                player.AddGun(stats);
                PickedUp();
            }
            else
            {
                PickMeUp();
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
        Pick = true;
        prompt.SetActive(true);
    }

    public void PutMeDown()
    {
        prompt.SetActive(false);
        Pick = false;
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
