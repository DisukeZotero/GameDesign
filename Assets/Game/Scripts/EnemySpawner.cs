using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyGO;               // The enemy prefab to spawn
    
    float maxSpawnRateInSeconds = 5f;        // Maximum time interval between enemy spawns

    void Start()
    {
        // Schedule the first enemy spawn after the initial delay
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        // Start increasing the spawn rate every 30 seconds
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    void Update()
    {
        // Additional logic can be added here if needed
    }

    void SpawnEnemy()
    {
        // Get the screen bounds in world coordinates
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // Instantiate the enemy at a random position at the top of the screen
        GameObject anEnemy = Instantiate(EnemyGO);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y); // Spawn at random x position, top y position

        // Schedule the next enemy spawn
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInNSeconds;
        // Determine the next spawn time based on the current maximum spawn rate
        if (maxSpawnRateInSeconds > 1f)
        {
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds); // Spawn within the range
        }
        else
        {
            spawnInNSeconds = 1f; // Ensure at least 1 second between spawns
        }
        Invoke("SpawnEnemy", spawnInNSeconds); // Schedule the next spawn
    }

    void IncreaseSpawnRate()
    {
        // Gradually decrease the maximum spawn rate, making enemies spawn more frequently
        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;
        
        // Stop increasing the spawn rate when it reaches 1 second
        if (maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    public void ScheduleEnemySpawner()
    {
        // Reset the spawn rate and schedule enemy spawns
        maxSpawnRateInSeconds = 5f;

        Invoke("SpawnEnemy", maxSpawnRateInSeconds); // Schedule the first enemy spawn
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f); // Start the spawn rate increase
    }

    public void UnscheduleEnemySpawner()
    {
        // Cancel any scheduled spawns
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}
