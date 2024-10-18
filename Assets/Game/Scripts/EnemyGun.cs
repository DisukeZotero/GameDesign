using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyBulletGO; // The bullet prefab

    void Start()
    {
        // Start firing bullets repeatedly
        InvokeRepeating("FireEnemyBullet", 1f, 2f); // Fires a bullet every 2 seconds after an initial delay of 1 second
    }

    void Update()
    {
        // Update logic can be added here if needed in the future
    }

    void FireEnemyBullet()
    {
        // Find the player object
        GameObject playerShip = GameObject.Find("PlayerGO");

        if (playerShip != null)
        {
            // Instantiate the bullet
            GameObject bullet = Instantiate(EnemyBulletGO); // Instantiate the bullet prefab
            bullet.transform.position = transform.position; // Set bullet position to the gun's position

            // Calculate the direction from the enemy to the player
            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            // Set the bullet's direction
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
