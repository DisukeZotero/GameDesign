using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossScript : MonoBehaviour
{
    public Transform[] gunPoint; // Points from where the miniboss will shoot
    public GameObject enemyBullet; // Bullet prefab for the miniboss
    public float enemyBulletSpawnTime = 0.5f; // Time between enemy bullets
    public HealthBar healthbar; // Health bar for the miniboss
    public float speed = 1f; // Movement speed of the miniboss
    public float health = 20f; // Health of the miniboss
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
    private float damage = 0; // Damage value
    private bool isMovingHorizontally = false; // Flag to track horizontal movement
    private Vector2 initialPosition; // Initial position of the miniboss
    private float yPosition; // Store the y-axis position after moving down
    private float targetX; // Target x position for left/right movement
    private float moveSpeed = 2f; // Speed of horizontal movement
    private float moveRange = 3f; // Distance to move left and right

    void Start()
    {
        initialPosition = new Vector2(0f, transform.position.y); // Set initial position to the middle of the screen
        transform.position = initialPosition; // Set the miniboss position to the middle
        StartCoroutine(EnemyShooting()); // Start shooting
        damage = barSize / health; // Calculate damage per health
        StartCoroutine(MoveDownAndThenHorizontal()); // Start movement coroutine
    }

    void Update()
    {
        // If moving horizontally, move left and right
        if (isMovingHorizontally)
        {
            MoveLeftRight();
        }
    }

    // Coroutine for shooting bullets
    IEnumerator EnemyShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemyBulletSpawnTime);
            EnemyFire();
        }
    }

    // Method to fire bullets from gun points
    void EnemyFire()
    {
        for (int i = 0; i < gunPoint.Length; i++)
        {
            Instantiate(enemyBullet, gunPoint[i].position, Quaternion.identity);
        }
    }

    // Coroutine to handle the miniboss's movement
    IEnumerator MoveDownAndThenHorizontal()
    {
        // Move down first
        float moveDownDistance = 2f; // Distance to move down before moving horizontally
        float moveDuration = 1f; // Duration to move down
        Vector2 targetPosition = new Vector2(initialPosition.x, initialPosition.y - moveDownDistance);
        
        // Move down
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector2.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Ensure the position is set to the target position
        yPosition = transform.position.y; // Store the y-axis position
        yield return new WaitForSeconds(0.5f); // Wait before starting horizontal movement

        isMovingHorizontally = true; // Start moving horizontally

        // Set the initial target position for left/right movement
        targetX = 0f; // Start at the center
    }

    // Method to move left and right smoothly
    void MoveLeftRight()
    {
        // Calculate the target position using Mathf.PingPong for smooth left/right movement
        targetX = Mathf.PingPong(Time.time * moveSpeed, moveRange * 2) - moveRange; // Adjusted to move between -3 and 3

        // Smoothly move towards the target x position
        float smoothX = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * moveSpeed);
        transform.position = new Vector2(smoothX, yPosition); // Update position, keeping y constant
    }

    // Handle health damage and drops
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            DamageHealthbar();
            Destroy(collision.gameObject);
            Debug.Log($"Miniboss hit! Current health: {health}"); // Debug log for health
            if (health <= 0)
            {
                HandleDrops();
                Debug.Log("Miniboss defeated!"); // Debug log for defeat
                Destroy(gameObject);
            }
        }
    }

    void DamageHealthbar()
    {
        if (health > 0)
        {
            health -= 1;
            barSize = barSize - damage;
        }
    }

    void HandleDrops()
    {
        float randomValue = Random.Range(0f, 1f);

        // Adjusted drop logic to ensure that only one item drops per miniboss
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
}
