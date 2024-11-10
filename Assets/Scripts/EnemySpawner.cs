using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs; //Array for different enemy types

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8; //Base number of enemies in the first wave
    [SerializeField] private float enemiesPerSecond = 0.5f; //Rate that enemies spawn (enemies per second)
    [SerializeField] private float timeBetweenWaves = 5f; //Time delay between waves
    [SerializeField] private float difficultyScale = 0.75f; //Scaling factor for increasing wave difficulty

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent(); //Event triggered when an enemy is destroyed

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed); //Adds a listener to detect when an enemy is destroyed
    }

    private void Start()
    {
        StartCoroutine(StartWave()); //Starts the first wave by running the Coroutine
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

        //If no more enemies are alive or left to spawn, end the current wave
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    //Decrease the enemiesAlive count when an enemy is destroyed
    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    //Coroutine to manage wave timing and start spawning after the time between waves
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves); //Wait for the time delay between waves
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave(); //Calculate how many enemies to spawn this wave
    }

    //Ends the current wave and prepares the next one
    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave()); //Start the next wave
    }

    //Spawns an enemy at the start position
    private void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length); //Random Enemy selection from array
        GameObject prefabToSpawn = enemyPrefabs[enemyIndex]; //Selects the enemy prefab
        //Spawns enemy at starting point created in LevelManager and keeps its rotation
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    //Calculate the number of enemies for the current wave based on the wave number and difficulty scaling
    private int EnemiesPerWave()
    {
        // Formula: baseEnemies * currentWave ^(power of) difficultyScale = enemies to spawn
        // Example: Wave 2 -> 8 * 2 ^ 0.75 = 13 enemies
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScale)); //More enemies per wave
    }
}
