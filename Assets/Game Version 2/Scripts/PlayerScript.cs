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
    float maxHealth = 20f; // Maximum health the player can have
    float barFillAmount = 1f; // Initial fill amount of the health bar
    float damage = 0; // Amount of health deducted per damage

    void Start()
    {
        FindBoundaries(); // Calculate the screen boundaries for player movement
        damage = barFillAmount / health; // Calculate how much to decrease the health bar per damage
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
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        float newXpos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        float newYpos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);

        transform.position = new Vector2(newXpos, newYpos);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            DamagePlayerHealthbar();   
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                GameController.GameOver();
                Destroy(gameObject);
            }
        }

        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            coinCountScript.AddCount();
        }
    }

    void DamagePlayerHealthbar()
    {
        if (health > 0)
        {
            health -= 1;
            barFillAmount = barFillAmount - damage;
            playerHealthbar.SetAmount(barFillAmount);
        }
    }

    public void AdjustHealth(float amount)
{
    health = Mathf.Min(health + amount, maxHealth); // Increase health but do not exceed maxHealth
    barFillAmount = health / maxHealth; // Update the fill amount based on the new health
    playerHealthbar.SetAmount(barFillAmount);
}

}
