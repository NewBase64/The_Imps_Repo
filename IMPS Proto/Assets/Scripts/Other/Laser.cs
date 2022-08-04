// CREATED

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject laserStart;
    [SerializeField] GameObject laserEnd;
    LineRenderer laser;
    [SerializeField] int hp;
    [SerializeField] int dmg;

    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
        laser.startWidth = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        laser.SetPosition(0, laserStart.transform.position);
        laser.SetPosition(1, laserEnd.transform.position);
    }

    public void takeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            if (!gamemanager.instance.playerScript.takingDamage)
            {
                gamemanager.instance.playerScript.takingDamage = true;
                IDamageable isDamageable = other.GetComponent<IDamageable>();
                StartCoroutine(delay(isDamageable));
            }
        }
    }

    IEnumerator delay(IDamageable isDamageable)
    {
        isDamageable.takeDamage(dmg);
        yield return new WaitForSeconds(1);
        gamemanager.instance.playerScript.takingDamage = false;
    }
}
