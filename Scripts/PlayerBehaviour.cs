using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    int maxHealth = 100;
    int currentHealth = 100;
    //bool isDead = false;

    bool canInteract = false;
    public int score = 0;

    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform spawnPoint; 

    [SerializeField]
    float fireStrength = 0f;

    [SerializeField]
    float interactionDistance = 5f;
    
    CoinBehaviour currentCoin;
    DoorBehaviour currentDoor;

    void Update()
    {
        RaycastHit hitInfo;
        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * interactionDistance, Color.red);
        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo, interactionDistance))
        {
            //Debug.Log("Raycast hit: " + hitInfo.collider.gameObject.name);
            if (hitInfo.collider.gameObject.CompareTag("Collectible"))
            {
                if (currentCoin != null)
                {
                    currentCoin.Unhighlight();
                }
                currentCoin = hitInfo.collider.gameObject.GetComponent<CoinBehaviour>();
                canInteract = true;
                currentCoin.Highlight();
            }
        }

        else if (currentCoin != null)
        {
            currentCoin.Unhighlight();
            currentCoin = null;
            canInteract = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            currentHealth -= 20;
            if (currentHealth < 0)
            {
                currentHealth = 0;
                //isDead = true;
                //Debug.Log("Player has died.");
            }
            else
            {
                //Debug.Log("-1 health. Current health: " + currentHealth);
            }
        }
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
        Debug.Log("Score: " + score);
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
            currentDoor = other.gameObject.GetComponent<DoorBehaviour>();
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

    void OnFire()
    {
        GameObject newProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        Vector3 fireForce = spawnPoint.forward * fireStrength;
        newProjectile.GetComponent<Rigidbody>().AddForce(fireForce);
    }
}
