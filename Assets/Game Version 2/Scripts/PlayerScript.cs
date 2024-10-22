using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public PlayerHealthbarScript playerHealthbar; // Reference to the player's health bar script
    public float speed = 10f; // Speed at which the player moves
    public float padding = 0.8f; // Padding to keep the player within screen boundaries
    public CoinCount coinCountScript; // Reference to the CoinCount script to update the coin count
    public GameController GameController;

    float minX;
    float maxX;
    float minY;
    float maxY;

    public float health = 20f; // Player's starting health
    float barFillAmount = 1f; // Initial fill amount of the health bar
    float damage = 0; // Amount of health deducted per damage

    // Start is called before the first frame update
    void Start()
    {
        FindBoundaries(); // Calculate the screen boundaries for player movement
        damage = barFillAmount / health; // Calculate how much to decrease the health bar per damage
    }

    // Method to calculate the boundaries based on the camera's viewport
    void FindBoundaries()
    {
        Camera gameCamera = Camera.main; // Get a reference to the main camera
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    // Update is called once per frame
    void Update()
    {
        // Get input values for movement
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        // Calculate new positions and clamp them within the defined boundaries
        float newXpos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        float newYpos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);

        // Update both x and y positions for vertical and horizontal movement
        transform.position = new Vector2(newXpos, newYpos);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the tag "EnemyBullet"
        if (collision.CompareTag("EnemyBullet"))
        {
            DamagePlayerHealthbar();   
            Destroy(collision.gameObject); // Destroy the bullet GameObject upon collision
            if (health <= 0)
            {
                GameController.GameOver();
                Destroy(gameObject); // Destroy the player GameObject if health reaches zero
            }
        }

        // Check if the collided object has the tag "Coin"
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject); // Destroy the coin GameObject upon collection
            coinCountScript.AddCount(); // Call AddCount method from CoinCount script to increase the count
        }
    }

    void DamagePlayerHealthbar()
    {
        if(health > 0)
        {
            health -= 1; // Reduce health by 1 unit
            barFillAmount = barFillAmount - damage; // Update the health bar's fill amount based on damage
            playerHealthbar.SetAmount(barFillAmount);
        }
    }
}
