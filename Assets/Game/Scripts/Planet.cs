using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float speed; // Speed at which the planet moves
    public bool isMoving; // Flag to determine if the planet is currently moving

    Vector2 min; // Minimum boundary for the planet's position
    Vector2 max; // Maximum boundary for the planet's position

    void Awake()
    {
        // Initialize the isMoving flag to false
        isMoving = false;

        // Get the world coordinates of the camera's viewport
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // Bottom-left corner
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // Top-right corner

        // Adjust max.y to account for the height of the planet sprite
        max.y = max.y + GetComponent<SpriteRenderer>().sprite.bounds.extents.y;

        // Adjust min.y to account for the height of the planet sprite
        min.y = min.y - GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Additional initialization can be done here if needed
    }

    // Update is called once per frame
    void Update()
    {
        // If the planet is not moving, exit the method
        if (!isMoving)
            return;
        
        // Get the current position of the planet
        Vector2 position = transform.position;

        // Update the position based on speed and time
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        // Set the new position of the planet
        transform.position = position;

        // Check if the planet has moved below the minimum boundary
        if (transform.position.y < min.y)
        {
            // Stop the planet's movement if it goes off-screen
            isMoving = false;
        }
    }

    // Method to reset the planet's position to a random point above the screen
    public void ResetPosition()
    {
        // Set the planet's position to a random x-coordinate within bounds and max.y for height
        transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
    }
}
