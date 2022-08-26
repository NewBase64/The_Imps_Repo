using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landmine : MonoBehaviour, IDamageable
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [Range(0,.5f)][SerializeField] float landeminDelayExplosion;
    [SerializeField] GameObject explosion;
    [SerializeField] int hp;
    [SerializeField] bool isBrain = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(delayExplosion());
        }
    }
    public void takeDamage(int dmg)
    {
        hp -= dmg;


        if (hp <= 0)
        {
            foreach (Collider col in GetComponents<Collider>())
                col.enabled = false;
            StartCoroutine(delayExplosion());

        }
    }
    IEnumerator delayExplosion()
    {
        yield return new WaitForSeconds(landeminDelayExplosion);
        Instantiate(explosion, transform.position, explosion.transform.rotation);
        rb.velocity = (gamemanager.instance.player.transform.position - transform.position) + new Vector3(0, 0.5f, 0) * speed;
        if(isBrain)
        {
            gameObject.GetComponent<KillBrain>().StartDestruction();
        }
        Destroy(gameObject);

    }
}
