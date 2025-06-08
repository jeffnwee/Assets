using UnityEngine;

public class DoorBehaviour : MonoBehaviour  
{

    public bool isOpen = false;
    public void Interact()
    {
        if (!isOpen)
        {
            Vector3 doorRotation = transform.eulerAngles;
            doorRotation.y += 90f;
            transform.eulerAngles = doorRotation;
            isOpen = true;
        }
        else
        {
            Vector3 doorRotation = transform.eulerAngles;
            doorRotation.y += -90f;
            transform.eulerAngles = doorRotation;
            isOpen = false;
        }
    }
}
