using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerExplosion : MonoBehaviour
{
    [SerializeField] int playerDamage;
    [SerializeField] int explosionPushBack;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            gamemanager.instance.playerScript.pushback = (gamemanager.instance.player.transform.position - transform.position) * explosionPushBack;
            if (other.GetComponent<IDamageable>() != null)
            {
                IDamageable isDamagable = other.GetComponent<IDamageable>();
                isDamagable.takeDamage(playerDamage);
            }
        }
        StartCoroutine(explosionTime());
    }
    IEnumerator explosionTime()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}

