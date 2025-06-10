using UnityEngine;

public class ASG1_PowerBox : MonoBehaviour
{
    [SerializeField]
    Material highlightMaterial;

    Renderer[] renderers;
    Material[] originalMaterials;

    public ASG1_PlayerBehaviour player;

    [SerializeField]
    AudioClip fixSound;

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
        foreach (Renderer r in renderers)
        {
            r.material = highlightMaterial;
        }
    }

    public void Unhighlight()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = originalMaterials[i];
        }
    }

    public void Interact()
    {
        if (player != null && player.wrenchCount >= 3 && !player.powerBoxFixed)
        {
            AudioSource.PlayClipAtPoint(fixSound, transform.position);
            player.powerBoxFixed = true;
            Debug.Log("Power box fixed!");
        }
        else if (player != null && player.wrenchCount < 3)
        {
            Debug.Log("You need 3 wrenches to fix the power box.");
        }
        else if (player != null && player.powerBoxFixed)
        {
            Debug.Log("Power box is already fixed.");
        }
    }
}