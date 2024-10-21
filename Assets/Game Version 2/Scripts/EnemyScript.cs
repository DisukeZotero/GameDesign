using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform []gunPoint;
    public GameObject enemyBullet;       // Reference to the bullet prefab
    public float enemyBulletSpawnTime = 0.5f; // Time between each bullet spawn
    public HealthBar healthbar;
    public float speed = 1f;
    public float health = 10f;

    float barSize = 1f;
    float damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to handle automatic shooting
        StartCoroutine(EnemyShooting());
        damage = barSize / health;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void DamageHealthbar()
    {
        if(health > 0)
        {
            if (health > 0)
            {
                health -= 1;
                barSize = barSize - damage;
            }
            
        }
    }

    // Function to instantiate bullets from the enemy's gun points
    void EnemyFire()
    {
        for(int i = 0; i < gunPoint.Length; i++)
        {
            Instantiate(enemyBullet, gunPoint[i].position, Quaternion.identity);
        }
        // Check if enemyBullet and gun points are assigned before instantiating
        // if (enemyBullet != null && gunPoint1 != null && gunPoint2 != null)
        // {
        //     Instantiate(enemyBullet, gunPoint1.position, Quaternion.identity);
        //     Instantiate(enemyBullet, gunPoint2.position, Quaternion.identity);
        // }
        // else
        // {
        //     Debug.LogWarning("enemyBullet or gun points are not assigned.");
        // }
    }

    // Coroutine that handles automatic shooting at intervals
    IEnumerator EnemyShooting()
    {
        while (true) // Infinite loop for continuous firing
        {
            yield return new WaitForSeconds(enemyBulletSpawnTime); // Wait before firing again
            EnemyFire(); // Call the Fire method to shoot bullets
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the tag "PlayerBullet"
        if (collision.CompareTag("PlayerBullet"))
        {
            DamageHealthbar();
            Destroy(collision.gameObject); // Destroy the bullet GameObject
            if(health <= 0)
            {
                Destroy(gameObject); // Destroy the enemy GameObject
            }
        }
    }
}
