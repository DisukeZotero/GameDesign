using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform[] gunPoint; // Points from which enemy bullets will be spawned
    public GameObject enemyBullet; // Prefab for the enemy bullet
    public float enemyBulletSpawnTime = 0.5f; // Time between enemy shots
    public HealthBar healthbar; // Reference to the health bar
    public float speed = 1f; // Speed at which the enemy moves
    public float health = 10f; // Enemy's health
    public GameObject coinPrefab; // Prefab for coin drop
    public GameObject upgradePrefab; // Prefab for upgrade drop
    public GameObject healthBuffPrefab; // Prefab for health buff drop
    public GameObject enemyExplosionPrefab; // Prefab for Explosion

    [Range(0f, 1f)]
    public float healthBuffDropRate = 0.15f; // Drop rate for health buff
    [Range(0f, 1f)]
    public float upgradeDropRate = 0.1f; // Drop rate for upgrade
    [Range(0f, 1f)]
    public float coinDropRate = 0.3f; // Drop rate for coins

    public AudioClip bulletSound; // Sound for enemy shooting
    public AudioClip damageSound; // Sound for when the enemy takes damage
    public AudioClip explosionSound; // Sound for enemy destruction
    public AudioSource audioSource; // Audio source for playing sounds

    float barSize = 1f; // Size of the health bar
    float damage = 0; // Damage amount for health bar update

    void Start()
    {
        StartCoroutine(EnemyShooting()); // Start shooting coroutine
        damage = barSize / health; // Calculate damage per hit for health bar
    }

    void Update()
{
    // Find the player object
    GameObject player = GameObject.FindWithTag("Player");
    if (player != null)
    {
        // Calculate the direction to the player
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Desired distance to maintain from the player
        float stoppingDistance = 2f; // Adjust this value to set how far above the player the enemy should stop

        // Move towards the player only if the enemy is farther than the stopping distance
        if (distanceToPlayer > stoppingDistance)
        {
            // Apply a slight speed boost when moving towards the player
            float speedBoost = 1.5f; // Increase this value for a greater speed boost
            transform.Translate(direction * speed * speedBoost * Time.deltaTime);
        }
    }
}

    void DamageHealthbar()
    {
        if (health > 0)
        {
            health -= 1; // Decrease health
            barSize -= damage; // Update health bar size
        }
    }

    void EnemyFire()
    {
        for (int i = 0; i < gunPoint.Length; i++)
        {
            Instantiate(enemyBullet, gunPoint[i].position, Quaternion.identity); // Spawn bullets
        }
    }

    IEnumerator EnemyShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemyBulletSpawnTime); // Wait before shooting
            EnemyFire(); // Call to fire bullets
            audioSource.PlayOneShot(bulletSound, 0.5f); // Play shooting sound
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            audioSource.PlayOneShot(damageSound); // Play damage sound
            DamageHealthbar(); // Damage the enemy's health
            Destroy(collision.gameObject); // Destroy the player's bullet
            if (health <= 0)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position, 0.5f); // Play explosion sound
                HandleDrops(); // Handle item drops
                Destroy(gameObject); // Destroy the enemy
                GameObject enemyExplosion = Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(enemyExplosion, 0.4f);
            }
        }
    }

    void HandleDrops()
    {
        float randomValue = Random.Range(0f, 1f); // Generate a random value for item drops

        // Adjusted drop logic to ensure that only one item drops per enemy
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
}
