using UnityEngine;

public class ASG1_Float : MonoBehaviour
{
    [SerializeField] float floatSpeed = 1f;
    [SerializeField] float floatHeight = 0.5f;
    [SerializeField] float rotationSpeed = 50f;

    Vector3 startPos;

    void Start()
    {
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
