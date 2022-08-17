using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDoors : MonoBehaviour
{
    [SerializeField] Laser[] lasers;
    [SerializeField] GameObject[] walls;

    private void Start()
    {
        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i].transform.parent.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < walls.Length; i++)
            {
                walls[i].GetComponent<BoxCollider>().isTrigger = false;
            }
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].transform.parent.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < walls.Length; i++)
            {
                walls[i].GetComponent<BoxCollider>().isTrigger = true;
            }
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
