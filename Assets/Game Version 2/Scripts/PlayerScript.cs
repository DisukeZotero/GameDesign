using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public PlayerHealthbarScript playerHealthbar; // Reference to the player's health bar script
    public float speed = 10f; // Speed at which the player moves
    public float padding = 0.8f; // Padding to keep the player within screen boundaries
    public CoinCount coinCountScript; // Reference to the CoinCount script to update the coin count
    public GameController GameController; // Reference to the game controller script for game-over conditions
    public GameObject enemyExplosionPrefab; // Prefab for Explosion

    private float minX; // Minimum X boundary for player movement
    private float maxX; // Maximum X boundary for player movement
    private float minY; // Minimum Y boundary for player movement
    private float maxY; // Maximum Y boundary for player movement

    public AudioClip damageSound; // Sound clip to play when the player takes damage
    public AudioClip explosionSound; // Sound clip to play when the player dies

    public AudioSource audioSource; // Reference to the AudioSource component

    public float health = 20f; // Player's starting health
    private float maxHealth = 20f; // Maximum health the player can have
    private float barFillAmount = 1f; // Initial fill amount of the health bar
    private float damage; // Amount of health deducted per damage

    void Start()
    {
        FindBoundaries(); // Calculate the screen boundaries for player movement
        damage = barFillAmount / health; // Calculate how much to decrease the health bar per damage

        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    void FindBoundaries()
    {
        Camera gameCamera = Camera.main; // Get a reference to the main camera
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    void Update()
    {
        // Get player's input for movement
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        // Calculate new position and clamp it within boundaries
        float newXpos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        float newYpos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);

        // Update player's position
        transform.position = new Vector2(newXpos, newYpos);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with an enemy bullet
        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("MiniBossBullet"))
        {
            DamagePlayerHealthbar(); // Decrease health bar on taking damage

            // Play damage sound if available
            if (damageSound != null)
            {
                audioSource.PlayOneShot(damageSound);
            }

            Destroy(collision.gameObject); // Destroy the enemy bullet

            if (health <= 0) // Check if player health is depleted
            {
                // Play explosion sound if available
                AudioSource.PlayClipAtPoint(explosionSound, transform.position, 0.5f); // Play explosion sound
                GameController.GameOver(); // Trigger game over from GameController
                Destroy(gameObject); // Destroy player object
                GameObject enemyExplosion = Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(enemyExplosion, 0.4f);
            }
        }

        // Check if the collision is with a coin
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject); // Destroy the coin object
            coinCountScript.AddCount(); // Update coin count
        }
    }

    // Function to decrease player's health and update health bar UI
    void DamagePlayerHealthbar()
    {
        if (health > 0) // Check if player still has health
        {
            health -= 1; // Reduce health by 1
            barFillAmount -= damage; // Update fill amount
            playerHealthbar.SetAmount(barFillAmount); // Set health bar fill amount
        }
    }

    // Function to adjust player's health (healing or increase) without exceeding maxHealth
    public void AdjustHealth(float amount)
    {
        health = Mathf.Min(health + amount, maxHealth); // Increase health but do not exceed maxHealth
        barFillAmount = health / maxHealth; // Update the fill amount based on the new health
        playerHealthbar.SetAmount(barFillAmount); // Set health bar fill amount
    }
}
