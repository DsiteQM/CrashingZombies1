using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    public GameObject zombiePrefab;
    public Transform[] spawnPoints;

    public float spawnInterval = 2f;

    void Start()
    {
        StartCoroutine(SpawnZombiesRoutine());
    }

   
    IEnumerator SpawnZombiesRoutine()
    {
        while (true)
        {
            SpawnZombies();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnZombies()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Vector3 randomSpawnPoint = spawnPoints[randomIndex].position;
        Instantiate(zombiePrefab, randomSpawnPoint, Quaternion.identity);
    }

    public void DecreaseZombieSpawnTime()
    {
        spawnInterval -= 0.5f;
        StartCoroutine(SpawnZombiesRoutine());
    }
}
