using UnityEngine;

public class ASG1_Wrench : MonoBehaviour
{
    /*
    * Author: Jeffrey Ang
    * Date: 10 June 2025
    * Description: This script handles the wrench collectible in the game.
    * It allows the player to collect the wrench, highlights it when hovered over,
    * and plays a sound when collected.
    */
    
    [SerializeField]
    public int wrenchValue = 1;
    // The amount of wrench that will be added to the player's wrench count

    [SerializeField]
    Material highlightMaterial;
    // Material to highlight the wrench when hovered over

    Renderer[] renderers;
    // Array to store the Renderer components of this GameObject and its children
    Material[] originalMaterials;
    // Array to store the original materials of the renderers

    [SerializeField]
    AudioClip collectSound;
    // Sound to play when the wrench is collected

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

    public void Collect(ASG1_PlayerBehaviour player)
    {   
        // Play the collection sound and give the player a wrench
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        player.CollectWrench(wrenchValue);
        Destroy(gameObject);
    }
}
