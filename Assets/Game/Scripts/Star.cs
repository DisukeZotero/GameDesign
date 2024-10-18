using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public float speed; // Speed at which the star moves down

    // Start is called before the first frame update
    void Start()
    {
        // Optional initialization logic can go here
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current position of the star
        Vector2 position = transform.position;

        // Move the star downwards based on speed and time
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        // Update the star's position
        transform.position = position;

        // Get the screen boundaries in world coordinates
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // Bottom-left corner
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // Top-right corner

        // Check if the star goes off-screen and reset its position to the top
        if (transform.position.y < min.y)
        {
            // Set a new position at the top of the screen with a random x position
            transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        }
    }
}
