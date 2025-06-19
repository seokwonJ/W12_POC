using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Magician : Character
{
    [Header("스킬")]
    public GameObject skillProjectile;
    public int skillCount = 3;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillSize = 1f;
    public float skillProjectileSpeed = 15;

    [Header("강화")]
    public int upgradeNum;
    public bool isUpgradeTenAttackSkillAttack;
    public int tenAttackSkillAttackCountMax = 10;
    private int _nowTenAttackSkillAttackCount = 0;
    public bool isUpgradeSkillExplosionAttack;
    public float SkillExplosionAttackTime = 1.5f;

    public GameObject player;

    [Header("이펙트")]
    public GameObject skillActiveEffect;

    protected override void Start()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerMove>().gameObject;
    }

    // 일반 공격: 가까운 적에게 관통 공격
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        // 방향에 따라 캐릭터 스프라이트 좌우 반전
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float totalAttackDamage = TotalAttackDamage();
        bool isCritical = IsCriticalHit();
        if (isCritical) totalAttackDamage *= ((criticalDamage * criticalDamageUpNum / 100) / 100);

        if (_nowTenAttackSkillAttackCount >= tenAttackSkillAttackCountMax)
        {
            float totalSkillDamage = TotalSkillDamage();
            proj.GetComponent<MagicBall>().SetInit(direction.normalized, totalSkillDamage, projectileSpeed * (projectileSpeedUpNum / 100), skillSize, knockbackPower * (knockbackPowerUpNum / 100), false, false, 0);
            _nowTenAttackSkillAttackCount = 0;
        }
        else
        {
            if (isUpgradeTenAttackSkillAttack)
            {
                _nowTenAttackSkillAttackCount += 1;
            }
            proj.GetComponent<MagicBall>().SetInit(direction, totalAttackDamage, projectileSpeed * (projectileSpeedUpNum / 100), projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), isCritical, false, 0);
        }

        SoundManager.Instance.PlaySFX("MagicianAttack");
    }

    // 스킬: 느리고 커다란 관통 공격 3발 발사
    protected override IEnumerator FireSkill()
    {
        for (int i = 0; i < skillCount; i++)
        {
            yield return new WaitForSeconds(skillFireDelay);
            animator.Play("SKILL", -1, 0f);
            FireSkillProjectiles();
            Instantiate(skillActiveEffect, transform.position, Quaternion.identity, transform);
            SoundManager.Instance.PlaySFX("MagicianSkill");
            yield return new WaitForSeconds(skillInterval);
        }
    }

    // 스킬 발사 구현
    protected override void FireSkillProjectiles()
    {
        Transform target = FindNearestEnemy();

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        MagicBall mb = proj.GetComponent<MagicBall>();

        float totalSkillDamage = TotalSkillDamage();

        if (target != null)
        {
            mb.SetInit((target.position - firePoint.position).normalized, totalSkillDamage, projectileSpeed * (projectileSpeedUpNum / 100), skillSize, knockbackPower * (knockbackPowerUpNum / 100),false, isUpgradeSkillExplosionAttack, SkillExplosionAttackTime);
        }
        else
        {
            mb.SetInit((Random.insideUnitSphere).normalized, totalSkillDamage, projectileSpeed * (projectileSpeedUpNum / 100), skillSize, knockbackPower * (knockbackPowerUpNum / 100),false, isUpgradeSkillExplosionAttack, SkillExplosionAttackTime);
        }
    }

}