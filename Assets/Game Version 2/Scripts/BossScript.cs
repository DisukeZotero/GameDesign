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
    public GameObject enemyExplosionPrefab; // Prefab for explosion effect

    [Range(0f, 1f)]
    public float healthBuffDropRate = 0.15f; // Drop rate for health buff
    [Range(0f, 1f)]
    public float upgradeDropRate = 0.1f; // Drop rate for upgrade
    [Range(0f, 1f)]
    public float coinDropRate = 0.3f; // Drop rate for coins

    public AudioClip bulletSound; // Sound for shooting
    public AudioClip damageSound; // Sound for getting hit
    public AudioClip explosionSound; // Sound for explosion
    public AudioSource audioSource; // Audio source for playing sounds

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
        initialPosition = new Vector2(0f, transform.position.y); // Set initial position
        transform.position = initialPosition; 
        StartCoroutine(EnemyShooting()); // Start shooting bullets
        damage = barSize / health; // Calculate damage per health unit
        StartCoroutine(MoveDownAndThenHorizontal()); // Start vertical movement
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
            yield return new WaitForSeconds(enemyBulletSpawnTime); // Wait before next shot
            EnemyFire(); // Fire bullets from the gun points
        }
    }

    void EnemyFire()
    {
        // Instantiate bullets at each gun point
        for (int i = 0; i < gunPoint.Length; i++)
        {
            Instantiate(enemyBullet, gunPoint[i].position, Quaternion.identity); // Create bullet
        }
    }

    IEnumerator MoveDownAndThenHorizontal()
    {
        float moveDownDistance = 3f; 
        float moveDuration = 1f; 
        Vector2 targetPosition = new Vector2(initialPosition.x, initialPosition.y - moveDownDistance);

        // Move down over a specified duration
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector2.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime; // Increment time
            yield return null; // Wait for the next frame
        }

        transform.position = targetPosition; // Set final down position
        yPosition = transform.position.y; // Store y position for later use
        yield return new WaitForSeconds(0.5f); // Wait before starting horizontal movement

        isMovingHorizontally = true; // Begin horizontal movement
        targetX = 0f; // Start at the center
    }

    void MoveLeftRight()
    {
        // Calculate target X position for smooth left-right movement
        targetX = Mathf.PingPong(Time.time * moveSpeed, moveRange * 2) - moveRange; 
        float smoothX = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * moveSpeed); // Smooth movement
        transform.position = new Vector2(smoothX, yPosition); // Update position
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            audioSource.PlayOneShot(damageSound); // Play damage sound
            DamageHealthbar(); // Apply damage to the boss health
            Destroy(collision.gameObject); // Destroy the player's bullet
            Debug.Log($"Boss hit! Current health: {health}"); 
            if (health <= 0)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position, 0.5f); // Play explosion sound
                HandleDrops(); // Handle drops upon boss defeat
                DestroyRemainingEnemies(); // Destroy all remaining enemies
                Debug.Log("Boss defeated!"); 
                Destroy(gameObject); // Destroy the boss object
                GameObject enemyExplosion = Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(enemyExplosion, 0.4f); // Destroy explosion effect after a delay
            }
        }
    }

    void DamageHealthbar()
    {
        if (health > 0)
        {
            health -= 1; // Reduce health by 1
            barSize -= damage; // Update health bar size
        }
    }

    void HandleDrops()
    {
        float randomValue = Random.Range(0f, 1f); // Get a random value to determine drops
        if (randomValue <= healthBuffDropRate)
        {
            Instantiate(healthBuffPrefab, transform.position, Quaternion.identity); // Drop health buff
        }
        else if (randomValue <= healthBuffDropRate + upgradeDropRate)
        {
            Instantiate(upgradePrefab, transform.position, Quaternion.identity); // Drop upgrade
        }
        else if (randomValue <= healthBuffDropRate + upgradeDropRate + coinDropRate)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity); // Drop coin
        }
    }

    // Destroy all remaining enemies in the scene
    void DestroyRemainingEnemies()
    {
        EnemyScript[] remainingEnemies = FindObjectsOfType<EnemyScript>(); // Find all remaining enemies
        foreach (var enemy in remainingEnemies)
        {
            Destroy(enemy.gameObject); // Destroy each remaining enemy
        }
    }
}
