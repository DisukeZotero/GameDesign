using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHell : MonoBehaviour
{
    private Vector2 moveDirection; // Direction in which the bullet will move
    private float moveSpeed; // Speed of the bullet

    private void OnEnable()
    {
        Invoke("Destroy", 3f); // Schedule destruction of the bullet after 3 seconds
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5f; // Initialize bullet speed
    }

    // Update is called once per frame
    void Update()
    {
        // Move the bullet in the set direction at the defined speed
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    // Method to set the movement direction of the bullet
    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir; // Assign the provided direction to the moveDirection variable
    }

    // Method to deactivate the bullet
    private void Destroy()
    {
        gameObject.SetActive(false); // Deactivate the bullet GameObject
    }

    private void OnDisable()
    {
        CancelInvoke(); // Cancel any scheduled destruction when the bullet is disabled
    }
}
