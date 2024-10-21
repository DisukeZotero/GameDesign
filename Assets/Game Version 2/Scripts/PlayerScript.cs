using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public PlayerHealthbarScript playerHealthbar;
    public float speed = 10f;
    public float padding = 0.8f;
    float minX;
    float maxX;
    float minY;
    float maxY;

    public float health = 20f;
    float barFillAmount = 1f;
    float damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        FindBoundaries();
        damage = barFillAmount / health;
    }

    // Method to calculate the boundaries based on the camera's viewport
    void FindBoundaries()
    {
        Camera gameCamera = Camera.main; // Fixed the reference to Camera.main instead of gameCamera.main
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
        float newXpos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX); // Fixed x and y variables being swapped
        float newYpos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);

        // Update both x and y positions for vertical and horizontal movement
        transform.position = new Vector2(newXpos, newYpos);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the tag "PlayerBullet"
        if (collision.CompareTag("EnemyBullet"))
        {
            DamagePlayerHealthbar();   
            Destroy(collision.gameObject); // Destroy the bullet GameObject
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void DamagePlayerHealthbar()
    {
        health -= 1;
        barFillAmount = barFillAmount - damage;
    }
}
