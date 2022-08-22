using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{

    //[SerializeField] Animator transition;
    [SerializeField] GameObject platform;
    Vector3 origPosition;
    //[SerializeField] GameObject enemy;
    //bool canSpawn = true;
    //List<GameObject> enemies;

    private void Start()
    {
        origPosition = platform.transform.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        gamemanager.instance.player.transform.parent = GameObject.Find("ElevatorCollider").transform;
    }

    public void OnTriggerStay()
    {
        platform.transform.position += platform.transform.up * Time.deltaTime * 10;
        //if (canSpawn)
        //{
        //    StartCoroutine(spawnEnemy());
        //}
    }

    //IEnumerator spawnEnemy()
    //{
    //    canSpawn = false;
    //    GameObject spawnedEnemy = Instantiate(enemy, transform.position, enemy.transform.rotation);
    //    enemies.Add(spawnedEnemy);
    //    spawnedEnemy.transform.SetParent(platform.transform);
    //    spawnedEnemy.GetComponent<enemyAI>().assignedRoom = 12;
    //    yield return new WaitForSeconds(5);
    //    canSpawn = true;
    //}

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            platform.transform.position = origPosition;
            gameObject.transform.DetachChildren();
        }
        //for(int i = 0; i < enemies.Count; i++)
        //{
        //    Destroy(enemies[i]);
        //    enemies.RemoveAt(i);
        //}
    }

}
