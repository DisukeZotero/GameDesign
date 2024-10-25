using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEffects powerUpEffects;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        powerUpEffects.Apply(collision.gameObject);
    }
}
