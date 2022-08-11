using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingPlatform : MonoBehaviour
{


    [SerializeField] float floorTimerToDeActivate;
    [SerializeField] float floorTimerToReActiivate;
    [SerializeField] GameObject platform;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("stepped on me");
            StartCoroutine(FloorDisAppearTime());
            StartCoroutine(FloorReAppearTime());
        }
    }
    IEnumerator FloorDisAppearTime()
    {
        yield return new WaitForSeconds(floorTimerToDeActivate);
        platform.SetActive(false);

    }
    IEnumerator FloorReAppearTime()
    {
        yield return new WaitForSeconds(floorTimerToReActiivate);
        platform.SetActive(true);

    }
}
