using UnityEngine;

public class ProtecterSkill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyAttack")
        {
            Destroy(collision.gameObject);
        }
    }
}
