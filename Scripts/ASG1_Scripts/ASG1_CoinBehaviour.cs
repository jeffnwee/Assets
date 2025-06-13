using UnityEngine;

public class ASG1_CoinBehaviour : MonoBehaviour
{
    /*
    * Author: Jeffrey Ang
    * Date: 8 June 2025
    * Description: This script handles the coin object in the game.
    * It allows the player to collect coins, highlights them when hovered over,
    * and plays a sound when collected.
    */
    
    [SerializeField]
    public int coinValue = 1;
    // The amount of evidence that will be added to the player's evidence count

    [SerializeField]
    Material highlightMaterial;
    // Material to highlight the collectible when hovered over

    Renderer[] renderers;
    // Array to store the Renderer components of this GameObject and its children
    Material[] originalMaterials;
    // Array to store the original materials of the renderers

    [SerializeField]
    AudioClip collectSound;
    // Sound to play when the collectible is collected

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
        // Play the collection sound and modify the player's score
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        player.ModifyScore(coinValue);
        Destroy(gameObject);
    }
}
