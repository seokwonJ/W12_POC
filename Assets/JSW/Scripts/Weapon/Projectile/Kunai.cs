using UnityEngine;

public class Kunai : ProjectileBase
{
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

            DestroyProjectile(gameObject);
        }
    }

    public void SetInit(Vector2 dir, float damageNum, float speedNum, float scaleNum, float knockbackPowerNum)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        damage = damageNum;
        speed = speedNum;
        transform.localScale = Vector3.one * scaleNum;
        knockbackPower = knockbackPowerNum;
    }

}
