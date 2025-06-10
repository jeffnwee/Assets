using UnityEngine;

public class ASG1_ExitDoor : MonoBehaviour
{
    public ASG1_PlayerBehaviour player;

    public void Interact()
    {
        if (player != null)
        {
            if (player.powerBoxFixed)
            {
                float totalTime = Time.time - player.startTime;
                Debug.Log("You have successfully escaped the research laboratory!");
                Debug.Log("Total Deaths = " + player.deathCount);
                Debug.Log("Total Time Taken = " + totalTime.ToString("F2") + " seconds");
            }
            else
            {
                Debug.Log("You need to fix the power box before escaping!");
            }
        }
    }
}