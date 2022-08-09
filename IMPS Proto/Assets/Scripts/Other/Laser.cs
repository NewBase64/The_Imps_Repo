// CREATED

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] GameObject laserStart;
    [SerializeField] GameObject laserEnd;
    LineRenderer laser;
    [SerializeField] int dmg;
    [SerializeField] int disableTimer;
    [SerializeField] int delayBetweenDisables;
    public bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
        laser.startWidth = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            laser.SetPosition(0, laserStart.transform.position);
            laser.SetPosition(1, laserEnd.transform.position);
            StartCoroutine(disableDelay());
        }

    }

    public void OnTriggerStay(Collider other)
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
        yield return new WaitForSeconds(0.5f);
        gamemanager.instance.playerScript.takingDamage = false;
    }

    IEnumerator disableDelay()
    {
        isActive = false;
        yield return new WaitForSeconds(delayBetweenDisables);
        StartCoroutine(disable());
    }

    IEnumerator disable()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<LineRenderer>().enabled = false;
        gameObject.transform.parent.transform.Find("LaserStart").GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(disableTimer);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<LineRenderer>().enabled = true;
        gameObject.transform.parent.transform.Find("LaserStart").GetComponent<ParticleSystem>().Play();
        isActive = true;
    }
}
