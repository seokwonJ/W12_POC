using UnityEngine;

public class TankerAttack : ProjectileBase
{
    private float knockbackPower;

    //protected override void Update()
    //{
    //    transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
    //    transform.up = Vector2.right;
    //}

    public void SetInit(Vector2 dir, float damageNum, float speedNum, float lifetimeNum, float scaleNum, float KnockBackPowerNum)
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
        }
    }
}
