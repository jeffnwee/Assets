using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ASG1_ExitDoor : MonoBehaviour
{
    public ASG1_PlayerBehaviour player;

    [SerializeField]
    TextMeshProUGUI exitText;

    [SerializeField]
    TextMeshProUGUI deathCountText;

    [SerializeField]
    TextMeshProUGUI timeTakenText;

    [SerializeField]
    Image exitBackground;

    [SerializeField]
    TextMeshProUGUI exitFailText;
    
    [SerializeField]
    AudioSource bgmAudioSource;

    [SerializeField]
    AudioClip victorySound;

    AudioSource audioSource;

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
        exitFailText.gameObject.SetActive(false);
    }
}