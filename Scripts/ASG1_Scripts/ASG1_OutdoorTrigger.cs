using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ASG1_OutdoorTrigger : MonoBehaviour
{
    /*
    * Author: Jeffrey Ang
    * Date: 11 June 2025
    * Description: This script handles the outdoor trigger in the game.
    * It displays a hint to the player when they enter the outdoor area,
    * and hides the hint when both plates are pressed.
    */
    
    [SerializeField]
    TextMeshProUGUI outdoorHint;
    // The hint text that will be displayed when the player enters the outdoor area
    // and both plates are not pressed

    [SerializeField]
    Image outdoorBackground;
    // The background image that will be displayed with the hint

    public ASG1_Plate1 plate1;
    // Reference to the first plate script that checks if it is pressed
    public ASG1_Plate2 plate2;
    // Reference to the second plate script that checks if it is pressed

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if both plates are not pressed
            if (!plate1.plate1Pressed && !plate2.plate2Pressed)
            {
                outdoorHint.gameObject.SetActive(true);
                outdoorBackground.gameObject.SetActive(true);
            }
        }
    }

    public void CheckPlatesAndHideHint()
    {   
        // Hide the hint if both plates are pressed
        if (plate1 != null && plate2 != null && plate1.plate1Pressed && plate2.plate2Pressed)
        {
            outdoorHint.gameObject.SetActive(false);
            outdoorBackground.gameObject.SetActive(false);
        }
    }
}
