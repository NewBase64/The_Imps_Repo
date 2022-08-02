using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landmine : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] GameObject explosion;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Instantiate(explosion, transform.position, explosion.transform.rotation);

            Destroy(gameObject);
        }
    }
}
