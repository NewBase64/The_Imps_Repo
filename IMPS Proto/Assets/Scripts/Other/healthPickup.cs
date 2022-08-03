using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    [Range(1, 50)] [SerializeField] int healthAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController player = other.GetComponent<playerController>();
            if (player.HP != player.HPOrig)
            {
                gamemanager.instance.playerScript.GiveHP(healthAmount);
                Destroy(gameObject);
            }
        }
    }
}
