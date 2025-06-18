using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Character
{
    [Header("여우 공격")]
    public float attackDuration = 0.7f;

    [Header("스킬")]
    public GameObject skillProjectile;
    public int skillCount = 10;
    public float skillFireDelay = 0.1f;
    public float skillSize = 1f;
    public float skillDamage = 1f;
    public Dictionary<GameObject, int> hitEnemies;

    [Header("강화")]
    public bool isUpgradeTenAttackSkillDamageUp;
    public int tenAttackSkillDamageUpCount;
    public float tenAttackSkillDamageUpPercent;

    [Header("이펙트")]
    public GameObject skillActiveEffect;

    public int upgradeNum;

    // 일반 공격: 원형 관통하고 돌아오는 원형 정수 발사
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        // 방향에 따라 캐릭터 스프라이트 좌우 반전
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float totalAttackDamage = TotalAttackDamage();

        if (isUpgradeTenAttackSkillDamageUp)
        {
            tenAttackSkillDamageUpCount += 1;
            if (tenAttackSkillDamageUpCount == 10)
            {
                proj.GetComponent<FoxAttack>().SetInit(direction, totalAttackDamage * tenAttackSkillDamageUpPercent / 100, projectileSpeed * (projectileSpeedUpNum / 100), projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), transform, attackDuration, this, false);
                tenAttackSkillDamageUpCount = 0;
            }
            else
            {
                proj.GetComponent<FoxAttack>().SetInit(direction, totalAttackDamage, projectileSpeed * (projectileSpeedUpNum / 100), projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), transform, attackDuration, this, false);
            }
        }
        else
        {
            proj.GetComponent<FoxAttack>().SetInit(direction, totalAttackDamage, projectileSpeed * (projectileSpeedUpNum / 100), projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), transform, attackDuration, this, false);
        }

        SoundManager.Instance.PlaySFX("FoxAttack");
    }

    // 스킬: 점프 후 원형 정수를 360도 사방으로 발사
    protected override IEnumerator FireSkill()
    {
        animator.Play("FOXSKILLREADY", -1, 0f);
        yield return new WaitForSeconds(skillFireDelay);
        animator.Play("SKILL", -1, 0f);
        yield return new WaitForSeconds(0.08f);

        FireSkillProjectiles();
        Instantiate(skillActiveEffect, transform.position, Quaternion.identity);

        SoundManager.Instance.PlaySFX("FoxSkill");
    }

    // 궁극기 발사 구현
    protected override void FireSkillProjectiles()
    {
        float angleStep = 360f / skillCount;

        float totalSkillDamage = TotalSkillDamage();

        // 스킬 데미지 받은 것들
        hitEnemies = new Dictionary<GameObject, int>();

        for (int i = 0; i < skillCount; i++)
        {
            float angle = i * angleStep;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;

            GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            FoxAttack mb = proj.GetComponent<FoxAttack>();
            mb.SetInit(dir.normalized, totalSkillDamage, projectileSpeed * (projectileSpeedUpNum / 100), projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), transform, attackDuration, this, true);
            mb.speed = 5;
        }
    }
}