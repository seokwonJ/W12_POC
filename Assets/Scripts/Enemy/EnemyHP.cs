using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int enemyHP;

    public void TakeDamage(int hp)
    {
        enemyHP -= hp;
        Debug.Log("���� ���ظ� ����: " + hp);

        if (enemyHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("���� ����");

        Destroy(gameObject);
    }
}
