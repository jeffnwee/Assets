using UnityEngine;

public class ASG1_Plate2 : MonoBehaviour
{
    /*
    * Author: Jeffrey Ang
    * Date: 11 June 2025
    * Description: This script handles the second plate in the game.
    * It detects when a crate is placed on it, changes its material to indicate it has been pressed,
    * and plays a sound. It also communicates with the gun door and outdoor trigger to check the state of the plates.
    */
    
    public bool plate2Pressed = false;
    // Indicates whether the plate is pressed
    public ASG1_GunDoor gunDoor;
    // Reference to the gun door script that checks if both plates are pressed
    public ASG1_OutdoorTrigger outdoorTrigger;
    // Reference to the outdoor trigger script that checks if both plates are pressed

    [SerializeField]
    Material pressedMaterial;
    // Material to apply when the plate is pressed

    Material originalMaterial;
    // Original material of the plate

    [SerializeField]
    private AudioClip platePressSound;
    // Sound to play when the plate is pressed
    private AudioSource audioSource;
    // AudioSource component to play sounds

    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crates"))
        {   
            // Mark the plate as pressed and change its material
            plate2Pressed = true;
            GetComponent<Renderer>().material = pressedMaterial;
            audioSource.clip = platePressSound;
            audioSource.Play();

            if (gunDoor != null)
            {   
                // Notify the gun door to check the state of the plates
                gunDoor.CheckPlates();
            }
            if (outdoorTrigger != null)
            {
                // Notify the outdoor trigger to check the state of the plates and hide the hint if necessary
                outdoorTrigger.CheckPlatesAndHideHint();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crates"))
        {
            plate2Pressed = false;
            GetComponent<Renderer>().material = originalMaterial;
            if (gunDoor != null)
            {
                gunDoor.CheckPlates();
            }
            if (outdoorTrigger != null)
            {
                outdoorTrigger.CheckPlatesAndHideHint();
            }
        }
    }
}
