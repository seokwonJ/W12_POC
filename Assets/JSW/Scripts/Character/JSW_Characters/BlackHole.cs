using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : Character
{
    [Header("스킬")]
    public GameObject skillProjectile;
    public int skillCount = 3;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillSize = 1f;
    public float skillDuration = 5f;
    public float skillPullInterval = 0.2f;

    [Header("강화")]
    public bool isUpgradeSkillSizeDownExplosion;
    public float explosionDamagePercent = 150;
    public bool isUpgradeSkillEnemyDenfenseDown;
    public float skillEnemyDenfenseDownPercent;
    public float skillEnemyDenfenseDownDuration;

    public int upgradeNum;

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

        GameObject proj = Instantiate(normalProjectile, targetPos, Quaternion.identity);

        float totalAttackDamage = TotalAttackDamage();
        bool isCritical = IsCriticalHit();
        if (isCritical) totalAttackDamage *= ((criticalDamage * criticalDamageUpNum / 100) / 100);

        proj.GetComponent<BlackHoleAttack>().SetInit(direction, totalAttackDamage, projectileSpeed * (projectileSpeedUpNum / 100), projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), isCritical);

        SoundManager.Instance.PlaySFX("BlackHoleAttack");
    }

    // 스킬: 느리고 커다란 관통 공격 3발 발사
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(skillFireDelay);
        animator.Play("SKILL", -1, 0f);
        FireSkillProjectiles();
        Instantiate(skillActiveEffect, transform.position, Quaternion.identity, transform);
        SoundManager.Instance.PlaySFX("BlackHoleSkillActive");
        yield return new WaitForSeconds(skillInterval);
    }

    // 스킬 발사 구현
    protected override void FireSkillProjectiles()
    {
        Transform target = FindClusteredEnemy();
        if (target == null) target = transform;
        GameObject proj = Instantiate(skillProjectile, target.position, Quaternion.identity);
        BlackHoleSkill mb = proj.GetComponent<BlackHoleSkill>();

        float totalSkillDamage = TotalSkillDamage();
        float explosionDamage = 0;
        if (isUpgradeSkillSizeDownExplosion) explosionDamage = attackBase * abilityPower * explosionDamagePercent / 100;

        mb.SetInit(skillSize, totalSkillDamage, knockbackPower, skillDuration, skillPullInterval, isUpgradeSkillSizeDownExplosion, explosionDamage, isUpgradeSkillEnemyDenfenseDown, skillEnemyDenfenseDownPercent, skillEnemyDenfenseDownDuration);
    }

    private Transform FindClusteredEnemy()
    {
        Transform bestTarget = null;
        int maxCount = -1;
        float closestToPlayer = float.MaxValue;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject centerObj in enemies)
        {
            Transform center = centerObj.transform;

            // 카메라 뷰 안에 있는 적만 대상으로 함
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(center.position);
            bool isVisible = viewportPos.z > 0 &&
                             viewportPos.x >= 0 && viewportPos.x <= 1 &&
                             viewportPos.y >= 0 && viewportPos.y <= 1;

            if (!isVisible) continue;

            int nearbyCount = 0;

            foreach (GameObject otherObj in enemies)
            {
                if (centerObj == otherObj) continue;

                float dist = Vector2.Distance(center.position, otherObj.transform.position);
                if (dist <= skillSize * 2)
                {
                    nearbyCount++;
                }
            }

            float distToPlayer = Vector2.Distance(center.position, player.transform.position);

            // 우선순위: 적 수 > 플레이어와의 거리
            if (nearbyCount > maxCount || (nearbyCount == maxCount && distToPlayer < closestToPlayer))
            {
                maxCount = nearbyCount;
                closestToPlayer = distToPlayer;
                bestTarget = center;
            }

            print($"[{center.name}] 주변 적 수: {nearbyCount}, 플레이어 거리: {distToPlayer}");
        }

        return bestTarget;
    }

}