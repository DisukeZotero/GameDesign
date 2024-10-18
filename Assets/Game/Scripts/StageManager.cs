using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int currentStage = 1; // Current stage
    public int maxStages = 3; // Total number of stages
    public float stageDuration = 30f; // Duration for each stage
    public BackgroundManager backgroundManager; // Reference to the background manager

    void Start()
    {
        StartCoroutine(StageRoutine());
    }

    IEnumerator StageRoutine()
    {
        while (currentStage <= maxStages)
        {
            Debug.Log("Starting Stage: " + currentStage);
            backgroundManager.UpdateBackground(currentStage); // Change the background

            // Wait for the duration of the stage
            yield return new WaitForSeconds(stageDuration);

            currentStage++; // Move to the next stage
        }

        Debug.Log("All stages completed!");
        // Handle end of game logic here
    }
}
