using System.Collections;
using UnityEngine;

public class Magician : Character
{
    [Header("스킬")]
    public GameObject skillProjectile;
    public int skillCount = 3;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillSize = 1f;
    public float skillDamage = 1f;
    public float skillProjectileSpeed = 15;

    [Header("강화")]
    public float nomalAttackSize;
    public bool isAddAttackDamage;
    public bool isnomalAttackSizePerMana;
    public bool isCanTeleport;
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

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float nownomalAttackSize = nomalAttackSize;
        if (isnomalAttackSizePerMana) nownomalAttackSize *= currentMP / 50;

        if (isAddAttackDamage) proj.GetComponent<MagicBall>().SetInit(direction, abilityPower + attackDamage, projectileSpeed, nownomalAttackSize);
        else
        {
            proj.GetComponent<MagicBall>().SetInit(direction, attackDamage, projectileSpeed, nownomalAttackSize);
        }

        SoundManager.Instance.PlaySFX("MagicianAttack");
    }

    // 스킬: 느리고 커다란 관통 공격 3발 발사
    protected override IEnumerator FireSkill()
    {
        if (isCanTeleport) StartCoroutine(TeleportToPlayer());

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

        if (target != null)
        {
            mb.SetInit((target.position - firePoint.position).normalized, (int)(attackDamage + skillDamage), skillProjectileSpeed, skillSize);
        }
        else
        {
            mb.SetInit((Random.insideUnitSphere).normalized, (int)(attackDamage + skillDamage), skillProjectileSpeed, skillSize);
        }
    }

    IEnumerator TeleportToPlayer()
    {
        yield return new WaitForSeconds(5f);
        if (!isGround) transform.position = player.transform.position + Vector3.up;
    }
}