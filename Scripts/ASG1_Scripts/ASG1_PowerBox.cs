using UnityEngine;
using TMPro;

public class ASG1_PowerBox : MonoBehaviour
{
    [SerializeField]
    Material highlightMaterial;

    Renderer[] renderers;
    Material[] originalMaterials;

    public ASG1_PlayerBehaviour player;

    [SerializeField]
    AudioClip fixSound;

    [SerializeField]
    TextMeshProUGUI powerBoxFixed;

    [SerializeField]
    TextMeshProUGUI powerBoxNotFixed;

    [SerializeField]
    TextMeshProUGUI powerBoxAlreadyFixed;


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
        powerBoxFixed.gameObject.SetActive(false);
    }

    private void HidePowerBoxNotFixedText()
    {
        powerBoxNotFixed.gameObject.SetActive(false);
    }

    private void HidePowerBoxAlreadyFixedText()
    {
        powerBoxAlreadyFixed.gameObject.SetActive(false);
    }
}