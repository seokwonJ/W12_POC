using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : Character
{
    [Header("스킬")]
    public GameObject skillProjectile;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillSpeed = 10f;
    public int skillShotCount = 8;

    [Header("강화")]
    public bool isAttackPerMana;
    public float attackPerManaPercent = 15;
    public bool isNoMoreExplosionAttackDamageUp;
    public bool isManaMultipleSkillProjectileMultiple;


    public int upgradeNum;

    public GameObject player;

    [Header("이펙트")]
    public GameObject skillActiveEffect;


    protected override void Start()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerMove>().gameObject;
    }

    protected override void Update()
    {
        if (!isGround && !isSkillActive)
        {
            Vector3 direction = playerTransform.position - transform.position;
            if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            animator.SetBool("5_Fall", true);
        }
        if (!isGround) return;

        currentMP += Time.deltaTime * (mpPerSecond * (manaRegenSpeedUpNum / 100));
        float currentMaxMp = maxMP;                                                                     // 해적만 2배를 위해 특별히 들어감

        if (isManaMultipleSkillProjectileMultiple) currentMaxMp *= 2;

        currentMP = Mathf.Min(currentMP, currentMaxMp);

        if (mpImage != null) mpImage.fillAmount = currentMP / currentMaxMp;

        if (currentMP >= currentMaxMp && !isSkillActive)
        {
            StartCoroutine(ActiveSkill());
            isSkillActive = true;
        }
    }

    // 일반 공격: 대포알 한발씩 발사 대포알 경우 광역 넉백
    protected override void FireNormalProjectile(Vector3 targetPos)
    {

        Vector2 direction = (targetPos + Vector3.up - firePoint.position).normalized;

        // 방향에 따라 캐릭터 스프라이트 좌우 반전
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float totalAttackDamage = TotalAttackDamage();

        Transform enemyTarget = FindNearestEnemy(); // 타겟 추적하는 메서드 필요
        proj.GetComponent<PirateAttack>().SetInit(direction, totalAttackDamage, projectileSpeed * (projectileSpeedUpNum / 100), projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), isNoMoreExplosionAttackDamageUp, false, isAttackPerMana, this, enemyTarget);

        SoundManager.Instance.PlaySFX("PirateAttack");

    }

    // 스킬: 점프 후 공중에 대포알들 여러발 발사
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(skillInterval);

        SoundManager.Instance.PlaySFX("PirateSkillActive");
        SoundManager.Instance.PlaySFX("PirateAttack");
        Instantiate(skillActiveEffect, transform.position + Vector3.up, Quaternion.identity);

        yield return StartCoroutine(FireSkillCanon());
    }

    // 궁극기 발사 구현
    IEnumerator FireSkillCanon()
    {
        List<Transform> originalTargets = GetFarthestEnemies(skillShotCount);
        List<Transform> activeTargets = new List<Transform>(originalTargets);

        animator.Play("SKILL", -1, 0f);

        int currentSkillShotCount = skillShotCount;                                 

        if (isManaMultipleSkillProjectileMultiple) currentSkillShotCount *= 2;                   // 해적만 2배를 위해 특별히 들어감

        for (int i = 0; i < skillShotCount; i++)
        {
            rb.linearVelocity = Vector3.zero;

            // 살아있는 타겟 중 하나 선택
            activeTargets.RemoveAll(t => t == null || !t.gameObject.activeInHierarchy);

            Transform target = null;
            if (activeTargets.Count > 0)
            {
                target = activeTargets[i % activeTargets.Count]; // 순차 순회
            }

            GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

            float totalSkillDamage = TotalSkillDamage();

            PirateAttack mb = proj.GetComponent<PirateAttack>();

            if (target != null)
            {
                Vector3 dir = target.position - proj.transform.position;
                mb.SetInit(dir.normalized, totalSkillDamage, skillSpeed, projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), isNoMoreExplosionAttackDamageUp, true,false,this, target);
            }
            else
            {
                // 유효한 타겟이 없으면 랜덤 방향 발사
                float randomAngle = Random.Range(0f, 360f);
                Vector2 randomDir = Quaternion.Euler(0, 0, randomAngle) * Vector2.right;
                mb.SetInit(randomDir.normalized, totalSkillDamage, skillSpeed, projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), isNoMoreExplosionAttackDamageUp, true, false, this, null);
            }

            
        yield return new WaitForSeconds(skillFireDelay);
        }
    }

    private List<Transform> GetFarthestEnemies(int count)
    {
        List<Transform> enemies = new List<Transform>();
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy.transform);
        }

        enemies.Sort((a, b) =>
            Vector2.Distance(firePoint.position, b.position)
            .CompareTo(Vector2.Distance(firePoint.position, a.position))
        );

        return enemies.GetRange(0, Mathf.Min(count, enemies.Count));
    }

    public void AttackMana()
    {
        currentMP += maxMP * attackPerManaPercent / 100;
    }
}
