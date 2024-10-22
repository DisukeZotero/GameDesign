using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCount : MonoBehaviour
{
    public Text coinCountText; // Reference to the UI Text component for displaying the coin count
    public Text coinsText;
    int count = 0; // Variable to store the player's coin count

    // Start is called before the first frame update
    void Start()
    {
        // Initialization logic can go here if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Update the displayed text to show the current coin count
        // Ensure coinCountText is assigned in the Unity inspector to avoid NullReferenceException
        coinCountText.text = count.ToString();
        coinsText.text = "Coins: " +count.ToString();
    }

    // Method to increase the coin count by 1
    public void AddCount()
    {
        count++; // Increment the count each time a coin is collected
        // The coinCountText will automatically update in the next frame due to the Update method
    }
}
