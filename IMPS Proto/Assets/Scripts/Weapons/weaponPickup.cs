using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickup : MonoBehaviour
{
    public int gun;
    [SerializeField] GameObject notpistol;
    [SerializeField] GameObject rifle;

    [SerializeField] public int specifiedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        if (specifiedWeapon == 0)
        {
            Random.InitState((int)Time.time * 1000);
            gun = Random.Range(1, gamemanager.instance.numGuns);
        }
        else
            gun = specifiedWeapon;

        switch (gun)
        {
            case 1:
                notpistol.SetActive(true);
                break;
            case 2:
                rifle.SetActive(true);
                break;
        }
    }

    public void pickedUp()
    {
        Destroy(this);
    }
}
