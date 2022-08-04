// CREATED

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPowerSource : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject[] lasers;
    [SerializeField] int hp;
    [SerializeField] GameObject explosion;

    public void takeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            for(int i = 0; i < lasers.Length; i++)
            {
                Destroy(lasers[i].transform.Find("LaserBeam").gameObject);
                lasers[i].transform.Find("LaserStart").GetComponent<ParticleSystem>().Stop();
            }
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            Destroy(gameObject);
        }
    }
}
