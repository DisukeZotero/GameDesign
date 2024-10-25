using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{
    [SerializeField]
    private int bulletsAmount = 10; // Number of bullets to be fired in an arc

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f; // Angles defining the range of bullet firing

    private Vector2 bulletMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 0f, 2f); // Repeatedly calls the Fire method every 2 seconds
    }

    private void Fire()
{
    float angleStep = (endAngle - startAngle) / bulletsAmount; // Calculate the step between each bullet's angle
    float angle = startAngle; // Initialize the angle to the starting angle

    // Loop through to create each bullet
    for(int i = 0; i < bulletsAmount + 1; i++)
    {
        float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f); // Calculate the X direction of the bullet
        float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f); // Calculate the Y direction of the bullet
        
        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f); // Create a movement vector for the bullet
        Vector2 bulDir = (bulMoveVector - transform.position).normalized; // Normalize the direction vector for consistent speed

        GameObject bul = BulletPool.bulletPoolInstance.GetBullet(); // Get an inactive bullet from the bullet pool

        if (bul != null) // Check if the bullet is not null before using it
        {
            bul.transform.position = transform.position; // Set the bullet's position to the firing point
            bul.transform.rotation = transform.rotation; // Maintain the rotation of the bullet
            bul.SetActive(true); // Activate the bullet
            bul.GetComponent<BulletHell>().SetMoveDirection(bulDir); // Set the movement direction for the bullet
        }

        angle += angleStep; // Increment the angle for the next bullet
    }
}


    // Update is called once per frame
    void Update()
    {
        // No specific update logic is needed for now
    }
}
