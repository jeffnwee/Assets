using UnityEngine;

public class GiftBox : MonoBehaviour
{
    [SerializeField]
    GameObject giftDrop;

    [SerializeField]
    int giftCount = 0;

    [SerializeField]
    float spawnRadius = 0.5f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

            Quaternion coinRotation = Quaternion.Euler(90f, 0f, 0f);

            for (int i = 0; i < giftCount; i++)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-spawnRadius, spawnRadius),
                    0f,
                    Random.Range(-spawnRadius, spawnRadius)
                );

                Vector3 spawnPosition = transform.position + randomOffset;

                Instantiate(giftDrop, spawnPosition, coinRotation);
            }
        }
    }
}
