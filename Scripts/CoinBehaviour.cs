using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
[SerializeField]
    public int coinValue = 1;

    public void Collect(PlayerBehaviour player)
    {
        player.ModifyScore(coinValue);
        Destroy(gameObject);
    }
}
