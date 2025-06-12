using UnityEngine;

public class ASG1_Plate2 : MonoBehaviour
{
    public bool plate2Pressed = false;
    public ASG1_GunDoor gunDoor;
    public ASG1_OutdoorTrigger outdoorTrigger;

    [SerializeField]
    Material pressedMaterial;

    Material originalMaterial;

    [SerializeField]
    private AudioClip platePressSound;
    private AudioSource audioSource;

    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crates"))
        {
            plate2Pressed = true;
            GetComponent<Renderer>().material = pressedMaterial;
            audioSource.clip = platePressSound;
            audioSource.Play();
            
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
