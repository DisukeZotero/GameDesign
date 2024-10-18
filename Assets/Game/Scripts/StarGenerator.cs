using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public GameObject StarGO; // Reference to the star prefab
    public int MaxStars;      // Maximum number of stars to generate

    // Array of colors for the stars
    Color[] starColors = new Color[] {
        new Color(0.5f, 0.5f, 1f), // Light Blue
        new Color(0, 1f, 1f),      // Cyan
        new Color(1f, 1f, 0),      // Yellow
        new Color(1f, 0, 0),       // Red
    };

    // Start is called before the first frame update
    void Start()
    {
        // Get the screen boundaries in world coordinates
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // Bottom-left corner
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // Top-right corner

        for (int i = 0; i < MaxStars; ++i)
        {
            // Instantiate a new star
            GameObject star = Instantiate(StarGO);
            
            // Assign a color from the starColors array based on the index
            star.GetComponent<SpriteRenderer>().color = starColors[i % starColors.Length];

            // Set the star's position to a random point within the screen bounds
            star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

            // Ensure the Star script exists on the star GameObject and set its speed
            star.GetComponent<Star>().speed = (1f * Random.value + 0.5f); // Random speed between 0.5 and 1.5

            // Set the parent of the star to the StarGenerator to keep the hierarchy organized
            star.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Optional: Additional logic can be added here if needed
    }
}
