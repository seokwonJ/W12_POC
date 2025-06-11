using UnityEngine;

public class PirateAttack : ProjectileBase
{
    public Rigidbody2D rb;
    public float knockbackPower;
    public GameObject explosionEffect;
    public bool isFirstHitDealsBonusDamage;
    public float acceleration = 5f;
    private float currentSpeed = 0f;
    private bool isAttack;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (isAttack) { return; }
        // 가속도에 따라 현재 속도 증가
        currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, speed);

        // 현재 속도로 이동
        transform.Translate(direction.normalized * currentSpeed * Time.deltaTime, Space.World);

        //transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void SetInit(Vector2 dir, int damageNum, float speedNum, float scaleNum, bool isFirstHitDealsBonus)
    {
        rb = GetComponent<Rigidbody2D>();

        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = damageNum;
        transform.localScale = Vector3.one * scaleNum;
        //rb.linearVelocity = direction * speedNum;
        speed = speedNum;
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

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, transform.localScale.magnitude);
            print(transform.localScale.magnitude);

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    Enemy enemy = hit.GetComponent<Enemy>();
                    EnemyHP otherEnemyHP = hit.GetComponent<EnemyHP>();

                    int totalDamage = damage;

                    otherEnemyHP.TakeDamage(totalDamage);

                    Vector3 knockbackDirection = hit.transform.position - transform.position;
                    if (enemy != null && otherEnemyHP.enemyHP > 0)
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
        isAttack = true;
        explosionEffect.SetActive(true);
        rb.linearVelocity = Vector3.zero;
        Destroy(projectile,0.1f);
    }
}
