using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemy; // Array to store enemy prefabs
    public float respawnTime = 1.0f; // Time interval between spawns

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawner()); // Start the coroutine to spawn enemies repeatedly
    }

    // Update is called once per frame
    void Update()
    {
        // Not used for now, but can be utilized for other logic
    }

    // Coroutine to spawn enemies with a delay
    IEnumerator EnemySpawner()
    {
        while(true)
        {
            yield return new WaitForSeconds(respawnTime); // Wait for a specified time before spawning the next enemy
            SpawnEnemy(); // Call the method to spawn a new enemy
        }
    }

    // Method to spawn an enemy at a random position
    void SpawnEnemy()
    {
        int randomValue = Random.Range(0, enemy.Length); // Fixed: Changed "enemy.length" to "enemy.Length"
        float randomXpos = Random.Range(-2f, 2f); // Fixed: Changed "int randomXpos" to "float randomXpos" for more precise positioning
        Instantiate(enemy[randomValue], new Vector2(randomXpos, transform.position.y), Quaternion.identity); // Fixed: Changed "randomXPos" to "randomXpos" to match variable name
    }
}
