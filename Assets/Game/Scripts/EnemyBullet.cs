using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float speed;              // Speed of the bullet
    Vector2 _direction;       // Direction the bullet will move in
    bool isReady;             // Flag to check if the bullet is ready to move

    void Awake()
    {
        speed = 5f;           // Initialize speed
        isReady = false;      // Initialize readiness to false
    }

    // Start is called before the first frame update
    void Start()
    {
        // Any additional initialization can go here
    }

    // Method to set the direction of the bullet
    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized; // Normalize the direction vector
        isReady = true;                    // Mark the bullet as ready
    }

    // Update is called once per frame
    void Update()
    {
        if (isReady) // Check if the bullet is ready to move
        {
            // Move the bullet
            Vector2 position = transform.position; // Explicitly cast to Vector2
            position += _direction * speed * Time.deltaTime; // Update position based on direction and speed
            transform.position = position; // Set the new position

            // Check for bounds
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            // Destroy the bullet if it goes out of bounds
            if (transform.position.x < min.x || transform.position.x > max.x ||
                transform.position.y < min.y || transform.position.y > max.y)
            {
                Destroy(gameObject); // Destroy the bullet if it goes out of view
            }
        }
    }

    // Handle collision with other objects
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Fixed the usage of CompareTag method
        if (collision.CompareTag("PlayerShipTag")) // Check if the bullet hits the player ship
        {
            Destroy(gameObject); // Destroy the bullet on collision
        }
    }
}
