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
            // Show the stairs hint if the player does not have a keycard
            if (!playerBehaviour.hasKeycard)
            {
                stairsHint.gameObject.SetActive(true);
                stairsBackground.gameObject.SetActive(true);
            }
        }
    }

    public void CheckForKeycard()
    {   
        // Hide the stairs hint and background if the player has a keycard
        if (playerBehaviour.hasKeycard)
        {
            stairsHint.gameObject.SetActive(false);
            stairsBackground.gameObject.SetActive(false);
        }
    }
}
