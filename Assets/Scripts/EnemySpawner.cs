using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs; //For different enemy types

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScale = 0.75f;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;


    private void Start()
    {
        StartWave();
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        //Spawn enemies every x amount of seconds if there are enemies left to spawn
        if (timeSinceLastSpawn >= (1f/ enemiesPerSecond) && enemiesLeftToSpawn > 0) 
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f; //Resets spawn time so enemies don't spawn every frame
        }
    }

    private void StartWave()
    {
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];
        //Spawns enemy at starting point created in LevelManager and keeps its rotation
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        Debug.Log("Spawn enemy");
    }

    private int EnemiesPerWave()
    {
        //baseEnemies * currentWave ^(power of) difficultyScale = enemies to spawn ------- Wave 2 example: 8 * 2 ^ 0.75 = 14 enemies
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScale)); //More enemies per wave
    }
}
