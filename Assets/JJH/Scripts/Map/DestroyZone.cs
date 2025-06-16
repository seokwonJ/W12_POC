using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Boss boss = collision.GetComponent<Boss>();
            if (boss != null)
            {
                return; // 보스는 파괴되지 않도록 함
            }
            collision.GetComponent<EnemyHP>().Die(); // 적이 파괴되는 함수 호출
        }
        else if (collision.gameObject.CompareTag("EnemyProjectile") || collision.gameObject.CompareTag("PlayerProjectile"))
        {
            Destroy(collision.gameObject); // 적이나 플레이어의 투사체가 파괴되는 함수 호출
        }
    }
}
