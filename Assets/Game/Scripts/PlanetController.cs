using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public GameObject[] Planets; // Array of planet GameObjects
    Queue<GameObject> availablePlanets = new Queue<GameObject>(); // Queue to manage available planets

    // Start is called before the first frame update
    void Start()
    {
        // Enqueue the first three planets into the available planets queue
        availablePlanets.Enqueue(Planets[0]);
        availablePlanets.Enqueue(Planets[1]);
        availablePlanets.Enqueue(Planets[2]);

        // Invoke the MovePlanetDown method every 20 seconds, starting immediately
        InvokeRepeating("MovePlanetDown", 0, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        // Continuously check and enqueue planets that have moved off screen
        EnqueuePlanets();
    }

    // Move a planet down by dequeuing it from the available queue
    void MovePlanetDown()
    {
        // If there are no available planets, exit the method
        if (availablePlanets.Count == 0)
            return;

        // Dequeue a planet and set it to moving
        GameObject aPlanet = availablePlanets.Dequeue();
        aPlanet.GetComponent<Planet>().isMoving = true; // Start the movement of the planet
    }

    // Check for planets that have moved off screen and reset their position
    void EnqueuePlanets()
    {
        // Iterate through each planet in the Planets array
        foreach (GameObject aPlanet in Planets) // Use 'in' instead of 'is'
        {
            // Check if the planet is below the screen and is not currently moving
            if ((aPlanet.transform.position.y < 0) && (!aPlanet.GetComponent<Planet>().isMoving))
            {
                aPlanet.GetComponent<Planet>().ResetPosition(); // Reset the planet's position to the top
                availablePlanets.Enqueue(aPlanet); // Enqueue it back into the available queue for reuse
            }
        }
    }
}
