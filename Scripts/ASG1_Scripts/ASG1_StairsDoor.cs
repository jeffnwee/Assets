using UnityEngine;

public class ASG1_StairsDoor : MonoBehaviour
{
    public bool isOpen = false;
    public Transform player;

    [SerializeField]
    private Transform leftDoor;

    [SerializeField]
    private Transform rightDoor;

    [SerializeField]
    private AudioClip doorOpenSound;

    [SerializeField]
    private AudioClip doorCloseSound;

    private AudioSource audioSource;

    private Vector3 leftDoorClosedRotation;
    private Vector3 rightDoorClosedRotation;

    public ASG1_PlayerBehaviour playerBehaviour;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        leftDoorClosedRotation = leftDoor.localEulerAngles;
        rightDoorClosedRotation = rightDoor.localEulerAngles;
    }

    public void Interact()
    {
        if (isOpen) return;

        if (playerBehaviour != null && playerBehaviour.score >= 5)
        {
            Vector3 doorPos = transform.position;
            Vector3 playerPos = player.position;

            if (playerPos.z < doorPos.z)
                OpenDoors(-90, 90);
            else
                OpenDoors(90, -90);
        }

        else
        {
            Debug.Log("You need to collect all 5 evidence to open this door.");
        }
    }

    void OpenDoors(float leftAngle, float rightAngle)
    {
        leftDoor.localEulerAngles = leftDoorClosedRotation + new Vector3(0, leftAngle, 0);
        rightDoor.localEulerAngles = rightDoorClosedRotation + new Vector3(0, rightAngle, 0);
        isOpen = true;
        audioSource.clip = doorOpenSound;
        audioSource.Play();
    }

    public void CloseDoors()
    {
        if (!isOpen) return;
        leftDoor.localEulerAngles = leftDoorClosedRotation;
        rightDoor.localEulerAngles = rightDoorClosedRotation;
        isOpen = false;
        audioSource.clip = doorCloseSound;
        audioSource.Play();
    }
}