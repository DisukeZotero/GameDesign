using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject[] enemies; // Array to store different enemy prefabs
    public GameObject[] bosses; // Array to store different boss prefabs
    private bool bossSpawned = false; // Tracks if a boss has already spawned

    public float respawnTime = 2.0f; // Time interval between enemy spawns
    public int enemiesPerWave = 5; // Number of enemies to spawn per wave

    public GameController gameController; // Reference to the GameController script

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemiesContinuously()); // Start the coroutine to spawn enemies continuously
    }

    // Update is called once per frame
    void Update()
    {
        // Check if no enemies or bosses exist in the scene
        if (bossSpawned && FindObjectsOfType<EnemyScript>().Length == 0 && FindObjectsOfType<BossScript>().Length == 0)
        {
            // Start a coroutine for the LevelComplete method from the GameController
            StartCoroutine(gameController.LevelComplete());
        }
    }

    // Coroutine to spawn enemies continuously
    IEnumerator SpawnEnemiesContinuously()
    {
        while (true) // Infinite loop for continuous enemy spawning
        {
            yield return StartCoroutine(SpawnWave()); // Spawn a wave of enemies
            yield return new WaitForSeconds(5f); // Wait before starting the next wave
        }
    }

    // Coroutine to spawn a single wave of enemies
    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            yield return new WaitForSeconds(respawnTime); // Wait before spawning the next enemy

            // Spawn the boss as the last enemy of the wave if it hasn't been spawned yet
            if (i == enemiesPerWave - 1 && !bossSpawned)
            {
                SpawnBoss(); // Spawn the boss
                bossSpawned = true; // Prevent future boss spawns
            }
            else
            {
                SpawnEnemy(); // Spawn a regular enemy
            }
        }
    }

    // Method to spawn a random enemy at a random position
    void SpawnEnemy()
    {
        int randomValue = Random.Range(0, enemies.Length); // Randomly select an enemy prefab
        float randomXpos = Random.Range(-10f, 10f); // Randomly select a position on the x-axis
        Instantiate(enemies[randomValue], new Vector2(randomXpos, transform.position.y), Quaternion.identity); // Instantiate the selected enemy
    }

    // Method to spawn a boss at a random position
    void SpawnBoss()
    {
        int randomValue = Random.Range(0, bosses.Length); // Randomly select a boss prefab
        float randomXpos = Random.Range(-10f, 10f); // Randomly select a position on the x-axis
        Instantiate(bosses[randomValue], new Vector2(randomXpos, transform.position.y), Quaternion.identity); // Instantiate the selected boss
        Debug.Log("Boss spawned: " + bosses[randomValue].name); // Log the boss spawn for verification
    }
}
