using System.Collections;
using UnityEngine;

public class Fox : Character
{
    [Header("스킬")]
    public GameObject skillProjectile;
    public int skillCount = 3;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillSize = 1f;
    public float skillDamage = 1f;
    public float skillTime = 0.7f;

    [Header("강화")]
    public float nomalAttackSize;
    public bool isReturnDamageScalesWithHitCount;
    public bool isEmpoweredAttackEvery3Hits;
    public int EmpoweredAttackEveryCount;
    public bool isMoreDamageBasedOnOnboardAllies;
    public bool isOrbPausesBeforeReturning;
    public bool isAutoReturnAfterSeconds;

    public int upgradeNum;
    public GameObject player;

    protected override void Start()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerMove>().gameObject;
    }

    // 일반 공격: 가까운 적에게 관통 공격
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float nownomalAttackSize = nomalAttackSize;

        float totalAttackDamage = 0;

        if (isMoreDamageBasedOnOnboardAllies) totalAttackDamage += Managers.Status.RiderCount;

        totalAttackDamage = abilityPower;

        if (isEmpoweredAttackEvery3Hits)
        {
            EmpoweredAttackEveryCount += 1;
            if (EmpoweredAttackEveryCount == 3)
            {
                proj.GetComponent<FoxAttack>().SetInit(direction, (int)totalAttackDamage * 2, projectileSpeed, nomalAttackSize, transform, isReturnDamageScalesWithHitCount, skillTime, isOrbPausesBeforeReturning);
            }
        }
        else
        {
            proj.GetComponent<FoxAttack>().SetInit(direction, (int)totalAttackDamage, projectileSpeed, nomalAttackSize, transform, isReturnDamageScalesWithHitCount, skillTime, isOrbPausesBeforeReturning);
        }
    }

    // 스킬: 느리고 커다란 관통 공격
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(skillFireDelay);
        FireSkillProjectiles();
        if(isAutoReturnAfterSeconds) StartCoroutine(TeleportToPlayer());
    }

    // 궁극기 발사 구현
    protected override void FireSkillProjectiles()
    {

        float angleStep = 360f / skillCount;

        for (int i = 0; i < skillCount; i++)
        {
            float angle = i * angleStep;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;

            GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            FoxAttack mb = proj.GetComponent<FoxAttack>();
            mb.SetInit(dir.normalized, (int)(abilityPower * skillDamage), projectileSpeed, skillSize, transform, isReturnDamageScalesWithHitCount, skillTime, isOrbPausesBeforeReturning);
            mb.speed = 5;
        }
    }

    IEnumerator TeleportToPlayer()
    {
        yield return new WaitForSeconds(7f);
        if (!isGround) transform.position = player.transform.position + Vector3.up;
    }
}