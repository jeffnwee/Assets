using UnityEngine;

public class ASG1_Plate1 : MonoBehaviour
{
    public bool plate1Pressed = false;
    public ASG1_GunDoor gunDoor;

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
            plate1Pressed = true;
            GetComponent<Renderer>().material = pressedMaterial;
            if (gunDoor != null)
            {
                gunDoor.CheckPlates();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crates"))
        {
            plate1Pressed = false;
            GetComponent<Renderer>().material = originalMaterial;
            if (gunDoor != null)
            {
                gunDoor.CheckPlates();
            }
        }
    }
}
