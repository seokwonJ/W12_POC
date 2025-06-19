using UnityEngine;

public class Kunai : ProjectileBase
{
    private bool _isCritical;

    protected override void Update()
    {
        base.Update(); // 이동
        transform.right = direction;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyHP>();
            if (enemy != null)
                enemy.TakeDamage((int)damage, ECharacterType.Ninja);

            // 넉백
            Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

            Enemy enemyComponenet = enemy.GetComponent<Enemy>();
            if (enemyComponenet != null)
            {
                enemyComponenet.ApplyKnockback(knockbackDirection, knockbackPower);
            }

            if (_isCritical)
            {
                Vector2 contactPoint = other.ClosestPoint(transform.position);
                Instantiate(Managers.PlayerControl.NowPlayer.GetComponent<PlayerEffects>().criticalEffect, contactPoint, Quaternion.identity);
            }


            DestroyProjectile(gameObject);
        }
    }

    public void SetInit(Vector2 dir, float damageNum, float speedNum, float scaleNum, float knockbackPowerNum, bool isCritical)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        damage = damageNum;
        speed = speedNum;
        transform.localScale = Vector3.one * scaleNum;
        knockbackPower = knockbackPowerNum;
        _isCritical = isCritical;
    }

}
