using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Speed of the bullet

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code can be added here if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Move the bullet upwards based on its speed
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet collides with an object tagged "MiniBossBullet"
        if (collision.CompareTag("MiniBossBullet"))
        {
            Destroy(gameObject); // Destroy this bullet on collision
        }
    }
}
