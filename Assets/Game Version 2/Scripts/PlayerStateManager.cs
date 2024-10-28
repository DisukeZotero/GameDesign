using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance { get; private set; }

    public float shootSpeed = 0.5f; // Default fire rate, adjust this value based on your game's requirements

    private void Awake()
    {
        // Ensure that this instance persists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpgradeShootSpeed(float amount)
    {
        shootSpeed = Mathf.Max(0.1f, shootSpeed - amount); // Decrease fire rate, ensuring it doesn’t go below 0.1 seconds
    }
}
