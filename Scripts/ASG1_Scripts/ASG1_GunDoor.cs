using UnityEngine;
using TMPro;

public class ASG1_GunDoor : MonoBehaviour
{
    /*
    * Author: Jeffrey Ang
    * Date: 11 June 2025
    * Description: This script handles the gun door in the game.
    * It allows the player to open the door if both plates are pressed,
    * plays a sound when the door opens or closes, and displays messages
    * when the plates are not pressed or both are pressed.
    */
    
    [SerializeField]
    TextMeshProUGUI notPressed;
    // Message displayed when the plates are not pressed

    [SerializeField]
    TextMeshProUGUI bothPressed;
    // Message displayed when both plates are pressed

    public ASG1_Plate1 plate1;
    // Reference to the first plate script
    public ASG1_Plate2 plate2;
    // Reference to the second plate script

    public bool isOpen = false;
    // Indicates whether the door is currently open
    public Transform player;
    // Reference to the player transform
    
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
        audioSource = GetComponent<AudioSource>();
    }

    public void CheckPlates()
    {   
        // Check the state of the plates and update the UI accordingly
        if (plate1.plate1Pressed && plate2.plate2Pressed)
        {
            bothPressed.gameObject.SetActive(true);
            Invoke("HideBothPressedText", 3.5f);
        }
        else
        {
            bothPressed.gameObject.SetActive(false);
        }
    }

    public void Interact()
    {
        if (isOpen) return;

        // Check if both plates are pressed
        Vector3 doorPos = transform.position;
        Vector3 playerPos = player.position;

        if (plate1.plate1Pressed && plate2.plate2Pressed)
        {

            if (playerPos.z < doorPos.z)
                OpenDoor(-90);
            else
                OpenDoor(90);
        }
        else
        {
            notPressed.gameObject.SetActive(true);
            Invoke("HideNotPressedText", 3f);
        }
    }

    private void HideNotPressedText()
    {   
        // Hide the not pressed message after a delay
        notPressed.gameObject.SetActive(false);
    }
    
    private void HideBothPressedText()
    {   
        // Hide the both pressed message after a delay
        bothPressed.gameObject.SetActive(false);
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
        if (!isOpen) return;

        // Close the door by resetting its rotation
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
