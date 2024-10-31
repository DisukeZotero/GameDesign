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

    void Start()
    {
        initialPosition = transform.position; // Store the initial position
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
        isMovingHorizontally = true; // Start moving horizontally
    }

    // Method to move left and right
    void MoveLeftRight()
    {
        float moveSpeed = 2f; // Speed of horizontal movement
        float moveRange = 2f; // Distance to move left and right
        float newX = Mathf.PingPong(Time.time * moveSpeed, moveRange) - (moveRange / 2);
        transform.position = new Vector2(initialPosition.x + newX, transform.position.y);
    }

    // Handle health damage and drops
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            DamageHealthbar();
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                HandleDrops();
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
