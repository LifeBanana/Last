using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//the enemy wave spawner for continually spawns most amount of enemies at a given time
public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public Transform[] spawnPoints;

    public int maxEnemies = 5;
    public float spawnInterval = 2.5f;

    public bool spawnOnStart = true;
    public int startingEnemies = 5;

    public bool randomEnemy = true;
    public bool randomSpawnPoint = true;

    private readonly List<GameObject> activeEnemies = new List<GameObject>();

    private Coroutine spawnRoutine;

    private void Start()
    {
        CleanupList();

        if (spawnOnStart)
        {
            int amount = Mathf.Min(startingEnemies, maxEnemies);

            for (int i = 0; i < amount; i++)
            {
                SpawnEnemy();
            }
        }

        spawnRoutine = StartCoroutine(SpawnRoutine());
    }
    //where the pawning happens
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            CleanupList();

            if (activeEnemies.Count < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }
    //spawns enemy
    public void SpawnEnemy()
    {
        CleanupList();

        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogError("EnemySpawner: No enemy prefabs assigned.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("EnemySpawner: No spawn points assigned.");
            return;
        }

        if (activeEnemies.Count >= maxEnemies)
        {
            return;
        }

        GameObject enemyPrefab = GetEnemyPrefab();
        Transform spawnPoint = GetSpawnPoint();

        if (enemyPrefab == null || spawnPoint == null)
            return;

        GameObject enemy = Instantiate( enemyPrefab, spawnPoint.position,  spawnPoint.rotation );

        activeEnemies.Add(enemy);

        Debug.Log(
            "Spawned enemy: " +
            enemy.name +
            " | Active enemies: " +
            activeEnemies.Count
        );
    }
    //get prefabs
    private GameObject GetEnemyPrefab()
    {
        if (randomEnemy)
        {
            return enemyPrefabs[
                Random.Range(0, enemyPrefabs.Length)
            ];
        }

        return enemyPrefabs[0];
    }
    //get spawn points
    private Transform GetSpawnPoint()
    {
        if (randomSpawnPoint)
        {
            return spawnPoints[
                Random.Range(0, spawnPoints.Length)
            ];
        }

        return spawnPoints[0];
    }
    //checks how many enemies
    private void CleanupList()
    {
        for (int i = activeEnemies.Count - 1; i >= 0; i--)
        {
            if (activeEnemies[i] == null)
            {
                activeEnemies.RemoveAt(i);
            }
        }
    }

    public int GetActiveEnemyCount()
    {
        CleanupList();

        return activeEnemies.Count;
    }

    public void SpawnWave(int amount)
    {
        CleanupList();

        int availableSlots = maxEnemies - activeEnemies.Count;

        int amountToSpawn = Mathf.Min(amount, availableSlots);

        for (int i = 0; i < amountToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    public void ClearEnemies()
    {
        for (int i = activeEnemies.Count - 1; i >= 0; i--)
        {
            if (activeEnemies[i] != null)
            {
                Destroy(activeEnemies[i]);
            }
        }

        activeEnemies.Clear();
    }

    private void OnDestroy()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
        }
    }
}