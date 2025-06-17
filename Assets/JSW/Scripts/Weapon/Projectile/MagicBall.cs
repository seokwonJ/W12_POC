using UnityEngine;

public class MagicBall : ProjectileBase
{
    public void SetInit(Vector2 dir, float damageNum, float speedNum, float scaleNum, float knockbackPowerNum)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = damageNum;
        speed = speedNum;
        transform.localScale = Vector3.one * scaleNum;
        knockbackPower = knockbackPowerNum;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyHP>();
            if (enemy != null)
                enemy.TakeDamage((int)damage, ECharacterType.Magician);

            // ³Ë¹é
            Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

            Enemy enemyComponenet = enemy.GetComponent<Enemy>();
            if (enemyComponenet != null)
            {
                enemyComponenet.ApplyKnockback(knockbackDirection, knockbackPower);
            }

            DestroyProjectile(gameObject);
        }
    }

    public override void DestroyProjectile(GameObject projectile)
    {
    }
}
