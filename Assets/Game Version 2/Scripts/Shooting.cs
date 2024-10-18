using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject playerBullet; // Reference to the bullet prefab
    public Transform spawnPoint1;   // Position for the first bullet spawn
    public Transform spawnPoint2;   // Position for the second bullet spawn
    public float fireRate = 0.5f;   // Time in seconds between shots

    private float nextFireTime = 0f; // Keeps track of when the next shot can be fired

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the "Fire1" button is held down (spacebar in this case) and the current time is past the next allowed fire time
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            // Update the time for the next shot
            nextFireTime = Time.time + fireRate;

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
        }
    }
}
