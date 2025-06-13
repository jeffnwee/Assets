using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ASG1_PlayerBehaviour : MonoBehaviour
{
    /*
    * Author: Jeffrey Ang
    * Date: 8 June 2025
    * Description: This script handles the player character's behavior in the game.
    * It manages player interactions, health, score, and collectibles.
    * The player can collect evidence, keycards, guns, and wrenches, interact with doors,  
    * and handle hazards. The player can also teleport to a spawn point upon death.
    */

    [SerializeField]
    TextMeshProUGUI evidenceCountText;
    // Text to display the number of evidence collected

    [SerializeField]
    TextMeshProUGUI interactText;
    // Text to display when the player can interact with an object

    [SerializeField]
    TextMeshProUGUI enoughEvidenceText;
    // Text to display when the player has enough evidence to open the stairs door

    [SerializeField]
    TextMeshProUGUI keycardCollectedText;
    // Text to display when the player collects a keycard

    [SerializeField]
    TextMeshProUGUI gunCollectedText;
    // Text to display when the player collects a gun

    [SerializeField]
    TextMeshProUGUI collectibleInteractText;
    // Text to display when the player can interact with a collectible

    [SerializeField]
    TextMeshProUGUI wrenchCountText;
    // Text to display the number of wrenches collected

    [SerializeField]
    Image evidenceBackground;
    // Background image for the evidence count text

    [SerializeField]
    Image wrenchBackground;
    // Background image for the wrench count text

    [SerializeField]
    TextMeshProUGUI keycardText;
    // Text to display when the player has collected enough evidence for a keycard

    [SerializeField]
    Image keycardBackground;
    // Background image for the keycard text

    [SerializeField]
    TextMeshProUGUI gunText;
    // Text to display when the player has collected a gun

    [SerializeField]
    Image gunBackground;
    // Background image for the gun text

    [SerializeField]
    TextMeshProUGUI deathText;
    // Text to display when the player dies

    [SerializeField]
    AudioClip gunFireSound;
    // Sound to play when the player fires the gun

    AudioSource audioSource;
    // AudioSource component to play sounds


    public int deathCount = 0;
    // Counter for the number of times the player has died
    public float startTime = 0f;
    // Time when the game started
    int maxHealth = 100;
    // Maximum health of the player
    int currentHealth = 100;
    // Current health of the player
    bool isDead = false;
    // Boolean to check if the player is dead

    int hazardDamage = 20;
    // Damage taken from hazards
    float lastHazardTime = 0f;
    // Last time the player took damage from a hazard
    float hazardCooldown = 0.2f;
    // Damage once every 0.2s

    Vector3 playerSpawn;
    // Player spawn position

    bool canInteract = false;
    // Boolean to check if the player can interact with objects
    public int score = 0;
    // Amount of evidence collected by the player
    public int wrenchCount = 0;
    // Amount of wrenches collected by the player

    [SerializeField]
    GameObject projectile;
    // Projectile prefab for the gun

    [SerializeField]
    float fireStrength = 0f;
    // Strength of the projectile when fired

    [SerializeField]
    Transform spawnPoint;
    // Spawn point for the projectile

    [SerializeField]
    float interactionDistance = 5f;
    // Distance within which the player can interact with objects

    ASG1_CoinBehaviour currentCoin;
    // Current evidence collectible the player is interacting with
    ASG1_DoorBehaviour currentDoor;
    // Current door the player is interacting with
    ASG1_DoubleDoor currentDoubleDoor;
    // Current double door the player is interacting with
    ASG1_StairsDoor currentStairsDoor;
    // Current stairs door the player is interacting with
    public bool hasKeycard = false;
    // Boolean to check if the player has collected a keycard
    ASG1_Keycard currentKeycard;
    // Current keycard the player is interacting with
    public bool hasGun = false;
    // Boolean to check if the player has collected a gun
    ASG1_GunBehaviour currentGun;
    // Current gun the player is interacting with
    ASG1_Wrench currentWrench;
    // Current wrench the player is interacting with
    public bool powerBoxFixed = false;
    // Boolean to check if the power box has been fixed
    ASG1_PowerBox currentPowerBox;
    // Current power box the player is interacting with
    ASG1_ExitDoor currentExitDoor;
    // Current exit door the player is interacting with

    ASG1_GunDoor currentGunDoor;
    // Current gun door the player is interacting with
    public ASG1_StairsTrigger stairsTrigger;
    // Reference to the stairs trigger to check for keycard

    CharacterController characterController;
    // CharacterController component for player movement
    Rigidbody rb;
    // Rigidbody component for player physics

    void Start()
    {   
        // Initialize player spawn position and components
        playerSpawn = transform.position;
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        startTime = Time.time;

        // Initialize UI texts
        evidenceCountText.text = "Evidence Collected: " + score.ToString();
        wrenchCountText.text = "Wrench Collected: " + wrenchCount.ToString();

        audioSource = GetComponent<AudioSource>();
    }

    void TeleportToSpawn()
    {   
        // Teleport the player to the spawn position
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
        // Check for player input to interact with objects
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
            // Handle collision with hazard ball
            deathText.gameObject.SetActive(true);
            Invoke("HideDeathText", 2f);
            isDead = true;
            deathCount++;
            if (isDead)
            {   
                // Teleport the player to the spawn point upon death
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
        // Modify the score based on the amount collected
        score += amount;
        evidenceCountText.text = "Evidence Collected: " + score.ToString();

        if (score >= 5)
        {   
            // If enough evidence is collected, show the message and update UI
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
        // Hide the enough evidence text after a delay
        enoughEvidenceText.gameObject.SetActive(false);
    }

    public void CollectKeycard()
    {   
        // Collect the keycard and update UI
        hasKeycard = true;
        keycardCollectedText.gameObject.SetActive(true);
        Invoke("HideKeycardCollectedText", 3f);
        keycardText.gameObject.SetActive(false);
        keycardBackground.gameObject.SetActive(false);
        gunText.gameObject.SetActive(true);
        gunBackground.gameObject.SetActive(true);

        if (stairsTrigger != null)
        {   
            // Check for keycard in stairs trigger
            stairsTrigger.CheckForKeycard();
        }
    }

    void HideKeycardCollectedText()
    {   
        // Hide the keycard collected text after a delay
        keycardCollectedText.gameObject.SetActive(false);
    }

    public void CollectGun()
    {   
        // Collect the gun and update UI
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
        // Hide the gun collected text after a delay
        gunCollectedText.gameObject.SetActive(false);
    }

    public void CollectWrench(int wrenchAmount)
    {   
        // Collect the wrench and update UI
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

            // Handle hazard damage over time
            if (Time.time - lastHazardTime >= hazardCooldown)
            {
                currentHealth -= hazardDamage;
                lastHazardTime = Time.time;

                if (currentHealth <= 0)
                {   
                    // Handle player death
                    currentHealth = 0;
                    isDead = true;
                    deathCount++;
                    deathText.gameObject.SetActive(true);
                    Invoke("HideDeathText", 2f);

                    if (isDead)
                    {   
                        // Teleport the player to the spawn point upon death
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
        // Hide the death text after a delay
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
        // Fire the gun if the player has it
        if (!hasGun)
            return;

        GameObject newProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        Vector3 fireForce = spawnPoint.forward * fireStrength;
        newProjectile.GetComponent<Rigidbody>().AddForce(fireForce);

        Destroy(newProjectile, 2f);

        audioSource.clip = gunFireSound;
        audioSource.Play();
    }
}