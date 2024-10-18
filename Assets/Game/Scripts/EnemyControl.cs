using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    float speed = 2f; // Set the speed of the enemy

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position; // Get the current position of the enemy

        // Move downwards
        position = new Vector2(position.x, position.y - speed * Time.deltaTime); // Update the y position based on speed

        // Update the position
        transform.position = position; // Set the new position of the enemy

        // Check if the enemy has gone off screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // Get the bottom-left corner of the camera view
        if (transform.position.y < min.y) // Check if the enemy's y position is below the camera view
        {
            Destroy(gameObject); // Destroy the enemy if it goes off screen
        }
    }

    // Handle collision with player bullets or the player ship
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for collisions with player bullets or the player ship
        if (collision.CompareTag("PlayerShipTag") || collision.CompareTag("PlayerBulletTag"))
        {
            Destroy(gameObject); // Destroy the enemy on collision
        }
    }
}
