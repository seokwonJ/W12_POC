using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PirateAttack : ProjectileBase
{
    public Rigidbody2D rb;
    public GameObject explosionEffect;
    private bool _isNoMoreExplosionAttackDamageUp;
    private float _noMoreExplosionAttackDamageUpPercent;

    public float acceleration = 5f;
    private float currentSpeed = 0f;
    private bool isAttack;

    public Transform target;
    public float homingStopDistance = 2f;
    private bool isHoming = false;

    public float spiralDuration = 0.3f;
    private bool isSpiraling = false;

    private bool _isAttackPerMana;
    private Pirate _characterPirate;
    private bool _isSkill;
    private bool _isCritical;

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

    public void SetInit(Vector2 dir, float damageNum, float speedNum, float scaleNum, float knockbackPowerNum, bool isCritical, bool isNoMoreExplosionAttackDamageUp,float noMoreExplosionAttackDamageUpPercent, bool isSkill, bool isAttackPerMana, Pirate pirate, Transform homingTarget = null)
    {
        rb = GetComponent<Rigidbody2D>();

        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = damageNum;
        transform.localScale = Vector3.one * scaleNum;
        speed = speedNum;
        knockbackPower = knockbackPowerNum;
        _isCritical = isCritical;
        _noMoreExplosionAttackDamageUpPercent = noMoreExplosionAttackDamageUpPercent;

        this._isNoMoreExplosionAttackDamageUp = isNoMoreExplosionAttackDamageUp;

        this._isAttackPerMana = isAttackPerMana;
        _isSkill = isSkill;
        _characterPirate = pirate;

        if (homingTarget != null)
        {
            isHoming = true;
            target = homingTarget;
        }
        if (_isSkill) StartCoroutine(SpiralThenHoming());
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
        Destroy(projectile);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            SoundManager.Instance.PlaySFX("PirateAttackExplosion");

            EnemyHP enemyHp = other.GetComponent<EnemyHP>();
            if (enemyHp != null && _isNoMoreExplosionAttackDamageUp)
            {
                enemyHp.TakeDamage((int)(damage * _noMoreExplosionAttackDamageUpPercent/ 100) , ECharacterType.Pirate);

                if (_isCritical)
                {
                    Vector2 contactPoint = other.ClosestPoint(transform.position);
                    Instantiate(Managers.PlayerControl.NowPlayer.GetComponent<PlayerEffects>().criticalEffect, contactPoint, Quaternion.identity);
                }
                Destroy(gameObject);
            }
            else
            {
                if (!_isSkill && _isAttackPerMana) _characterPirate.AttackMana();

                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, transform.localScale.magnitude);
                foreach (var hit in hits)
                {
                    if (hit.CompareTag("Enemy"))
                    {
                        Enemy enemy = hit.GetComponent<Enemy>();
                        EnemyHP otherEnemyHP = hit.GetComponent<EnemyHP>();

                        otherEnemyHP.TakeDamage((int)damage, ECharacterType.Pirate);


                        Vector3 knockbackDirection = hit.transform.position - transform.position;
                        if (enemy != null && otherEnemyHP.enemyHP > 0)
                        {
                            enemy.ApplyKnockback(knockbackDirection, knockbackPower);
                        }

                        if (_isCritical)
                        {
                            Vector2 contactPoint = other.ClosestPoint(other.transform.position);
                            Instantiate(Managers.PlayerControl.NowPlayer.GetComponent<PlayerEffects>().criticalEffect, contactPoint, Quaternion.identity);
                        }
                    }
                }
                DestroyProjectile(gameObject);
            }
        }
    }
}
