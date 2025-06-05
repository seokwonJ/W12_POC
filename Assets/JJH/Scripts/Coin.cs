using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Managers.Status.Gold += value;
            // to do 코인 획득 이펙트
            Debug.Log($"Coin collected! Value: {value}, Total Gold: {Managers.Status.Gold}");
            Destroy(gameObject);
        }
    }
}
