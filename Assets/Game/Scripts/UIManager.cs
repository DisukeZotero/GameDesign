using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text stageText; // Reference to the UI Text component for stage display

    // Method to update the stage UI
    public void OnStageChanged(int stage)
    {
        if (stageText != null)
        {
            stageText.text = "Stage: " + stage; // Update the displayed stage number
        }
        else
        {
            Debug.LogError("Stage Text reference is not set in UIManager.");
        }
    }
}
