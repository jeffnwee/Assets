using UnityEngine;

public class ASG1_Keycard : MonoBehaviour
{
    [SerializeField]
    Material highlightMaterial;

    Material originalMaterial;

    [SerializeField]
    AudioClip collectSound;

    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
    }

    public void Highlight()
    {
        GetComponent<Renderer>().material = highlightMaterial;
    }

    public void Unhighlight()
    {
        GetComponent<Renderer>().material = originalMaterial;
    }

    public void Collect(ASG1_PlayerBehaviour player)
    {
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        player.CollectKeycard();
        Destroy(gameObject);
    }
}