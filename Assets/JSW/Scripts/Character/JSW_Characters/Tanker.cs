using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class Tanker : Character
{
    [Header("스킬")]
    public bool isSkillLanding;
    public int skillDamage;
    public float skillknockbackPower;
    public float skillInterval = 0.3f;
    public float skillRange;
    public GameObject landingSkillEffect;

    [Header("공격")]
    public float nomalAttackSize;
    public float nomalAttackLifetime;
    public float knockBackpower = 1;

    [Header("강화")]
    public bool isFallingSpeedToSkillDamage;
    public bool isShieldFlyer;
    public bool isHitSkillPerGetMana;
    public bool isCloserMoreDamage;

    public int upgradeNum;

    private PlayerStatus _playerStatus;

    protected override void Start()
    {
        base.Start();
        _playerStatus = FindAnyObjectByType<PlayerStatus>();
    }

    // 일반 공격: 직진형 투사체 발사
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;
        SoundManager.Instance.PlaySFX("TankerAttack");
        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<TankerAttack>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackLifetime, nomalAttackSize, knockBackpower); // 이 메서드가 없다면 그냥 방향 저장해서 쓰면 됨
    }

    // 스킬: 커다란 직진형 투사체 3발 연속 발사
    protected override IEnumerator FireSkill()
    {
        if (isShieldFlyer) _playerStatus.defensePower -= 5;
        isSkillLanding = true;

        yield return new WaitForSeconds(skillInterval);
    }

    // 착지했을 경우
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision); // 부모 로직 먼저 실행

        if (!isGround) return;

        if (isShieldFlyer) _playerStatus.defensePower += 5;

        if (isSkillLanding)
        {
            isSkillLanding = false;
            SoundManager.Instance.PlaySFX("TankerLandingSkillEffect");
            LandingSkill(skillDamage);
        }
    }

    // 착지했을 경우 주위의 투사체 사라지고 적들은 넉백
    void LandingSkill(int skillDamageNum)
    {
        GameObject landingSkillEffectObject = Instantiate(landingSkillEffect, transform.position, Quaternion.identity);

        Debug.Log("랜딩 스킬");

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, skillRange);

        int hitEnemyCount = 0;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hitEnemyCount += 1;

                Enemy enemy = hit.GetComponent<Enemy>();
                EnemyHP enemyHP = hit.GetComponent<EnemyHP>();

                int totalDamage = skillDamageNum;
                if (isCloserMoreDamage) totalDamage += (int)(skillDamage / Vector2.Distance(hit.transform.position, transform.position));

                if (isFallingSpeedToSkillDamage) {enemyHP.TakeDamage(totalDamage + (int)rb.linearVelocity.magnitude);}
                else enemyHP.TakeDamage(totalDamage);

                if (enemyHP != null && enemyHP.enemyHP <= 0)  continue;

                Vector3 knockbackDirection = hit.transform.position - transform.position;

                if (enemy != null)
                {
                    enemy.ApplyKnockback(knockbackDirection.normalized, skillknockbackPower);
                }
            }

            //if (hit.CompareTag("EnemyAttack"))
            //{
            //    Destroy(hit.gameObject);
            //}
        }

        if (isHitSkillPerGetMana) currentMP += 2 * hitEnemyCount;

        landingSkillEffectObject.transform.localScale = Vector3.one * skillRange;
    }
}
