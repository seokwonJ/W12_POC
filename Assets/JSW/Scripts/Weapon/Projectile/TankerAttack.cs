using UnityEngine;

public class TankerAttack : ProjectileBase
{
    private bool _isCritical;
    public void SetInit(Vector2 dir, float damageNum, float speedNum, float scaleNum, float KnockBackPowerNum, bool isCritical ,float lifetimeNum)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = damageNum;
        speed = speedNum;
        lifetime = lifetimeNum;
        transform.localScale = Vector3.one * scaleNum;
        knockbackPower = KnockBackPowerNum;
        _isCritical = isCritical;
    }

    public override void DestroyProjectile(GameObject projectile)
    {
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemyHP = other.GetComponent<EnemyHP>();

            if (enemyHP != null) enemyHP.TakeDamage((int)damage, ECharacterType.Tanker);
            if (enemyHP != null && enemyHP.enemyHP <= 0) return;

            Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ApplyKnockback(knockbackDirection, knockbackPower);
            }

            if (_isCritical)
            {
                Vector2 contactPoint = other.ClosestPoint(transform.position);
                Instantiate(Managers.PlayerControl.NowPlayer.GetComponent<PlayerEffects>().criticalEffect, contactPoint, Quaternion.identity);
            }
        }
    }
}
