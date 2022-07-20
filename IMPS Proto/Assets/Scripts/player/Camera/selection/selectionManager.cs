using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectionManager : MonoBehaviour
{
    private weaponHandler handler;

    public bool Pickup;

    private void Start()
    {
        handler = gamemanager.instance.weaponHandler;
    }

    private void Update()
    {
        //RaycastHit hit;
        //
        //if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
        //{
        //    Transform selection = hit.transform;
        //    if (selection.CompareTag("Highlightable"))
        //    {             
        //        GameObject obj = selection.gameObject;
        //        var pick = obj.GetComponent<weaponPickup>();
        //        if (obj.GetComponent<weaponPickup>())
        //        {
        //            Pickup = true;
        //            if (Input.GetButtonDown("Pickup"))
        //            {
        //                 gamemanager.instance.weaponHandler.AddGun(pick.stats);                     
        //            }
        //        }
        //        else { Pickup = false; }
        //    }
        //}
        //else { Pickup = false; }
    }
}