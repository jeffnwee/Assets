using UnityEngine;

public class ASG1_BoxDrop : MonoBehaviour
{   
    /*
    * Author: Jeffrey Ang
    * Date: 10 June 2025
    * Description: This script handles the box drop object in the game.
    * It destroys the projectile that hits it, instantiates a box drop at its position,
    * and then destroys itself.
    */

    [SerializeField]
    GameObject boxDrop;
    // This is the wrench prefab that will be instantiated when this object is hit by a projectile

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
