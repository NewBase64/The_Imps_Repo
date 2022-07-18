using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuns : MonoBehaviour
{
    [Header("Normal Components")]
    [SerializeField] int damage;
    [SerializeField] int speed;
    [SerializeField] Rigidbody rb;
    [SerializeField] int destroyTime;
    [SerializeField] GameObject hitEffect;
    enum bulletType
    {
        normal,
        slow,
        explosive,
    }

    [SerializeField] bulletType type;

    [Header("Slow Components")]
    [SerializeField] int slowTimer;
    [SerializeField] float slowValue;

    [Header("Explosive Components")]
    [SerializeField] int timer;
    [SerializeField] GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case bulletType.normal:
                rb.velocity = (gamemanager.instance.player.transform.position - transform.position).normalized * speed;
                Destroy(gameObject, destroyTime);
                break;
            case bulletType.slow:
                rb.velocity = (gamemanager.instance.player.transform.position - transform.position).normalized * speed;
                Destroy(gameObject, destroyTime);
                break;
            case bulletType.explosive:
                rb.velocity = (gamemanager.instance.player.transform.position - transform.position) + new Vector3(0, 0.5f, 0) * speed;
                StartCoroutine(explosionTime());
                break;
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        switch (type)
        {
            case bulletType.normal:
                if (other.GetComponent<IDamageable>() != null)
                {
                    IDamageable isDamageable = other.GetComponent<IDamageable>();
                    isDamageable.takeDamage(damage);
                }
                Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                Destroy(gameObject);
                break;
            case bulletType.slow:
                if (other.GetComponent<IDamageable>() != null)
                {
                    IDamageable isDamageable = other.GetComponent<IDamageable>();
                    isDamageable.takeDamage(damage);
                    if (!gamemanager.instance.playerScript.slowed)
                    {
                        gamemanager.instance.playerScript.StartCoroutine(gamemanager.instance.playerScript.slowPlayer(slowTimer, slowValue));
                    }
                }
                Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                Destroy(gameObject);
                break;
            case bulletType.explosive:
                break;
        }
    }

    IEnumerator explosionTime()
    {
        yield return new WaitForSeconds(timer);
        Instantiate(explosion, transform.position, explosion.transform.rotation);
        Destroy(gameObject);
    }

}
