using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ASG1_StairsTrigger : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI stairsHint;

    [SerializeField]
    Image stairsBackground;

    public ASG1_PlayerBehaviour playerBehaviour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playerBehaviour.hasKeycard)
            {
                stairsHint.gameObject.SetActive(true);
                stairsBackground.gameObject.SetActive(true);
            }
        }
    }

    public void CheckForKeycard()
    {
        if (playerBehaviour.hasKeycard)
        {
            stairsHint.gameObject.SetActive(false);
            stairsBackground.gameObject.SetActive(false);
        }
    }
}
