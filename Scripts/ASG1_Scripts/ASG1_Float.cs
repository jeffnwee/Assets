using UnityEngine;

public class ASG1_Float : MonoBehaviour
{
    /*
    * Author: Jeffrey Ang
    * Date: 8 June 2025
    * Description: This script makes the collectibles float up and down
    * and rotate around its Y-axis.
    */

    [SerializeField]
    float floatSpeed = 1f;
    // Speed at which the object floats up and down
    
    [SerializeField]
    float floatHeight = 0.25f;
    // Height of the floating motion

    [SerializeField]
    float rotationSpeed = 50f;
    // Speed at which the object rotates around its Y-axis

    Vector3 startPos;

    void Start()
    {   
        // Store the initial position of the object
        startPos = transform.position;
    }

    void Update()
    {
        // Floating up and down
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        // Rotating
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
