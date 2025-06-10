using UnityEngine;

public class ASG1_BoxDrop : MonoBehaviour
{
    [SerializeField]
    GameObject boxDrop;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            Instantiate(boxDrop, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
