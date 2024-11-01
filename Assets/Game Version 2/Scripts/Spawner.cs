using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies; // Array to store different enemy prefabs
    public GameObject[] minibosses; // Array to store different miniboss prefabs
    public float respawnTime = 2.0f; // Time interval between spawns
    public int enemiesPerWave = 5; // Number of enemies to spawn per wave
    public int totalWaves = 9; // Total number of waves to spawn

    public GameController gameController; // Reference to the GameController script

    private int currentWave = 0; // Tracks the current wave number
    private bool lastWaveSpawned = false; // Tracks if the last wave has been spawned
    private bool minibossSpawned = false; // Tracks if a miniboss has already been spawned

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaveSpawner()); // Start the coroutine to spawn waves of enemies
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the last wave has been spawned and if no enemies exist in the scene
        if (lastWaveSpawned && FindObjectsOfType<EnemyScript>().Length == 0 && FindObjectsOfType<MiniBossScript>().Length == 0)
        {
            // Start a coroutine for the LevelComplete method from the GameController when all enemies are defeated
            StartCoroutine(gameController.LevelComplete());
        }
    }

    // Coroutine to spawn enemies in waves
    IEnumerator WaveSpawner()
    {
        while (currentWave < totalWaves) // Continue spawning until all waves are done
        {
            yield return StartCoroutine(SpawnWave()); // Wait for the current wave to finish spawning
            currentWave++; // Move to the next wave
            yield return new WaitForSeconds(5f); // Wait before starting the next wave (adjust as needed)
        }

        lastWaveSpawned = true; // Set to true after all waves are spawned
    }

    // Coroutine to spawn a single wave of enemies
    IEnumerator SpawnWave()
    {
        minibossSpawned = false; // Reset miniboss spawn flag at the start of the wave

        for (int i = 0; i < enemiesPerWave; i++)
        {
            yield return new WaitForSeconds(respawnTime); // Wait for the specified respawn time before spawning the next enemy

            // Spawn minibosses only once in the last wave (wave 9)
            if (currentWave == totalWaves - 1 && !minibossSpawned) // Check if it's the last wave and miniboss hasn't spawned
            {
                SpawnMiniboss(); // Spawn a miniboss
                minibossSpawned = true; // Set the flag to true to prevent further miniboss spawns
            }
            else
            {
                SpawnEnemy(); // Call method to spawn a random enemy in other waves
            }
        }
    }

    // Method to spawn a random enemy at a random position
    void SpawnEnemy()
    {
        int randomValue = Random.Range(0, enemies.Length); // Randomly select an enemy prefab from the array
        float randomXpos = Random.Range(-10f, 10f); // Randomly select a position on the x-axis for spawning
        Instantiate(enemies[randomValue], new Vector2(randomXpos, transform.position.y), Quaternion.identity); // Instantiate the selected enemy at the specified position with no rotation
    }

    // Method to spawn a miniboss at a random position
    void SpawnMiniboss()
    {
        if (minibosses.Length > 0) // Check if there are minibosses available to spawn
        {
            int randomValue = Random.Range(0, minibosses.Length); // Randomly select a miniboss prefab from the array
            float randomXpos = Random.Range(-10f, 10f); // Randomly select a position on the x-axis for spawning
            Instantiate(minibosses[randomValue], new Vector2(randomXpos, transform.position.y), Quaternion.identity);
            Debug.Log("Miniboss spawned: " + minibosses[randomValue].name); // Debug log to verify spawn
        }
        else
        {
            Debug.LogWarning("No minibosses available to spawn."); // Log a warning if no minibosses are available
        }
    }
}
