using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemy; // Array to store enemy prefabs
    public float respawnTime = 2.0f; // Time interval between spawns
    public int enemySpawnCount = 10; // Number of enemies to spawn
    public GameController gameController; // Reference to the GameController script

    private bool lastEnemySpawned = false; // Tracks if the last enemy has been spawned

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawner()); // Start the coroutine to spawn enemies repeatedly
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the last enemy has been spawned and if no enemy exists in the scene
        if (lastEnemySpawned && FindObjectOfType<EnemyScript>() == null)
        {
            // Start a coroutine for the LevelComplete method from the GameController when all enemies are defeated
            StartCoroutine(gameController.LevelComplete());
        }
    }

    // Coroutine to spawn enemies with a delay
    IEnumerator EnemySpawner()
    {
        // Loop to spawn a specific number of enemies
        for (int i = 0; i < enemySpawnCount; i++)
        {
            yield return new WaitForSeconds(respawnTime); // Wait for the specified respawn time before spawning the next enemy
            SpawnEnemy(); // Call method to spawn a random enemy
        }

        lastEnemySpawned = true; // Set to true after all enemies are spawned
    }

    // Method to spawn an enemy at a random position
    void SpawnEnemy()
    {
        int randomValue = Random.Range(0, enemy.Length); // Randomly select an enemy prefab from the array
        float randomXpos = Random.Range(-2f, 2f); // Randomly select a position on the x-axis for spawning within a range of -2 to 2
        Instantiate(enemy[randomValue], new Vector2(randomXpos, transform.position.y), Quaternion.identity); // Instantiate the selected enemy at the specified position with no rotation
    }
}
