using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Variables should be here!!!
    string output = "";
    int variable1 = 50;
    int variable2 = 13;
    int incrementNo = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Task 1
        for (int i = 1; i < 11; i++)
        {
            output += i + " ";
        }

        Debug.Log(output);

        // Task
        while (variable1 != variable2)
        {   
            if (variable1 < variable2)
            {
                variable1++;
                incrementNo++;
            }

            else if (variable1 > variable2)
            {
                variable1--;
                incrementNo++;
            }
        }

        Debug.Log(incrementNo + " increments were made to variable1 to reach variable2.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
