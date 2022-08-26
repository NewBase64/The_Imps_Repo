using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] int numEnemiesToSpawn;
    [SerializeField] GameObject enemy;
    [SerializeField] int timer;
    [SerializeField] int assignedRoom;
    int spawnedEnemyNum;
    bool canSpawn = true;

    IEnumerator spawnEnemy()
    {
        canSpawn = false;
        GameObject spawnedEnemy = Instantiate(enemy, transform.position, enemy.transform.rotation);
        spawnedEnemy.GetComponent<enemyAI>().assignedRoom = assignedRoom;
        spawnedEnemyNum++;
        yield return new WaitForSeconds(timer);
        canSpawn = true;
    }
    void Update()
    {
        if (canSpawn && spawnedEnemyNum < numEnemiesToSpawn)
        {
            StartCoroutine(spawnEnemy());
        }
    }
}
