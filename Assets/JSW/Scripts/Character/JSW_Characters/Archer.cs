using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Character
{
    [Header("스킬")]
    public GameObject skillProjectile;
    public int skillCount = 3;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public int skillProjectileCount = 10;
    public Dictionary<GameObject, int> hitEnemies;

    [Header("강화")]
    public int upgradeNum;
    public bool isUpgradeTwoShot;
    public bool isUpgradeDieInstantly;
    public float dieInstantlyProbability;
    public bool isUpgradeSameEnemyDamage;
    public GameObject attackedEnemy;
    public int sameEnemyCount = 0;
    public int sameEnemyCountLimit = 20;
    public float sameEnemyDamageDuration = 5;
    public float nowSameEnemyDamageDuration;

    [Header("이펙트")]
    public GameObject skillActiveEffect;

    protected override void Update()
    {
        base.Update();

        if (isUpgradeSameEnemyDamage)
        {
            if (nowSameEnemyDamageDuration < sameEnemyDamageDuration)
            {
                nowSameEnemyDamageDuration += Time.deltaTime;
            }
            else
            {
                attackedEnemy = null;
                sameEnemyCount = 0;
            }
        }
    }

    // 일반 공격 : 화살 발사 
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        if (targetPos == null) return;


        Vector2 direction = (targetPos - firePoint.position).normalized;

        // 방향에 따라 캐릭터 스프라이트 좌우 반전
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float totalAttackDamage = TotalAttackDamage();
        bool isCritical = IsCriticalHit();
        if (isCritical) totalAttackDamage *= ((criticalDamage * criticalDamageUpNum / 100) / 100);

        bool instantlyDie = false;
        if (isUpgradeDieInstantly && Random.value < dieInstantlyProbability / 100) instantlyDie = true;
        else instantlyDie = false;

        proj.GetComponent<Arrow>().SetInit(direction, totalAttackDamage, projectileSpeed * (projectileSpeedUpNum / 100), projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), isCritical, this, false, instantlyDie, isUpgradeSameEnemyDamage);

        if (isUpgradeTwoShot)
        {
            if (isUpgradeDieInstantly && Random.value < dieInstantlyProbability / 100) instantlyDie = true;
            else instantlyDie = false;

            GameObject proj2 = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            proj2.GetComponent<Arrow>().SetInit(Quaternion.Euler(0, 0, 5) * direction, totalAttackDamage, projectileSpeed * (projectileSpeedUpNum / 100), projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), isCritical, this, false, instantlyDie, isUpgradeSameEnemyDamage);
        }

        SoundManager.Instance.PlaySFX("ArcherAttack");

        Destroy(proj, 3);
    }

    // 스킬 : 사방에 화살 여러번 발사
    protected override IEnumerator FireSkill()
    {
        for (int i = 0; i < skillCount; i++)
        {
            yield return new WaitForSeconds(skillFireDelay);
            FireSkillProjectiles(i);
            animator.Play("SKILL", -1, 0f);
            SoundManager.Instance.PlaySFX("ArcherSkill");
            Instantiate(skillActiveEffect, transform.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(skillInterval / skillCount);
        }
    }

    // 궁극기 연사 오버라이드 (필요 시)
    protected void FireSkillProjectiles(int skillCount)
    {
        float angleStep = 360f / skillProjectileCount;
        Vector3 startPos = transform.position;

        float totalSkillDamage = TotalSkillDamage();

        // 스킬 데미지 받은 것들
        hitEnemies = new Dictionary<GameObject, int>();

        for (int i = 0; i < skillProjectileCount; i++)
        {
            float angle = i * angleStep + 10 * skillCount;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject proj = Instantiate(skillProjectile, startPos, rotation);
            proj.GetComponent<Arrow>().SetInit(rotation * Vector2.right, totalSkillDamage, projectileSpeed * (projectileSpeedUpNum / 100), projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100),false, this, true, false, false);
        }
    }

    public float SameEnemyTotalDamage(GameObject enemy)
    {
        nowSameEnemyDamageDuration = 0;
        float sameEnemyStack = 1;
        if (attackedEnemy == enemy)
        {
            if (sameEnemyCount < sameEnemyCountLimit) sameEnemyCount += 1;
        }
        else
        {
            attackedEnemy = enemy;
            sameEnemyCount = 0;
        }
        return (sameEnemyStack + sameEnemyCount * 0.1f);
    }
}