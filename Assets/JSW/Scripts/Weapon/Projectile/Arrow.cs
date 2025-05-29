using UnityEngine;

public class Arrow : ProjectileBase
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyHP>();
            if (enemy != null)
                enemy.TakeDamage(damage);

            Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

            Enemy2 enemy2 = other.GetComponent<Enemy2>();
            if (enemy2 != null)
            {
                enemy2.ApplyKnockback(knockbackDirection, 1);
            }
            else
            {
                EnemyAI enemyAI = other.GetComponent<EnemyAI>();
                if (enemyAI != null)
                    enemyAI.ApplyKnockback(knockbackDirection, 1);
            }

            Destroy(gameObject);
        }
    }
}
