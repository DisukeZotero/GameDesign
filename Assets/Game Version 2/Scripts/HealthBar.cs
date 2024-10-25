using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform bar; // Transform representing the health bar

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to set the size of the health bar
    public void SetSize(float size)
    {
        bar.localScale = new Vector2(size, 1f); // Set the scale of the bar
    }
}
