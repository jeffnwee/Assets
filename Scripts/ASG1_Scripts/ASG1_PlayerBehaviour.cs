using UnityEngine;

public class ASG1_PlayerBehaviour : MonoBehaviour
{
    int maxHealth = 100;
    int currentHealth = 100;
    bool isDead = false;

    int hazardDamage = 20; // Damage taken from hazards
    float lastHazardTime = 0f;
    float hazardCooldown = 0.2f; // Damage once every second

    Vector3 playerSpawn;

    bool canInteract = false;
    public int score = 0;

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    float interactionDistance = 5f;

    ASG1_CoinBehaviour currentCoin;
    ASG1_DoorBehaviour currentDoor;

    CharacterController characterController;
    Rigidbody rb;

    void Start()
    {
        playerSpawn = transform.position;
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
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
            if (currentCoin != null && currentCoin != hitInfo.collider.gameObject.GetComponentInParent<ASG1_CoinBehaviour>())
            {
                currentCoin.Unhighlight();
            }

            currentCoin = hitInfo.collider.gameObject.GetComponentInParent<ASG1_CoinBehaviour>();
            canInteract = true;
            currentCoin.Highlight();
        }
        else
        {
            if (currentCoin != null)
            {
                currentCoin.Unhighlight();
                currentCoin = null;
                canInteract = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // if (collision.gameObject.CompareTag("Hazard"))
        // {
        //     currentHealth -= 20;
        //     if (currentHealth < 0)
        //     {
        //         currentHealth = 0;
        //         isDead = true;
        //         //Debug.Log("Player has died.");
        //     }
        //     else
        //     {
        //         Debug.Log("-1 health. Current health: " + currentHealth);
        //     }
        // }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("HealingArea"))
        {
            currentHealth += 1;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                //Debug.Log("+1 health. Current health: " + currentHealth);
            }
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
            }
        }
    }

    public void ModifyScore(int amount)
    {
        score += amount;
        Debug.Log("Evidence: " + score);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            //Debug.Log("Player is looking at " + other.gameObject.name);
            //currentCoin = other.gameObject.GetComponent<CoinBehaviour>();
            //canInteract = true;
        }

        else if (other.CompareTag("Door"))
        {
            canInteract = true;
            currentDoor = other.gameObject.GetComponent<ASG1_DoorBehaviour>();
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
                    Debug.Log("You died.");

                    if (isDead)
                    {
                        TeleportToSpawn();
                    }
  
                    currentHealth = maxHealth;
                    isDead = false;
                }
                else
                {
                    Debug.Log("-1 health. Current health: " + currentHealth);
                }
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            //Debug.Log("Player is no longer looking at " + other.gameObject.name);
            currentCoin = null;
            canInteract = false;
        }

        else if (other.CompareTag("Door"))
        {
            currentDoor = null;
            canInteract = false;
        }
    }
}