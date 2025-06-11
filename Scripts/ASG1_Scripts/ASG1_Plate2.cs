using UnityEngine;

public class ASG1_Plate2 : MonoBehaviour
{
    public bool plate2Pressed = false;
    public ASG1_GunDoor gunDoor;
    public ASG1_OutdoorTrigger outdoorTrigger;

    [SerializeField]
    Material pressedMaterial;

    Material originalMaterial;

    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crates"))
        {
            plate2Pressed = true;
            GetComponent<Renderer>().material = pressedMaterial;
            if (gunDoor != null)
            {
                gunDoor.CheckPlates();
            }
            if (outdoorTrigger != null)
            {
                outdoorTrigger.CheckPlatesAndHideHint();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crates"))
        {
            plate2Pressed = false;
            GetComponent<Renderer>().material = originalMaterial;
            if (gunDoor != null)
            {
                gunDoor.CheckPlates();
            }
            if (outdoorTrigger != null)
            {
                outdoorTrigger.CheckPlatesAndHideHint();
            }
        }
    }
}
