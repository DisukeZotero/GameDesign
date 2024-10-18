using UnityEngine;

public class GameController : MonoBehaviour
{
    public UIManager uiManager; // Reference to the UIManager
    public GameObject enemyPrefab; // Regular enemy prefab
    public GameObject miniBossPrefab; // Mini-boss prefab

    private float timer = 0f; // Timer to track survival time
    private int currentStage = 1; // Starting stage

    public float timeToNextStage = 5f; // Time in seconds to reach the next stage
    public float spawnWidth = 8f; // Define the spawn width
    public float spawnHeight = 5f; // Define the spawn height

    void Update()
    {
        timer += Time.deltaTime;

        // Check if the player has survived long enough to advance to the next stage
        if (timer >= timeToNextStage)
        {
            NextStage();
            timer = 0f; // Reset the timer after advancing to the next stage
        }
    }

    public void NextStage()
    {
        currentStage++;
        Debug.Log("Advancing to stage: " + currentStage);

        // Update the stage number in the UI
        if (uiManager != null)
        {
            uiManager.OnStageChanged(currentStage);
        }

        // Call a method to spawn enemies based on the current stage
        SpawnEnemies(currentStage);
    }

    private void SpawnEnemies(int stage)
    {
        int numberOfEnemiesToSpawn = stage; // Example: spawn 'stage' number of regular enemies

        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            if (stage >= 5 && i % 5 == 0) // Spawn a mini-boss every 5 enemies on stage 5 and above
            {
                SpawnMiniBoss();
            }
            else
            {
                SpawnEnemy(); // Regular enemy spawn
            }
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), Random.Range(-spawnHeight, spawnHeight), 0);
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Spawned an enemy at position: " + spawnPosition);
    }

    private void SpawnMiniBoss()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), Random.Range(-spawnHeight, spawnHeight), 0);
        GameObject miniBoss = Instantiate(miniBossPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Spawned a mini-boss at position: " + spawnPosition);
    }
}
