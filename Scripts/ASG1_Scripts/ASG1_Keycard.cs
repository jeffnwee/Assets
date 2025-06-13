using UnityEngine;

public class ASG1_Keycard : MonoBehaviour
{
    /*
    * Author: Jeffrey Ang
    * Date: 10 June 2025
    * Description: This script handles the keycard object in the game.
    * It allows the player to collect the keycard, highlights it when hovered over,
    * and plays a sound when collected.
    */

    [SerializeField]
    Material highlightMaterial;
    // Material to highlight the keycard when hovered over

    Material originalMaterial;
    // Original material of the keycard

    [SerializeField]
    AudioClip collectSound;
    // Sound to play when the keycard is collected

    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
    }

    public void Highlight()
    {   
        // Change the material of the keycard to the highlight material
        GetComponent<Renderer>().material = highlightMaterial;
    }

    public void Unhighlight()
    {   
        // Restore the original material of the keycard
        GetComponent<Renderer>().material = originalMaterial;
    }

    public void Collect(ASG1_PlayerBehaviour player)
    {   
        // Play the collection sound and give the player a keycard
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        player.CollectKeycard();
        Destroy(gameObject);
    }
}