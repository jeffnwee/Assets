using UnityEngine;

public class ASG1_Wrench : MonoBehaviour
{
    [SerializeField]
    Material highlightMaterial;

    Renderer[] renderers;
    Material[] originalMaterials;

    [SerializeField]
    AudioClip collectSound;

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

    public void Collect(ASG1_PlayerBehaviour player)
    {
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        player.CollectWrench();
        Destroy(gameObject);
    }
}
