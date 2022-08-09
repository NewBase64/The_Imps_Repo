using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGrenade : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speedMult;
    [SerializeField] float ark;
    [SerializeField] float distance;
    [SerializeField] int timer;
    [SerializeField] GameObject explosion;

    void Start()
    {
        StartCoroutine(explosionTime());
        rb.velocity = (transform.forward * distance * speedMult) + (transform.up * ark * speedMult);
    }
    IEnumerator explosionTime()
    {
        yield return new WaitForSeconds(timer);
        Instantiate(explosion, transform.position, explosion.transform.rotation);
        Destroy(gameObject);
    }
}
