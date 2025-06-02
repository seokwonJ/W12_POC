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
            EnemyHP enemyHp = other.GetComponent<EnemyHP>();
            if (enemyHp != null && isFirstHitDealsBonusDamage)
            {
                enemyHp.TakeDamage(damage);
            }

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);


            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    Enemy enemy = hit.GetComponent<Enemy>();
                    EnemyHP otherEnemyHP = hit.GetComponent<EnemyHP>();

                    int totalDamage = damage;

                    otherEnemyHP.TakeDamage(totalDamage);

                    Vector3 knockbackDirection = hit.transform.position - transform.position;
                    if (enemy != null)
                    {
                        enemy.ApplyKnockback(knockbackDirection, knockbackPower);
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
