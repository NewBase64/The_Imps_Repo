using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class softKillZone : MonoBehaviour
{
    bool oOB = false;
    public int countnum = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oOB = true;
            StartCoroutine(Count());
        }
    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(1);
        countnum--;
        if (countnum > 0 && oOB)
            StartCoroutine(Count());
        else if (oOB)
        {
            gamemanager.instance.playerScript.Teleport();
        }
        else
        {
            yield return new WaitForSeconds(5);
            countnum = 10;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oOB = false;
        }
    }
}
