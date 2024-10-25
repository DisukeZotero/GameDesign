using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform[] gunPoint;
    public GameObject enemyBullet;
    public float enemyBulletSpawnTime = 0.5f;
    public HealthBar healthbar;
    public float speed = 1f;
    public float health = 10f;
    public GameObject coinPrefab;
    public GameObject upgradePrefab;
    public GameObject healthBuffPrefab;

    [Range(0f, 1f)]
    public float healthBuffDropRate = 0.15f; // Drop rate for the health buff, adjustable in Unity
    [Range(0f, 1f)]
    public float upgradeDropRate = 0.1f; // Drop rate for the upgrade, adjustable in Unity
    [Range(0f, 1f)]
    public float coinDropRate = 0.3f; // Drop rate for the coin, adjustable in Unity

    float barSize = 1f;
    float damage = 0;

    void Start()
    {
        StartCoroutine(EnemyShooting());
        damage = barSize / health;
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void DamageHealthbar()
    {
        if (health > 0)
        {
            health -= 1;
            barSize = barSize - damage;
        }
    }

    void EnemyFire()
    {
        for (int i = 0; i < gunPoint.Length; i++)
        {
            Instantiate(enemyBullet, gunPoint[i].position, Quaternion.identity);
        }
    }

    IEnumerator EnemyShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemyBulletSpawnTime);
            EnemyFire();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            DamageHealthbar();
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                HandleDrops();
                Destroy(gameObject);
            }
        }
    }

    void HandleDrops()
    {
        float randomValue = Random.Range(0f, 1f);

        // Adjusted drop logic to ensure that only one item drops per enemy
        if (randomValue <= healthBuffDropRate)
        {
            Instantiate(healthBuffPrefab, transform.position, Quaternion.identity);
        }
        else if (randomValue <= healthBuffDropRate + upgradeDropRate)
        {
            Instantiate(upgradePrefab, transform.position, Quaternion.identity);
        }
        else if (randomValue <= healthBuffDropRate + upgradeDropRate + coinDropRate)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }
}
