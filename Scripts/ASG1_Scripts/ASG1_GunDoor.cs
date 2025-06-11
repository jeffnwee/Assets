using UnityEngine;
using TMPro;

public class ASG1_GunDoor : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI notPressed;

    [SerializeField]
    TextMeshProUGUI bothPressed;

    public ASG1_Plate1 plate1;
    public ASG1_Plate2 plate2;

    public bool isOpen = false;
    public Transform player;
    
    [SerializeField]
    private AudioClip doorOpenSound;

    [SerializeField]
    private AudioClip doorCloseSound;

    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void CheckPlates()
    {
        if (plate1.plate1Pressed && plate2.plate2Pressed)
        {
            bothPressed.gameObject.SetActive(true);
            Invoke("HideBothPressedText", 3.5f);
        }
        else
        {
            bothPressed.gameObject.SetActive(false);
        }
    }

    public void Interact()
    {
        if (isOpen) return;

        Vector3 doorPos = transform.position;
        Vector3 playerPos = player.position;

        if (plate1.plate1Pressed && plate2.plate2Pressed)
        {

            if (playerPos.z < doorPos.z)
                OpenDoor(-90);
            else
                OpenDoor(90);
        }
        else
        {
            notPressed.gameObject.SetActive(true);
            Invoke("HideNotPressedText", 3f);
        }
    }

    private void HideNotPressedText()
    {
        notPressed.gameObject.SetActive(false);
    }
    
    private void HideBothPressedText()
    {
        bothPressed.gameObject.SetActive(false);
    }

    void OpenDoor(float angle)
    {
        transform.eulerAngles += new Vector3(0, angle, 0);
        isOpen = true;
        audioSource.clip = doorOpenSound;
        audioSource.Play();
    }

    public void CloseDoor()
    {
        if (!isOpen) return;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        isOpen = false;
        audioSource.clip = doorCloseSound;
        audioSource.Play();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            CloseDoor();
        }
    }
}
