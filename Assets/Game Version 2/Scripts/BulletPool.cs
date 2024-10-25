using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool bulletPoolInstance; // Singleton instance of BulletPool

    [SerializeField]
    private GameObject pooledBullet; // Reference to the bullet prefab that will be pooled
    private bool notEnoughBulletsInPool = true; // Flag to determine if bullets need to be added to the pool

    private List<GameObject> bullets; // List to store the pooled bullet objects

    private void Awake()
    {
        // Set the singleton instance to this BulletPool instance
        bulletPoolInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>(); // Initialize the list of bullets
    }

    // Method to get a bullet from the pool
    // Method to get a bullet from the pool
public GameObject GetBullet()
{
    // Check if there are bullets in the pool
    if (bullets.Count > 0)
    {
        // Loop through the bullets to find an inactive one
        for (int i = 0; i < bullets.Count; i++)
        {
            // Check if the bullet is not null and not active
            if (bullets[i] != null && !bullets[i].activeInHierarchy)
            {
                return bullets[i]; // Return the inactive bullet
            }
        }
    }

    // If there are not enough bullets in the pool, instantiate a new one
    if (notEnoughBulletsInPool)
    {
        GameObject bul = Instantiate(pooledBullet); // Instantiate the bullet prefab
        bul.SetActive(false); // Set the bullet to inactive
        bullets.Add(bul); // Add the bullet to the pool
        return bul; // Return the newly created bullet
    }

    return null; // Return null if no bullets are available and the pool is full
}


    // Update is called once per frame
    void Update()
    {
        // This method can be used for additional logic if needed
    }
}
