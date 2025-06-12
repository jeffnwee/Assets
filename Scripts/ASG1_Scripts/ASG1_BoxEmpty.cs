using UnityEngine;

public class ASG1_BoxEmpty : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a projectile
        // and if so, destroy the projectile and this object
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
