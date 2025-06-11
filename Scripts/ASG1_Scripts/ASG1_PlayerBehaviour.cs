using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ASG1_PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI evidenceCountText;

    [SerializeField]
    TextMeshProUGUI interactText;

    [SerializeField]
    TextMeshProUGUI enoughEvidenceText;

    [SerializeField]
    TextMeshProUGUI keycardCollectedText;

    [SerializeField]
    TextMeshProUGUI gunCollectedText;

    [SerializeField]
    TextMeshProUGUI collectibleInteractText;

    [SerializeField]
    TextMeshProUGUI wrenchCountText;

    [SerializeField]
    Image evidenceBackground;

    [SerializeField]
    Image wrenchBackground;

    [SerializeField]
    TextMeshProUGUI keycardText;

    [SerializeField]
    Image keycardBackground;

    [SerializeField]
    TextMeshProUGUI gunText;

    [SerializeField]
    Image gunBackground;

    [SerializeField]
    TextMeshProUGUI deathText;


    public int deathCount = 0;
    public float startTime = 0f;
    int maxHealth = 100;
    int currentHealth = 100;
    bool isDead = false;

    int hazardDamage = 20; // Damage taken from hazards
    float lastHazardTime = 0f;
    float hazardCooldown = 0.2f; // Damage once every second

    Vector3 playerSpawn;

    bool canInteract = false;
    public int score = 0;
    public int wrenchCount = 0;

    [SerializeField]
    GameObject projectile;

    [SerializeField]
    float fireStrength = 0f;

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    float interactionDistance = 5f;

    ASG1_CoinBehaviour currentCoin;
    ASG1_DoorBehaviour currentDoor;
    ASG1_DoubleDoor currentDoubleDoor;
    ASG1_StairsDoor currentStairsDoor;
    public bool hasKeycard = false;
    ASG1_Keycard currentKeycard;
    public bool hasGun = false;
    ASG1_GunBehaviour currentGun;
    ASG1_Wrench currentWrench;
    public bool powerBoxFixed = false;
    ASG1_PowerBox currentPowerBox;
    ASG1_ExitDoor currentExitDoor;

    ASG1_GunDoor currentGunDoor;

    CharacterController characterController;
    Rigidbody rb;

    void Start()
    {
        playerSpawn = transform.position;
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        startTime = Time.time;

        evidenceCountText.text = "Evidence Collected: " + score.ToString();
        wrenchCountText.text = "Wrench Collected: " + wrenchCount.ToString();
    }

    void TeleportToSpawn()
    {
        if (characterController != null)
            characterController.enabled = false;

        transform.position = playerSpawn;

        if (characterController != null)
            characterController.enabled = true;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void Update()
    {
        RaycastHit hitInfo;
        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * interactionDistance, Color.red);

        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo, interactionDistance) &&
            hitInfo.collider.gameObject.CompareTag("Collectible"))
        {
            ASG1_CoinBehaviour coin = hitInfo.collider.gameObject.GetComponentInParent<ASG1_CoinBehaviour>();
            ASG1_Keycard keycard = hitInfo.collider.gameObject.GetComponentInParent<ASG1_Keycard>();
            ASG1_GunBehaviour gun = hitInfo.collider.gameObject.GetComponentInParent<ASG1_GunBehaviour>();
            ASG1_Wrench wrench = hitInfo.collider.gameObject.GetComponentInParent<ASG1_Wrench>();
            ASG1_PowerBox powerBox = hitInfo.collider.gameObject.GetComponentInParent<ASG1_PowerBox>();

            if (coin != null)
            {
                if (currentCoin != null && currentCoin != coin)
                    currentCoin.Unhighlight();

                currentCoin = coin;
                if (currentKeycard != null)
                {
                    currentKeycard.Unhighlight();
                    currentKeycard = null;
                }
                canInteract = true;
                currentCoin.Highlight();
            }
            else if (keycard != null)
            {
                if (currentKeycard != null && currentKeycard != keycard)
                    currentKeycard.Unhighlight();

                currentKeycard = keycard;
                if (currentCoin != null)
                {
                    currentCoin.Unhighlight();
                    currentCoin = null;
                }
                canInteract = true;
                currentKeycard.Highlight();
            }
            else if (gun != null)
            {
                if (currentGun != null && currentGun != gun)
                    currentGun.Unhighlight();

                currentGun = gun;
                if (currentCoin != null)
                {
                    currentCoin.Unhighlight();
                    currentCoin = null;
                }
                if (currentKeycard != null)
                {
                    currentKeycard.Unhighlight();
                    currentKeycard = null;
                }
                canInteract = true;
                currentGun.Highlight();
            }
            else if (wrench != null)
            {
                if (currentWrench != null && currentWrench != wrench)
                    currentWrench.Unhighlight();

                currentWrench = wrench;
                if (currentCoin != null)
                {
                    currentCoin.Unhighlight();
                    currentCoin = null;
                }
                if (currentKeycard != null)
                {
                    currentKeycard.Unhighlight();
                    currentKeycard = null;
                }
                canInteract = true;
                currentWrench.Highlight();
            }
            else if (powerBox != null)
            {
                if (currentPowerBox != null && currentPowerBox != powerBox)
                    currentPowerBox.Unhighlight();

                currentPowerBox = powerBox;
                if (currentCoin != null)
                {
                    currentCoin.Unhighlight();
                    currentCoin = null;
                }
                if (currentKeycard != null)
                {
                    currentKeycard.Unhighlight();
                    currentKeycard = null;
                }
                if (currentGun != null)
                {
                    currentGun.Unhighlight();
                    currentGun = null;
                }
                if (currentWrench != null)
                {
                    currentWrench.Unhighlight();
                    currentWrench = null;
                }
                canInteract = true;
                currentPowerBox.Highlight();
            }
            if (!currentPowerBox)
            {
                collectibleInteractText.gameObject.SetActive(true);
            }
            
        }
        else
        {
            if (currentCoin != null)
            {
                currentCoin.Unhighlight();
                currentCoin = null;
            }
            if (currentKeycard != null)
            {
                currentKeycard.Unhighlight();
                currentKeycard = null;
            }
            if (currentGun != null)
            {
                currentGun.Unhighlight();
                currentGun = null;
            }
            if (currentWrench != null)
            {
                currentWrench.Unhighlight();
                currentWrench = null;
            }
            if (currentPowerBox != null)
            {
                currentPowerBox.Unhighlight();
                currentPowerBox = null;
            }
            collectibleInteractText.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HazardBall"))
        {
            Debug.Log("You died.");
            isDead = true;
            deathCount++;
            if (isDead)
            {
                TeleportToSpawn();
            }
            currentHealth = maxHealth;
            isDead = false;
        }
    }

    void OnInteract()
    {
        if (canInteract)
        {
            if (currentCoin != null)
            {
                //Debug.Log("Interacting with coin");
                currentCoin.Collect(this);
            }
            else if (currentDoor != null)
            {
                //Debug.Log("Interacting with door");
                currentDoor.Interact();
                interactText.gameObject.SetActive(false);
            }

            else if (currentDoubleDoor != null)
            {
                //Debug.Log("Interacting with double door");
                currentDoubleDoor.Interact();
                interactText.gameObject.SetActive(false);
            }

            else if (currentStairsDoor != null)
            {
                //Debug.Log("Interacting with stairs door");
                currentStairsDoor.Interact();
                interactText.gameObject.SetActive(false);
            }

            else if (currentKeycard != null)
            {
                //Debug.Log("Interacting with keycard");
                currentKeycard.Collect(this);
            }

            else if (currentGun != null)
            {
                //Debug.Log("Interacting with gun");
                currentGun.Collect(this);
            }
            else if (currentWrench != null)
            {
                //Debug.Log("Interacting with wrench");
                currentWrench.Collect(this);
            }
            else if (currentPowerBox != null)
            {
                currentPowerBox.Interact();
                interactText.gameObject.SetActive(false);
            }
            else if (currentExitDoor != null)
            {
                currentExitDoor.Interact();
                interactText.gameObject.SetActive(false);
            }
            else if (currentGunDoor != null)
            {
                currentGunDoor.Interact();
                interactText.gameObject.SetActive(false);
            }
        }
    }

    public void ModifyScore(int amount)
    {
        score += amount;
        evidenceCountText.text = "Evidence Collected: " + score.ToString();

        if (score >= 5)
        {
            enoughEvidenceText.gameObject.SetActive(true);
            Invoke("HideEnoughEvidenceText", 4f);
            evidenceCountText.gameObject.SetActive(false);
            evidenceBackground.gameObject.SetActive(false);

            keycardText.gameObject.SetActive(true);
            keycardBackground.gameObject.SetActive(true);
        }
    }
    void HideEnoughEvidenceText()
    {
        enoughEvidenceText.gameObject.SetActive(false);
    }

    public void CollectKeycard()
    {
        hasKeycard = true;
        keycardCollectedText.gameObject.SetActive(true);
        Invoke("HideKeycardCollectedText", 3f);
        keycardText.gameObject.SetActive(false);
        keycardBackground.gameObject.SetActive(false);
        gunText.gameObject.SetActive(true);
        gunBackground.gameObject.SetActive(true);
    }

    void HideKeycardCollectedText()
    {
        keycardCollectedText.gameObject.SetActive(false);
    }

    public void CollectGun()
    {
        hasGun = true;
        gunCollectedText.gameObject.SetActive(true);
        Invoke("HideGunCollectedText", 4f);
        gunText.gameObject.SetActive(false);
        gunBackground.gameObject.SetActive(false);
        wrenchCountText.gameObject.SetActive(true);
        wrenchBackground.gameObject.SetActive(true);
    }

    void HideGunCollectedText()
    {
        gunCollectedText.gameObject.SetActive(false);
    }

    public void CollectWrench(int wrenchAmount)
    {
        wrenchCount += wrenchAmount;
        wrenchCountText.text = "Wrench Collected: " + wrenchCount.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ExitDoor"))
        {
            canInteract = true;
            currentExitDoor = other.gameObject.GetComponent<ASG1_ExitDoor>();
            interactText.gameObject.SetActive(true);
        }

        else if (other.CompareTag("Door"))
        {
            canInteract = true;
            currentDoor = other.gameObject.GetComponent<ASG1_DoorBehaviour>();
            interactText.gameObject.SetActive(true);
        }

        else if (other.CompareTag("DoubleDoor"))
        {
            canInteract = true;
            currentDoubleDoor = other.gameObject.GetComponentInParent<ASG1_DoubleDoor>();
            interactText.gameObject.SetActive(true);
        }

        else if (other.CompareTag("StairsDoor"))
        {
            canInteract = true;
            currentStairsDoor = other.gameObject.GetComponentInParent<ASG1_StairsDoor>();
            interactText.gameObject.SetActive(true);
        }
        else if (other.CompareTag("GunDoor"))
        {
            canInteract = true;
            currentGunDoor = other.gameObject.GetComponent<ASG1_GunDoor>();
            interactText.gameObject.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hazard"))
        {


            if (Time.time - lastHazardTime >= hazardCooldown)
            {
                currentHealth -= hazardDamage;
                lastHazardTime = Time.time;

                if (currentHealth <= 0)
                {
                    currentHealth = 0;
                    isDead = true;
                    deathCount++;
                    deathText.gameObject.SetActive(true);
                    Invoke("HideDeathText", 2f);

                    if (isDead)
                    {
                        TeleportToSpawn();
                    }

                    currentHealth = maxHealth;
                    isDead = false;
                }
            }
        }
    }

    void HideDeathText()
    {
        deathText.gameObject.SetActive(false);
    }


    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            if (currentCoin != null)
            {
                currentCoin.Unhighlight();
                currentCoin = null;
            }
            if (currentKeycard != null)
            {
                currentKeycard.Unhighlight();
                currentKeycard = null;
            }
            if (currentGun != null)
            {
                currentGun.Unhighlight();
                currentGun = null;
            }
            if (currentWrench != null)
            {
                currentWrench.Unhighlight();
                currentWrench = null;
            }
        }

        else if (other.CompareTag("ExitDoor"))
        {
            currentExitDoor = null;
            canInteract = false;
            interactText.gameObject.SetActive(false);
        }

        else if (other.CompareTag("Door"))
        {
            currentDoor = null;
            canInteract = false;
            interactText.gameObject.SetActive(false);
        }

        else if (other.CompareTag("DoubleDoor"))
        {
            // Close the door when leaving
            if (currentDoubleDoor != null && currentDoubleDoor.isOpen)
            {
                currentDoubleDoor.CloseDoors();
            }
            currentDoubleDoor = null;
            canInteract = false;
            interactText.gameObject.SetActive(false);
        }

        else if (other.CompareTag("StairsDoor"))
        {
            // Close the door when leaving
            if (currentStairsDoor != null && currentStairsDoor.isOpen)
            {
                currentStairsDoor.CloseDoors();
            }
            currentStairsDoor = null;
            canInteract = false;
            interactText.gameObject.SetActive(false);
        }
        else if (other.CompareTag("GunDoor"))
        {
            currentGunDoor = null;
            canInteract = false;
            interactText.gameObject.SetActive(false);
        }
    }

    void OnFire()
    {
        if (!hasGun)
            return;

        GameObject newProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        Vector3 fireForce = spawnPoint.forward * fireStrength;
        newProjectile.GetComponent<Rigidbody>().AddForce(fireForce);

        Destroy(newProjectile, 2f);
    }
}