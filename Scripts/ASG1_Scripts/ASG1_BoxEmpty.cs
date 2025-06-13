using UnityEngine;

public class ASG1_BoxEmpty : MonoBehaviour
{   
    /*
    * Author: Jeffrey Ang
    * Date: 10 June 2025
    * Description: This script handles the empty box object in the game.
    * It destroys the projectile that hits it and then destroys itself.
    */

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
