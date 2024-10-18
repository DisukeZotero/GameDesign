using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Renderer meshRender;
    public float speed = 0.1f; // Added a semicolon to end the statement

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current offset of the material's main texture
        Vector2 offset = meshRender.material.mainTextureOffset;

        // Increment the offset based on speed and time
        offset = offset + new Vector2(0, speed * Time.deltaTime);

        // Apply the updated offset back to the material's texture
        meshRender.material.mainTextureOffset = offset; // Removed the extra semicolon after mainTextureOffset

        meshRender.material.mainTextureOffset += new Vector2(0, speed * Time.deltaTime);
    }
}
