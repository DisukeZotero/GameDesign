using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private float moveSpeed; // Speed at which the boss moves
    private bool moveRight; // Direction in which the boss is moving (true for right, false for left)
    public HealthBar healthbar; // Reference to the health bar component
    public float health = 10f; // Health of the boss
    public GameObject coinPrefab; // Coin prefab to instantiate on death

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 2f; // Initialize the move speed
        moveRight = true; // Start moving to the right
        UpdateHealthBar(); // Update health bar to full at the start
    }

    // Update is called once per frame
    void Update()
    {
        // Get the screen boundaries in world units
        float screenLeftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x; // Left boundary
        float screenRightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane)).x; // Right boundary

        // Check if the boss has reached the right or left boundary
        if (transform.position.x > screenRightBoundary)
        {
            moveRight = false; // Change direction to left
        }
        else if (transform.position.x < screenLeftBoundary)
        {
            moveRight = true; // Change direction to right
        }

        // Move the boss based on the current direction
        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y); // Move right
        }
        else
        {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y); // Move left
        }
    }

    void DamageHealthbar()
{
    if (health > 0)
    {
        health -= 1; // Decrease health
        Debug.Log("Boss Health: " + health); // Log current health
        UpdateHealthBar(); // Update health bar
    }
}


    void UpdateHealthBar()
    {
        if (healthbar != null)
        {
            // Calculate health ratio (0 to 1) and set it on the health bar
            float healthRatio = health / 10f; // Assuming max health is 10
            healthbar.SetSize(healthRatio); // Update the health bar size
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
{
    // Check if the collided object has the tag "PlayerBullet"
    if (collision.CompareTag("PlayerBullet"))
    {
        DamageHealthbar(); // Damage health
        Destroy(collision.gameObject); // Destroy the bullet GameObject
        
        // Check if health is 0 or less
        if (health <= 0)
        {
            Debug.Log("Boss defeated!"); // Log when boss is defeated
            Instantiate(coinPrefab, transform.position, Quaternion.identity); // Drop coin on death
            Destroy(gameObject); // Destroy the boss GameObject
        }
    }
}

}