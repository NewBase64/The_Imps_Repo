using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickup : MonoBehaviour
{
    public weapon stats;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(stats.model, transform.position, transform.rotation);
    }

    public void pickedUp()
    {
        Destroy(this);
    }
}
