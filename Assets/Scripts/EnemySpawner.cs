using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner main;

    [Header("Waves")]
    [SerializeField] private GameObject[] wave1;
    [SerializeField] private GameObject[] wave2;
    [SerializeField] private GameObject[] wave3;

    [Header("Attributes")]    
    [SerializeField] private float enemiesPerSecond = 0.5f; //Rate that enemies spawn (enemies per second)
    [SerializeField] private float timeBetweenWaves = 5f; //Time delay between waves

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent(); //Event triggered when an enemy is destroyed

    public int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    public bool lastWaveCompleted = false; // For starting next level

    private void Awake()
    {
        main = this; //Makes sure there is only one instance of LevelManager and makes it more easily accessible in other scripts
        onEnemyDestroy.AddListener(EnemyDestroyed); //Adds a listener to detect when an enemy is destroyed
    }

    private void Start()
    {
        StartCoroutine(StartWave()); //Starts the first wave by running the Coroutine
    }

    private void Update()
    {
        if (!isSpawning || lastWaveCompleted) return;

        timeSinceLastSpawn += Time.deltaTime;

        //Spawn enemies every x amount of seconds if there are enemies left to spawn
        if (timeSinceLastSpawn >= (1f/ enemiesPerSecond) && enemiesLeftToSpawn > 0) 
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f; //Resets spawn time so enemies don't spawn every frame
        }

        //If no more enemies are alive or left to spawn, end the current wave
        if (enemiesAlive <= 0 && enemiesLeftToSpawn <= 0)
        {
            EndWave();
        }
    }

    //Decrease the enemiesAlive count when an enemy is destroyed
    private void EnemyDestroyed()
    {
        if (enemiesAlive > 0)
        {
            enemiesAlive--;
        }

        // For testing since enemies are alive when next wave starts
        if (enemiesAlive <= 0 && enemiesLeftToSpawn <= 0)
        {
            EndWave();
        }
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
        if (lastWaveCompleted) return;

        ValidateEnemiesAlive(); //Ensure enemiesAlive is accurate

        if (enemiesAlive > 0) return; //Prevent wave ending before all enemies die

        isSpawning = false;
        timeSinceLastSpawn = 0f;

        if (currentWave >= 3)
        {
            Debug.Log("Finished Last Wave");
            lastWaveCompleted = true;
            LevelManager.main.LevelComplete();
            return;
        }

        currentWave++;
        StartCoroutine(StartWave()); //Start the next wave
    }

    private void SpawnEnemy()
    {
        // Get the enemy array for the current wave
        GameObject[] currentWaveArray = GetWaveArray();
        if (currentWaveArray == null || currentWaveArray.Length == 0) return; // Return if empty

        int enemyIndex = currentWaveArray.Length - enemiesLeftToSpawn; // Array Index
        GameObject prefabToSpawn = currentWaveArray[enemyIndex]; // Select enemy
        
        // Spawn enemy
        GameObject newEnemy = Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        if (newEnemy != null) 
        {
            enemiesAlive++; // Only increment if enemy is successfully created
        }

        enemiesLeftToSpawn--;
    }

    private int EnemiesPerWave()
    {
        GameObject[] currentWaveArray = GetWaveArray();
        
        if (currentWaveArray != null)
        {
            return currentWaveArray.Length;
        }
        else
        {
            return 0;
        }
    }

    private GameObject[] GetWaveArray()
    {
        // Returns wave array based on current wave number
        switch (currentWave)
        {
            case 1: return wave1;
            case 2: return wave2;
            case 3: return wave3;
            default: return null;
        }
    }

    private void ValidateEnemiesAlive() //Finds all spawned enemies using their Health script
    {
        enemiesAlive = FindObjectsOfType<Health>().Length - 1;
    }





    //[SerializeField] private GameObject[] enemyPrefabs; //Array for different enemy types

    //[SerializeField] private int baseEnemies = 8; //Base number of enemies in the first wave
    //[SerializeField] private float difficultyScale = 0.75f; //Scaling factor for increasing wave difficulty



    //Spawns an enemy at the start position
    // private void SpawnEnemy()
    // {
    //     int enemyIndex = Random.Range(0, enemyPrefabs.Length); //Random Enemy selection from array
    //     GameObject prefabToSpawn = enemyPrefabs[enemyIndex]; //Selects the enemy prefab
    //     //Spawns enemy at starting point created in LevelManager and keeps its rotation
    //     Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    // }

    // //Calculate the number of enemies for the current wave based on the wave number and difficulty scaling
    // private int EnemiesPerWave()
    // {
    //     // Formula: baseEnemies * currentWave ^(power of) difficultyScale = enemies to spawn
    //     // Example: Wave 2 -> 8 * 2 ^ 0.75 = 13 enemies
    //     return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScale)); //More enemies per wave
    // }
}
