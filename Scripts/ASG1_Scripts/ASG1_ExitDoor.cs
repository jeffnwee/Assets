using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ASG1_ExitDoor : MonoBehaviour
{
    /*
    * Author: Jeffrey Ang
    * Date: 10 June 2025
    * Description: This script handles the exit door object in the game.
    * It allows the player to exit the game if they have fixed the power box,
    * plays a victory sound, and displays the player's death count and time taken.
    */

    public ASG1_PlayerBehaviour player;
    // Reference to the player script that contains data for death count and time taken

    [SerializeField]
    TextMeshProUGUI exitText;
    // UI text to display when the player can exit

    [SerializeField]
    TextMeshProUGUI deathCountText;
    // UI text to display the total death count

    [SerializeField]
    TextMeshProUGUI timeTakenText;
    // UI text to display the time taken to complete the game

    [SerializeField]
    Image exitBackground;
    // Background image for the exit UI

    [SerializeField]
    TextMeshProUGUI exitFailText;
    // UI text to display when the player tries to exit without fixing the power box
    
    [SerializeField]
    AudioSource bgmAudioSource;
    // Reference to the background music audio source to stop it when exiting

    [SerializeField]
    AudioClip victorySound;
    // Sound to play when the player successfully exits

    AudioSource audioSource;
    // AudioSource component to play the victory sound

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Interact()
    {   
        if (player != null)
        {   
            // Check if the player has fixed the power box      
            if (player.powerBoxFixed)
            {   
                // Stop the BGM
                if (bgmAudioSource != null)
                {
                    bgmAudioSource.Stop();
                }

                audioSource.clip = victorySound;
                audioSource.Play();

                // Display the exit text and stats
                float totalTime = Time.time - player.startTime;
                int totalSeconds = (int)totalTime;

                int minutes = totalSeconds / 60;
                int seconds = totalSeconds % 60;
                string formattedTime = minutes + ":" + seconds.ToString("D2");

                deathCountText.text = "Total Deaths: " + player.deathCount;
                timeTakenText.text = "Time Taken: " + formattedTime;

                exitText.gameObject.SetActive(true);
                deathCountText.gameObject.SetActive(true);
                timeTakenText.gameObject.SetActive(true);
                exitBackground.gameObject.SetActive(true);
            }
            else
            {
                exitFailText.gameObject.SetActive(true);
                Invoke("HideExitFailText", 2f);
            }
        }
    }

    private void HideExitFailText()
    {   
        // Hide the exit fail text after a delay
        exitFailText.gameObject.SetActive(false);
    }
}