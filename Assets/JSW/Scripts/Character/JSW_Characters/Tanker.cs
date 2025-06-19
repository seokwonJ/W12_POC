using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class Tanker : Character
{
    [Header("스킬")]
    public bool isSkillLanding;

    public float skillknockbackPower;
    public float skillInterval = 0.3f;
    public float skillRange;
    public GameObject landingSkillEffect;

    [Header("공격")]
    public float nomalAttackSize;
    public float nomalAttackLifetime;
    public float knockBackpower = 1;

    [Header("강화")]
    public bool isUpgradeFallingSpeedToSkillDamage;
    public bool isUpgradeRidingDefenseUp;
    public bool isCloserMoreDamage;
    public float closerMoreDamagePercent = 20;
    public bool isSkillEnemySpeedDown;
    public float skillEnemySpeedDonwPercent = 70;
    public float skillEnemySpeedDonwDuration = 3;

    public int upgradeNum;

    private PlayerStatus _playerStatus;

    protected override void Start()
    {
        base.Start();
        _playerStatus = FindAnyObjectByType<PlayerStatus>();
    }

    protected override void Update()
    {
        if (!isGround && !isSkillActive)
        {
            if (!isSkillLanding)
            {
                Vector3 direction = playerTransform.position - transform.position;
                if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            animator.SetBool("5_Fall", true);
        }
        if (!isGround) return;

        currentMP += Time.deltaTime * (mpPerSecond * (manaRegenSpeedUpNum / 100));
        currentMP = Mathf.Min(currentMP, maxMP);

        if (currentMP >= maxMP && !isSkillActive)
        {
            StartCoroutine(ActiveSkill());
            isSkillActive = true;
        }
    }


    // 일반 공격: 직진형 투사체 발사
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        // 방향에 따라 캐릭터 스프라이트 좌우 반전
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);


        SoundManager.Instance.PlaySFX("TankerAttack");
        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float totalAttackDamage = TotalAttackDamage();
        bool isCritical = IsCriticalHit();
        if (isCritical) totalAttackDamage *= ((criticalDamage * criticalDamageUpNum / 100) / 100);

        proj.GetComponent<TankerAttack>().SetInit(direction, totalAttackDamage, projectileSpeed * (projectileSpeedUpNum / 100), projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), isCritical, nomalAttackLifetime); // 이 메서드가 없다면 그냥 방향 저장해서 쓰면 됨
    }

    // 스킬: 착지시 밀어냄 
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(skillInterval);

        if (isUpgradeRidingDefenseUp) _playerStatus.defensePower -= 5;
        isSkillLanding = true;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    // 착지했을 경우
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision); // 부모 로직 먼저 실행

        if (!isGround) return;

        if (isUpgradeRidingDefenseUp) _playerStatus.defensePower += 5;

        if (isSkillLanding)
        {
            isSkillLanding = false;
            SoundManager.Instance.PlaySFX("TankerLandingSkillEffect");
            LandingSkill();
        }
    }

    // 착지했을 경우 주위의 투사체 사라지고 적들은 넉백
    void LandingSkill()
    {
        GameObject landingSkillEffectObject = Instantiate(landingSkillEffect, transform.position, Quaternion.identity);

        Debug.Log("랜딩 스킬");

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, skillRange);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {

                Enemy enemy = hit.GetComponent<Enemy>();
                EnemyHP enemyHP = hit.GetComponent<EnemyHP>();

                float totalSkillDamage = TotalSkillDamage();

                if (isCloserMoreDamage) totalSkillDamage += (int)(closerMoreDamagePercent / Vector2.Distance(hit.transform.position, transform.position));

                if (isUpgradeFallingSpeedToSkillDamage) { enemyHP.TakeDamage((int)(totalSkillDamage + rb.linearVelocity.magnitude), ECharacterType.Tanker); }
                else enemyHP.TakeDamage((int)totalSkillDamage, ECharacterType.Tanker);

                if (enemyHP != null && enemyHP.enemyHP <= 0) continue;

                Vector3 knockbackDirection = hit.transform.position - transform.position;

                if (enemy != null)
                {
                    enemy.ApplyKnockback(knockbackDirection.normalized, skillknockbackPower / Vector2.Distance(hit.transform.position, transform.position));
                }

                if (isSkillEnemySpeedDown)
                {
                    enemy.GetComponent<Enemy>().ApplySlow((int)skillEnemySpeedDonwPercent, skillEnemySpeedDonwDuration);
                }
            }
        }

        landingSkillEffectObject.transform.localScale = Vector3.one * skillRange * 0.5f;
    }

    public override void EndFieldAct() // 필드전투가 종료될 때 실행
    {
        base.EndFieldAct();

        if (isSkillLanding == true)
        {
            if (isUpgradeRidingDefenseUp) _playerStatus.defensePower -= 5;
            Debug.Log("공격력 돌아옴");
            isSkillLanding = false;
        }
    }
}
