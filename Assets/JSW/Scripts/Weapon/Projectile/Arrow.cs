using UnityEngine;

public class Arrow : ProjectileBase
{
    private float knockbackPower = 1;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHP enemyHp = other.GetComponent<EnemyHP>();
            if (enemyHp != null)
                enemyHp.TakeDamage(damage);

            Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ApplyKnockback(knockbackDirection, knockbackPower);
            }


            Destroy(gameObject);
        }
    }

    public void SetInit(Vector2 dir, int damageNum, float speedNum, float knockbackPowerNum, float scaleNum)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        damage = damageNum;
        speed = speedNum;
        knockbackPower = knockbackPowerNum;
        transform.localScale = Vector3.one * scaleNum;
    }

}
