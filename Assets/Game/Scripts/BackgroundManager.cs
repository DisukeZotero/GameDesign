using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public Sprite[] stageBackgrounds; // Array of backgrounds for each stage
    public SpriteRenderer backgroundRenderer; // Reference to the SpriteRenderer for the background

    private int currentStage = 0; // Keep track of the current stage

    public int CurrentStage => currentStage; // Public property to access the current stage

    void Start()
    {
        // Initialize the background to the first stage
        UpdateBackground(currentStage);
    }

    public void UpdateBackground(int stage)
    {
        // Check if the stage index is valid
        if (stage >= 0 && stage < stageBackgrounds.Length)
        {
            backgroundRenderer.sprite = stageBackgrounds[stage]; // Change the background sprite
        }
        else
        {
            Debug.LogError("Invalid stage index: " + stage);
        }
    }

    public void ChangeStage(int newStage)
    {
        // Update the current stage and change the background
        currentStage = newStage;
        UpdateBackground(currentStage); // Call UpdateBackground to change the sprite
    }
}
