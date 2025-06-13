using UnityEngine;
using TMPro;

public class ASG1_PowerBox : MonoBehaviour
{
    /*
    * Author: Jeffrey Ang
    * Date: 10 June 2025 
    * Description: This script handles the power box object in the game.
    * It allows the player to interact with the power box to fix it if they have enough wrenches,
    * highlights it when hovered over, and plays a sound when fixed.
    */
    
    [SerializeField]
    Material highlightMaterial;
    // Material to highlight the power box when hovered over

    Renderer[] renderers;
    // Array to store the Renderer components of this GameObject and its children
    Material[] originalMaterials;
    // Array to store the original materials of the renderers

    public ASG1_PlayerBehaviour player;
    // Reference to the player script to check wrench count and fix status

    [SerializeField]
    AudioClip fixSound;
    // Sound to play when the power box is fixed

    [SerializeField]
    TextMeshProUGUI powerBoxFixed;
    // Text to display when the power box is successfully fixed

    [SerializeField]
    TextMeshProUGUI powerBoxNotFixed;
    // Text to display when the player does not have enough wrenches to fix the power box

    [SerializeField]
    TextMeshProUGUI powerBoxAlreadyFixed;
    // Text to display when the power box is already fixed


    void Start()
    {
        // Get all Renderer components in this GameObject and its children
        renderers = GetComponentsInChildren<Renderer>();
        originalMaterials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            originalMaterials[i] = renderers[i].material;
        }

    }

    public void Highlight()
    {   
        // Change the material of each renderer to the highlight material
        foreach (Renderer r in renderers)
        {
            r.material = highlightMaterial;
        }
    }

    public void Unhighlight()
    {   
        // Restore the original material of each renderer
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = originalMaterials[i];
        }
    }

    public void Interact()
    {   
        // Check if the player has enough wrenches to fix the power box
        if (player != null && player.wrenchCount >= 3 && !player.powerBoxFixed)
        {
            AudioSource.PlayClipAtPoint(fixSound, transform.position);
            player.powerBoxFixed = true;
            powerBoxFixed.gameObject.SetActive(true);
            Invoke("HidePowerBoxFixedText", 2f);
        }
        else if (player != null && player.wrenchCount < 3)
        {
            powerBoxNotFixed.gameObject.SetActive(true);
            Invoke("HidePowerBoxNotFixedText", 3f);
        }
        else if (player != null && player.powerBoxFixed)
        {
            powerBoxAlreadyFixed.gameObject.SetActive(true);
            Invoke("HidePowerBoxAlreadyFixedText", 2.5f);
        }
    }

    private void HidePowerBoxFixedText()
    {   
        // Hide the text indicating the power box has been fixed
        powerBoxFixed.gameObject.SetActive(false);
    }

    private void HidePowerBoxNotFixedText()
    {
        // Hide the text indicating the player does not have enough wrenches
        powerBoxNotFixed.gameObject.SetActive(false);
    }

    private void HidePowerBoxAlreadyFixedText()
    {   
        // Hide the text indicating the power box is already fixed
        powerBoxAlreadyFixed.gameObject.SetActive(false);
    }
}