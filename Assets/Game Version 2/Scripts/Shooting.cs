using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject playerBullet; // Reference to the bullet prefab
    public Transform[] spawnPoints;  // Array to hold positions for bullet spawn
    public float fireRate = 0.5f;    // Time in seconds between shots

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoFire()); // Start the coroutine for automatic firing
    }

    // Coroutine for automatic firing
    IEnumerator AutoFire()
    {
        while (true) // Infinite loop for continuous firing
        {
            // Check if playerBullet and spawn points are assigned before instantiating
            if (playerBullet != null && spawnPoints.Length > 0)
            {
                // Iterate through each spawn point and instantiate a bullet
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(playerBullet, spawnPoint.position, Quaternion.identity);
                }
            }
            else
            {
                Debug.LogWarning("playerBullet or spawn points are not assigned.");
            }

            // Wait for the specified fire rate before firing again
            yield return new WaitForSeconds(fireRate);
        }
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}