using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/FireRateIncrease")]
public class FireRateIncrease : PowerUpEffects
{
    public float amount; // Amount to decrease the fire rate

    // Minimum fire rate limit
    private const float MinFireRate = 0.17f;

    public override void Apply(GameObject target)
    {
        // Access the Shooting component on the target
        Shooting shootingComponent = target.GetComponent<Shooting>();

        if (shootingComponent != null) // Check if the Shooting component exists
        {
            // Calculate the new fire rate
            float newFireRate = shootingComponent.fireRate - amount;

            // Ensure the new fire rate does not go below the minimum limit
            if (newFireRate < MinFireRate)
            {
                newFireRate = MinFireRate; // Set to minimum limit if below it
            }

            shootingComponent.fireRate = newFireRate; // Update the fire rate
        }
        else
        {
            Debug.LogWarning("Shooting component not found on the target.");
        }
    }
}
