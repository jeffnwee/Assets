using UnityEngine;

public class ASG1_BoxDrop : MonoBehaviour
{
    [SerializeField]
    GameObject boxDrop;

    void OnCollisionEnter(Collision collision)
    {   
        // Check if the collision is with a projectile
        // and if so, destroy the projectile, instantiate a box drop, and destroy this object
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            Instantiate(boxDrop, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
