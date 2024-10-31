using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAndObstacleSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0;
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x + 10, transform.position.y, Random.Range(-5, 5));
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.GetComponent<EnemyController>().target = GameObject.FindWithTag("Knight").transform;
    }
}


