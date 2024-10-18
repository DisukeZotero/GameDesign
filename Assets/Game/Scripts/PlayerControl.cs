using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject GameManagerGO;

    public GameObject PlayerBulletGO; // Bullet prefab
    public GameObject bulletPosition01; // First bullet spawn position
    public GameObject bulletPosition02; // Second bullet spawn position
    public Text LivesUIText;
    const int MaxLives = 3;
    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text = lives.ToString();
        gameObject.SetActive(true);
    }
    int lives;
    public float speed = 5f; // Speed of the player's movement

    public float fireRate = 0.2f; // Time in seconds between shots
    private float nextFireTime = 0f; // Time until the next shot can be fired

    void Start()
    {
        Debug.Log("PlayerControl initialized");

        // Check for null references at the start
        if (PlayerBulletGO == null)
            Debug.LogError("PlayerBulletGO is not assigned!"); // Log error if bullet prefab is missing
        if (bulletPosition01 == null)
            Debug.LogError("bulletPosition01 is not assigned!"); // Log error if first bullet position is missing
        if (bulletPosition02 == null)
            Debug.LogError("bulletPosition02 is not assigned!"); // Log error if second bullet position is missing
    }

    void Update()
    {
        // Check for space key press and if the player can fire
        if (Input.GetKey("space") && Time.time >= nextFireTime)
        {
            FireBullets(); // Fire bullets if conditions are met
        }

        // Get the horizontal and vertical input axes
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Create a direction vector based on input and normalize it
        Vector2 direction = new Vector2(x, y).normalized;

        // Move the player based on the input direction
        Move(direction);
    }

    void FireBullets()
    {
        // Log to check when firing occurs
        Debug.Log("FireBullets called");

        // Check for null references before firing
        if (PlayerBulletGO == null || bulletPosition01 == null || bulletPosition02 == null)
        {
            Debug.LogError("Cannot fire bullets because one or more references are not assigned."); // Log error
            return; // Prevent further execution if references are missing
        }

        nextFireTime = Time.time + fireRate; // Set the next fire time

        // Instantiate bullets at defined positions
        GameObject bullet01 = Instantiate(PlayerBulletGO);
        bullet01.transform.position = bulletPosition01.transform.position; // Set position for the first bullet
        
        GameObject bullet02 = Instantiate(PlayerBulletGO);
        bullet02.transform.position = bulletPosition02.transform.position; // Set position for the second bullet
    }

    void Move(Vector2 direction)
    {
        // Get the screen bounds in world coordinates
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // Bottom-left corner
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // Top-right corner

        // Adjust the bounds to prevent the player from moving off-screen
        max.x -= 0.225f; // Adjust right boundary
        min.x += 0.225f; // Adjust left boundary
        max.y -= 0.285f; // Adjust upper boundary
        min.y += 0.285f; // Adjust lower boundary

        // Calculate the new position based on the movement direction
        Vector2 pos = (Vector2)transform.position + direction * speed * Time.deltaTime;

        // Clamp the position within the screen bounds
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos; // Update the player's position
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for collisions with enemy ships or enemy bullets
        if (collision.CompareTag("EnemyShipTag") || collision.CompareTag("EnemyBulletTag"))
        {
            Destroy(gameObject); // Destroy the enemy on collision
        }
    
    }
}
