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

        GameObject proj = Instantiate(normalProjectile, targetPos, Quaternion.identity);

        float nownomalAttackSize = nomalAttackSize;
        if (isnomalAttackSizePerMana) nownomalAttackSize *= currentMP / 50;

        proj.GetComponent<BlackHoleAttack>().SetInit(direction, attackDamage, projectileSpeed, nownomalAttackSize);
        
        SoundManager.Instance.PlaySFX("MagicianAttack");
    }

    // 스킬: 느리고 커다란 관통 공격 3발 발사
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(skillFireDelay);
        animator.Play("SKILL", -1, 0f);
        FireSkillProjectiles();
        //Instantiate(skillActiveEffect, transform.position, Quaternion.identity, transform);
        SoundManager.Instance.PlaySFX("MagicianSkill");
        yield return new WaitForSeconds(skillInterval);
    }

    // 스킬 발사 구현
    protected override void FireSkillProjectiles()
    {
        Transform target = FindFarestEnemy();
        if (target == null) target = transform;
        GameObject proj = Instantiate(skillProjectile, target.position, Quaternion.identity);
        BlackHoleSkill mb = proj.GetComponent<BlackHoleSkill>();

        if (target != null)
        {
            mb.SetInit(skillSize, skillDamage);
        }
        else
        {
            mb.SetInit(skillSize,skillDamage);
        }
    }

    private Transform FindFarestEnemy()
    {
        Transform farest = null;
        float maxDist = 0;

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Transform enemyTransform = obj.transform;

            // 카메라 뷰포트 안에 있는지 확인
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(enemyTransform.position);

            bool isVisible = viewportPos.z > 0 &&
                             viewportPos.x >= 0 && viewportPos.x <= 1 &&
                             viewportPos.y >= 0 && viewportPos.y <= 1;

            if (!isVisible) continue;

            // 가까운 적 계산
            float dist = Vector2.Distance(transform.position, enemyTransform.position);
            if (dist > maxDist)
            {
                maxDist = dist;
                farest = enemyTransform;
            }
        }

        return farest;
    }

}