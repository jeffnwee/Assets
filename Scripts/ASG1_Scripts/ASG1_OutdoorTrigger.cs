using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ASG1_OutdoorTrigger : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI outdoorHint;

    [SerializeField]
    Image outdoorBackground;

    public ASG1_Plate1 plate1;
    public ASG1_Plate2 plate2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!plate1.plate1Pressed && !plate2.plate2Pressed)
            {
                outdoorHint.gameObject.SetActive(true);
                outdoorBackground.gameObject.SetActive(true);
            }
        }
    }

    public void CheckPlatesAndHideHint()
    {
        if (plate1 != null && plate2 != null && plate1.plate1Pressed && plate2.plate2Pressed)
        {
            outdoorHint.gameObject.SetActive(false);
            outdoorBackground.gameObject.SetActive(false);
        }
    }
}
