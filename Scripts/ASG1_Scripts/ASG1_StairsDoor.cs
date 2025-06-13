using UnityEngine;
using TMPro;

public class ASG1_StairsDoor : MonoBehaviour
{
    /*
    * Author: Jeffrey Ang
    * Date: 10 June 2025
    * Description: This script handles the double door at the stairs in the game.
    * It allows the player to open the doors if they have enough evidence,
    * plays sounds when the doors are opened or closed, and displays a message
    * if the player does not have enough evidence.
    */

    [SerializeField]
    TextMeshProUGUI doubleDoorText;
    // UI text to display when the player does not have enough evidence
    
    public bool isOpen = false;
    // Indicates whether the doors are currently open
    public Transform player;
    // Reference to the player transform

    [SerializeField]
    private Transform leftDoor;
    // Reference to the left door transform

    [SerializeField]
    private Transform rightDoor;
    // Reference to the right door transform

    [SerializeField]
    private AudioClip doorOpenSound;
    // Sound to play when the doors open

    [SerializeField]
    private AudioClip doorCloseSound;
    // Sound to play when the doors close

    private AudioSource audioSource;
    // AudioSource component to play sounds

    private Vector3 leftDoorClosedRotation;
    // Stores the initial closed rotation of the left door
    private Vector3 rightDoorClosedRotation;
    // Stores the initial closed rotation of the right door

    public ASG1_PlayerBehaviour playerBehaviour;
    // Reference to the player behaviour script to check for score

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        leftDoorClosedRotation = leftDoor.localEulerAngles;
        rightDoorClosedRotation = rightDoor.localEulerAngles;
    }

    public void Interact()
    {
        if (isOpen) return;

        // Check if the player has enough score to open the doors
        if (playerBehaviour != null && playerBehaviour.score >= 5)
        {
            Vector3 doorPos = transform.position;
            Vector3 playerPos = player.position;

            if (playerPos.z < doorPos.z)
                OpenDoors(-90, 90);
            else
                OpenDoors(90, -90);
        }

        else
        {
            doubleDoorText.gameObject.SetActive(true);
            Invoke("HideDoubleDoorText", 3f);
        }
    }

    private void HideDoubleDoorText()
    {   
        // Hide the double door text after a delay
        doubleDoorText.gameObject.SetActive(false);
    }

    void OpenDoors(float leftAngle, float rightAngle)
    {   
        // Rotate the doors around their pivot points
        leftDoor.localEulerAngles = leftDoorClosedRotation + new Vector3(0, leftAngle, 0);
        rightDoor.localEulerAngles = rightDoorClosedRotation + new Vector3(0, rightAngle, 0);
        isOpen = true;
        audioSource.clip = doorOpenSound;
        audioSource.Play();
    }

    public void CloseDoors()
    {
        if (!isOpen) return;

        // Reset the doors to their closed rotation
        leftDoor.localEulerAngles = leftDoorClosedRotation;
        rightDoor.localEulerAngles = rightDoorClosedRotation;
        isOpen = false;
        audioSource.clip = doorCloseSound;
        audioSource.Play();
    }
}