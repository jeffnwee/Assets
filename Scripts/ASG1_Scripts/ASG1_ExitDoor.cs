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

    public void Interact()
    {
        if (player != null)
        {
            if (player.powerBoxFixed)
            {
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