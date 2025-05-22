using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    int maxHealth = 100;
    int currentHealth = 100;
    bool isDead = false;

    bool canInteract = false;
    public int score = 0;

    CoinBehaviour currentCoin;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {  
            currentHealth -= 20;
            if (currentHealth < 0)
            {
                currentHealth = 0;
                isDead = true;
                Debug.Log("Player has died.");
            }
            else
            {
                Debug.Log("-1 health. Current health: " + currentHealth);
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
                Debug.Log("+1 health. Current health: " + currentHealth);
            }
        }
    }

    void OnInteract()
    {
        if(canInteract)
        {
            //Debug.Log("Interacting with coin.");
            currentCoin.Collect(this);
            currentCoin = null;
            canInteract = false;
        }
        else
        {
            //Debug.Log("No coin to interact with.");
        }
    }

    public void ModifyScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Collectible"))
        {
            //Debug.Log("Player is looking at " + other.gameObject.name);
            currentCoin = other.gameObject.GetComponent<CoinBehaviour>();
            canInteract = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Collectible"))
        {
            //Debug.Log("Player is no longer looking at " + other.gameObject.name);
            currentCoin = null;
            canInteract = false;
        }
    }
}
