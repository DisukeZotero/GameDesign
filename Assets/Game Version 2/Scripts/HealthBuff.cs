using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/HealthBuff")]
public class HealthBuff : PowerUpEffects
{
    public float amount;

    public override void Apply(GameObject target)
    {
        PlayerScript playerScript = target.GetComponent<PlayerScript>();
        if (playerScript != null)
        {
            playerScript.AdjustHealth(amount);
        }
        else
        {
            Debug.LogWarning("PlayerScript not found on target.");
        }
    }
}
