using UnityEngine;

public class PirateAttack : ProjectileBase
{
    public Rigidbody2D rb;
    public float range;
    public float knockbackPower;
    public GameObject explosionEffect;
    public bool isFirstHitDealsBonusDamage;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update() { }
    

    public void SetInit(Vector2 dir, int damageNum, float speedNum, float scaleNum, bool isFirstHitDealsBonus)
    {
        rb = GetComponent<Rigidbody2D>();

        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = damageNum;
        transform.localScale = Vector3.one * scaleNum;
        rb.linearVelocity = direction * speedNum;
        isFirstHitDealsBonusDamage = isFirstHitDealsBonus;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyHP>();
            if (enemy != null && isFirstHitDealsBonusDamage)
            {
                enemy.TakeDamage(damage);
            }

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);


            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    EnemyAI enemyAI = hit.GetComponent<EnemyAI>();
                    EnemyHP enemyHP = hit.GetComponent<EnemyHP>();

                    int totalDamage = damage;

                    enemyHP.TakeDamage(totalDamage);

                    Vector3 knockbackDirection = hit.transform.position - transform.position;
                    if (enemyAI != null) enemyAI.ApplyKnockback(knockbackDirection, knockbackPower);
                    else
                    {
                        Enemy2 enemy2 = hit.GetComponent<Enemy2>();
                        enemy2.ApplyKnockback(knockbackDirection, knockbackPower);
                    }
                }
            }
            DestroyProjectile(gameObject);
         }
    }

    public override void DestroyProjectile(GameObject projectile)
    {
        explosionEffect.SetActive(true);
        rb.linearVelocity = Vector3.zero;
        Destroy(projectile,0.1f);
    }
}
