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
    [Header("---------------------------------------------------------------")]
    [Header("Grenade Color flash")]
    [SerializeField] Renderer rend;
    [SerializeField] Color startColor = Color.green;
    [SerializeField] Color endColor = Color.red;
    [SerializeField] float flashSpeed;

    void Start()
    {
        StartCoroutine(explosionTime());
        rb.velocity = (transform.forward * distance * speedMult) + (transform.up * ark * speedMult);
    }
    private void Update()
    {
        rend.material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * flashSpeed, 1));
    }
    IEnumerator explosionTime()
    {
        yield return new WaitForSeconds(timer);
        Instantiate(explosion, transform.position, explosion.transform.rotation);
        Destroy(gameObject);
    }
}
