using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PirateAttack : ProjectileBase
{
    public Rigidbody2D rb;
    public float knockbackPower;
    public GameObject explosionEffect;
    public bool isFirstHitDealsBonusDamage;
    public float acceleration = 5f;
    private float currentSpeed = 0f;
    private bool isAttack;

    public Transform target;
    public float homingStopDistance = 2f;
    private bool isHoming = false;

    public float spiralDuration = 0.3f;
    private bool isSpiraling = false;

    protected override void Update()
    {
        if (isAttack) return;

        
        if (isSpiraling)
        {
            // 속도 가속
            currentSpeed = Mathf.Min(currentSpeed + acceleration / 2 * Time.deltaTime, speed);

            // 그냥 현재 회전 방향(transform.right) 기준으로 이동
            transform.Rotate(0, 0, 360 * Time.deltaTime); // 스핀 효과
            transform.Translate(Vector3.right * currentSpeed * Time.deltaTime, Space.Self);
        }
        else if (isHoming && target != null)
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, speed);

            // 부드럽게 목표 방향으로 회전
            Vector2 toTarget = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);

            // 회전 방향(transform.right) 기준으로 이동
            transform.Translate(Vector3.right * currentSpeed * Time.deltaTime, Space.Self);
        }
        else
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration / 2 * Time.deltaTime, speed);

            // 회전 끝난 후에도 전진 유지
            transform.Translate(Vector3.right * currentSpeed * Time.deltaTime, Space.Self);
        }
    }

    public void SetInit(Vector2 dir, int damageNum, float speedNum, float scaleNum, bool isFirstHitDealsBonus, bool isSkill, Transform homingTarget = null)
    {
        rb = GetComponent<Rigidbody2D>();

        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = damageNum;
        transform.localScale = Vector3.one * scaleNum;
        speed = speedNum;
        isFirstHitDealsBonusDamage = isFirstHitDealsBonus;

        if (homingTarget != null)
        {
            isHoming = true;
            target = homingTarget;
        }
        if (isSkill) StartCoroutine(SpiralThenHoming());
    }
   
    private IEnumerator SpiralThenHoming()
    {
        isSpiraling = true;

        // 방향 살짝 랜덤 회전
        float randomAngle = Random.Range(-180f, 180f);
        transform.rotation = Quaternion.Euler(0, 0, randomAngle);

        yield return new WaitForSeconds(spiralDuration);

        currentSpeed /= 3;
        acceleration += 20;

        isSpiraling = false;
        isHoming = true;
    }

    public override void DestroyProjectile(GameObject projectile)
    {
        isAttack = true;
        Instantiate(explosionEffect,transform.position, Quaternion.identity);
        rb.linearVelocity = Vector3.zero;
        Destroy(projectile, 0.1f);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            SoundManager.Instance.PlaySFX("PirateAttackExplosion");

            EnemyHP enemyHp = other.GetComponent<EnemyHP>();
            if (enemyHp != null && isFirstHitDealsBonusDamage)
            {
                enemyHp.TakeDamage(damage, ECharacterType.Pirate);
            }

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, transform.localScale.magnitude);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    Enemy enemy = hit.GetComponent<Enemy>();
                    EnemyHP otherEnemyHP = hit.GetComponent<EnemyHP>();

                    otherEnemyHP.TakeDamage(damage,ECharacterType.Pirate);

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
}
