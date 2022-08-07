using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] int speed;
    [SerializeField] int timer;
    [SerializeField] GameObject explosion;
    public bool direction;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(explosionTime());
        if (direction)
            rb.velocity = -(gamemanager.instance.player.transform.forward + transform.position) + new Vector3(0, 0.5f, 0) * speed;
        else
            rb.velocity = (gamemanager.instance.player.transform.forward + transform.position) + new Vector3(0, 0.5f, 0) * speed;
    }
    IEnumerator explosionTime()
    {
        yield return new WaitForSeconds(timer);
        Instantiate(explosion, transform.position, explosion.transform.rotation);
        Destroy(gameObject);
    }
}