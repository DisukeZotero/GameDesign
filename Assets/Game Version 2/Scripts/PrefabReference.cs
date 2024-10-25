using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script allows you to reference and instantiate a prefab from a GameObject in the Unity scene.
public class PrefabReference : MonoBehaviour
{
    // Public variable to reference the prefab in the inspector
    public GameObject prefabToInstantiate;

    // Method to instantiate the prefab at the current position of the GameObject
    public void InstantiatePrefab()
    {
        // Check if the prefab is assigned before instantiating
        if (prefabToInstantiate != null)
        {
            // Instantiate the prefab at the current position and rotation of this GameObject
            Instantiate(prefabToInstantiate, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogWarning("Prefab reference is not assigned in the inspector.");
        }
    }
}
