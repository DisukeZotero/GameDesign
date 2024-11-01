using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public Transform[] gunPoint; // Points from where the boss will shoot
    public GameObject enemyBullet; // Bullet prefab for the boss
    public float enemyBulletSpawnTime = 0.5f; // Time between enemy bullets
    public HealthBar healthbar; // Health bar for the boss
    public float speed = 1f; // Movement speed of the boss
    public float health = 20f; // Health of the boss
    public GameObject coinPrefab; // Prefab for coin drop
    public GameObject upgradePrefab; // Prefab for upgrade drop
    public GameObject healthBuffPrefab; // Prefab for health buff drop

    [Range(0f, 1f)]
    public float healthBuffDropRate = 0.15f; // Drop rate for health buff
    [Range(0f, 1f)]
    public float upgradeDropRate = 0.1f; // Drop rate for upgrade
    [Range(0f, 1f)]
    public float coinDropRate = 0.3f; // Drop rate for coins

    private float barSize = 1f; // Size of the health bar
    private float damage; // Damage value per health unit
    private bool isMovingHorizontally = false; // Flag to track horizontal movement
    private Vector2 initialPosition; // Initial position of the boss
    private float yPosition; // Store the y-axis position after moving down
    private float targetX; // Target x position for left/right movement
    private float moveSpeed = 2f; // Speed of horizontal movement
    private float moveRange = 3f; // Distance to move left and right

    void Start()
    {
        initialPosition = new Vector2(0f, transform.position.y);
        transform.position = initialPosition; 
        StartCoroutine(EnemyShooting()); // Start shooting
        damage = barSize / health; 
        StartCoroutine(MoveDownAndThenHorizontal()); // Start movement coroutine
    }

    void Update()
    {
        if (isMovingHorizontally)
        {
            MoveLeftRight(); // Move left and right if enabled
        }
    }

    IEnumerator EnemyShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemyBulletSpawnTime);
            EnemyFire(); // Fire bullets
        }
    }

    void EnemyFire()
    {
        for (int i = 0; i < gunPoint.Length; i++)
        {
            Instantiate(enemyBullet, gunPoint[i].position, Quaternion.identity);
        }
    }

    IEnumerator MoveDownAndThenHorizontal()
    {
        float moveDownDistance = 3f; 
        float moveDuration = 1f; 
        Vector2 targetPosition = new Vector2(initialPosition.x, initialPosition.y - moveDownDistance);

        // Move down
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector2.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; 
        yPosition = transform.position.y; 
        yield return new WaitForSeconds(0.5f); 

        isMovingHorizontally = true; // Start moving horizontally
        targetX = 0f; // Start at the center
    }

    void MoveLeftRight()
    {
        targetX = Mathf.PingPong(Time.time * moveSpeed, moveRange * 2) - moveRange; 
        float smoothX = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * moveSpeed); 
        transform.position = new Vector2(smoothX, yPosition); 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            DamageHealthbar(); // Apply damage to the health bar
            Destroy(collision.gameObject); // Destroy the player's bullet
            Debug.Log($"Boss hit! Current health: {health}"); 
            if (health <= 0)
            {
                HandleDrops(); // Handle drops upon defeat
                DestroyRemainingEnemies(); // Destroy all remaining enemies
                Debug.Log("Boss defeated!"); 
                Destroy(gameObject); // Destroy the boss
            }
        }
    }

    void DamageHealthbar()
    {
        if (health > 0)
        {
            health -= 1; 
            barSize -= damage; 
        }
    }

    void HandleDrops()
    {
        float randomValue = Random.Range(0f, 1f);
        if (randomValue <= healthBuffDropRate)
        {
            Instantiate(healthBuffPrefab, transform.position, Quaternion.identity); 
        }
        else if (randomValue <= healthBuffDropRate + upgradeDropRate)
        {
            Instantiate(upgradePrefab, transform.position, Quaternion.identity); 
        }
        else if (randomValue <= healthBuffDropRate + upgradeDropRate + coinDropRate)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity); 
        }
    }

    // New method to destroy all remaining enemies
    void DestroyRemainingEnemies()
    {
        EnemyScript[] remainingEnemies = FindObjectsOfType<EnemyScript>();
        foreach (var enemy in remainingEnemies)
        {
            Destroy(enemy.gameObject); // Destroy each remaining enemy
        }
    }
}
