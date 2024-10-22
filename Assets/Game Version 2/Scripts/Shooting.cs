using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject playerBullet; // Reference to the bullet prefab
    public Transform spawnPoint1;   // Position for the first bullet spawn
    public Transform spawnPoint2;   // Position for the second bullet spawn
    public float fireRate = 0.5f;   // Time in seconds between shots

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
            if (playerBullet != null && spawnPoint1 != null && spawnPoint2 != null)
            {
                // Instantiate the bullet at the position of spawnPoint1 with no rotation
                Instantiate(playerBullet, spawnPoint1.position, Quaternion.identity);
                // Instantiate the bullet at the position of spawnPoint2 with no rotation
                Instantiate(playerBullet, spawnPoint2.position, Quaternion.identity);
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
        // If you want to include any additional functionality in Update, you can add it here.
    }
}
