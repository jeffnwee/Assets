using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
[SerializeField]
    public int coinValue = 1;

    [SerializeField]
    Material highlightMaterial;

    Material originalMaterial;

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

    public void Collect(PlayerBehaviour player)
    {
        player.ModifyScore(coinValue);
        Destroy(gameObject);
    }
}
