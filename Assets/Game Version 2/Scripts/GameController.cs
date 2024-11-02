using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject pauseMenu; // UI element for the pause menu
    public GameObject pauseButton; // UI element for the pause button
    public GameObject gameOverPanel; // UI element for the game over screen
    public GameObject levelCompletePanel; // UI element for the level complete screen
    public GameObject endText; // UI element for the end text

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f; // Set the game time scale to normal
        // Ensure that the endText and levelCompletePanel are hidden at the start
        endText.SetActive(false);
        levelCompletePanel.SetActive(false);

        // Hide the pause menu and show the pause button when the game starts
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);

        // Ensure that the game over panel is hidden at the start
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Intentionally left empty for future functionality if needed
    }

    // Method to pause the game and show the pause menu
    public void PauseGame()
    {
        pauseMenu.SetActive(true); // Display the pause menu
        pauseButton.SetActive(false); // Hide the pause button
        Time.timeScale = 0f; // Freeze game time to pause the game
    }

    // Method to resume the game from pause state
    public void ResumeGame()
    {
        pauseMenu.SetActive(false); // Hide the pause menu
        pauseButton.SetActive(true); // Show the pause button again
        Time.timeScale = 1f; // Resume game time
    }

    // Method to display the game over screen
    public void GameOver()
    {
        gameOverPanel.SetActive(true); // Show the game over panel
        pauseButton.SetActive(false); // Hide the pause button
    }

    // Coroutine to handle the level completion sequence
    public IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds before showing the end text
        endText.SetActive(true); // Show the end text

        yield return new WaitForSeconds(3f); // Wait for 3 more seconds before displaying the level complete panel
        Time.timeScale = 0f; // Pause the game time
        levelCompletePanel.SetActive(true); // Show the level complete panel
    }

    // Method to quit the game (only works in a built version, not in the editor)
    public void QuitGame()
    {
        Application.Quit(); // Close the application
    }
}
