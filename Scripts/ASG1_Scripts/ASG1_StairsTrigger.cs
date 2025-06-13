using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ASG1_StairsTrigger : MonoBehaviour
{
    /*
    * Author: Jeffrey Ang
    * Date: 11 June 2025
    * Description: This script handles the stairs trigger in the game.
    * It displays a hint to the player when they enter the stairs area,
    * and hides the hint if the player has a keycard.
    */

    [SerializeField]
    TextMeshProUGUI stairsHint;
    // The hint text that will be displayed when the player enters the stairs area

    [SerializeField]
    Image stairsBackground;
    // The background image that will be displayed with the hint

    public ASG1_PlayerBehaviour playerBehaviour;
    // Reference to the player behaviour script to check for keycard

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
