using UnityEngine;

public class ASG1_DoorBehaviour : MonoBehaviour
{
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

    public void Interact()
    {
        if (isOpen) return;

        Vector3 doorPos = transform.position;
        Vector3 playerPos = player.position;

        if (playerPos.z < doorPos.z)
            OpenDoor(-90);
        else
            OpenDoor(90);
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
