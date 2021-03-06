using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    [SerializeField] int damage;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gamemanager.instance.playerScript.pushback = (gamemanager.instance.player.transform.position - transform.position) * damage;
            if (other.GetComponent<IDamageable>() != null)
            {
                IDamageable isDamagable = other.GetComponent<IDamageable>();
                isDamagable.takeDamage(damage);

            }
        }
    }


}
