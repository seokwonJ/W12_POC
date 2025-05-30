using UnityEngine;

public class TankerAttack : ProjectileBase
{
    private float knockbackPower;


    protected override void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        transform.up = Vector2.right;
    }

    public void SetInit(Vector2 dir, int damageNum, float speedNum, float lifetimeNum, float scaleNum, float KnockBackPowerNum)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = damageNum;
        speed = speedNum;
        lifetime = lifetimeNum;
        transform.localScale = Vector3.one * scaleNum;
        knockbackPower = KnockBackPowerNum;
    }


    public override void DestroyProjectile(GameObject projectile)
    {
        Destroy(projectile);
    }

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
                enemy2.ApplyKnockback(knockbackDirection, knockbackPower);
            }

            else
            {
                EnemyAI enemyAI = other.GetComponent<EnemyAI>();
                if (enemyAI != null)
                    enemyAI.ApplyKnockback(knockbackDirection, knockbackPower);
            }

            Destroy(gameObject);
        }
    }
}
