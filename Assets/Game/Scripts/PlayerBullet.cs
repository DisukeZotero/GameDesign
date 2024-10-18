using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float speed = 8f;  // Speed of the bullet

    // Update is called once per frame
    void Update()
    {
        // Move the bullet upwards
        Vector2 position = transform.position; // Get the current position of the bullet
        position = new Vector2(position.x, position.y + speed * Time.deltaTime); // Calculate new position
        transform.position = position; // Update the bullet's position

        // Destroy the bullet if it moves off-screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // Get the top-right corner of the screen
        if (transform.position.y > max.y) // Check if the bullet's y position exceeds the screen's top boundary
        {
            Destroy(gameObject); // Destroy the bullet
        }
    }

    // Detect collisions
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits an enemy
        if (collision.CompareTag("EnemyShipTag")) // Use CompareTag for better performance
        {
            Destroy(gameObject); // Destroy the bullet
            Destroy(collision.gameObject); // Destroy the enemy ship
        }
    }
}
