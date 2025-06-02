using System.Collections;
using UnityEngine;

public class Fox : Character
{
    [Header("스킬")]
    public GameObject skillProjectile;
    public int skillCount = 10;
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

    // 일반 공격: 원형 관통하고 돌아오는 원형 정수 발사
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        // 방향에 따라 캐릭터 스프라이트 좌우 반전
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float nownomalAttackSize = nomalAttackSize;

        float totalAttackDamage = 0;

        totalAttackDamage += abilityPower;

        if (isMoreDamageBasedOnOnboardAllies) totalAttackDamage += Managers.Rider.riderCount;

        if (isEmpoweredAttackEvery3Hits)
        {
            EmpoweredAttackEveryCount += 1;
            if (EmpoweredAttackEveryCount == 3)
            {
                proj.GetComponent<FoxAttack>().SetInit(direction, (int)totalAttackDamage * 2, projectileSpeed, nomalAttackSize, transform, isReturnDamageScalesWithHitCount, skillTime, isOrbPausesBeforeReturning);
                EmpoweredAttackEveryCount = 0;
            }
            else
            {
                proj.GetComponent<FoxAttack>().SetInit(direction, (int)totalAttackDamage, projectileSpeed, nomalAttackSize, transform, isReturnDamageScalesWithHitCount, skillTime, isOrbPausesBeforeReturning);
            }

        }
        else
        {
            proj.GetComponent<FoxAttack>().SetInit(direction, (int)totalAttackDamage, projectileSpeed, nomalAttackSize, transform, isReturnDamageScalesWithHitCount, skillTime, isOrbPausesBeforeReturning);
        }
    }

    // 스킬: 점프 후 원형 정수를 360도 사방으로 발사
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