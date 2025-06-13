using UnityEngine;

public class ASG1_DoorBehaviour : MonoBehaviour
{
    /*
    * Author: Jeffrey Ang
    * Date: 8 June 2025
    * Description: This script handles the door object in the game.
    * It allows the player to interact with the door to open or close it,
    * plays sounds when the door is opened or closed, and automatically closes
    * when the player exits the trigger area.
    */
    
    public bool isOpen = false;
    // Boolean to track if the door is currently open
    public Transform player;
    // Reference to the player transform for interaction
    
    [SerializeField]
    private AudioClip doorOpenSound;
    // Sound to play when the door opens

    [SerializeField]
    private AudioClip doorCloseSound;
    // Sound to play when the door closes

    private AudioSource audioSource;
    // AudioSource component to play sounds

    public void Start()
    {   
        // Initialize the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        if (isOpen) return;

        // Check if the player is close enough to interact with the door
        Vector3 doorPos = transform.position;
        Vector3 playerPos = player.position;

        if (playerPos.z < doorPos.z)
            OpenDoor(-90);
        else
            OpenDoor(90);
    }

    void OpenDoor(float angle)
    {   
        // Rotate the door around the pivot point
        transform.eulerAngles += new Vector3(0, angle, 0);
        isOpen = true;
        audioSource.clip = doorOpenSound;
        audioSource.Play();
    }

    public void CloseDoor()
    {   
        // Close the door by resetting its rotation
        if (!isOpen) return;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        isOpen = false;
        audioSource.clip = doorCloseSound;
        audioSource.Play();
    }

    void OnTriggerExit(Collider other)
    {   
        // Automatically close the door when the player exits the trigger area
        if (other.CompareTag("Player") && isOpen)
        {
            CloseDoor();
        }
    }
}
