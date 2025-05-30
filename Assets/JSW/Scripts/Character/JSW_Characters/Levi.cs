using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levi : Character
{
    [Header("리바이 공격")]
    public float dobleAttackCoolTime = 0.05f;


    [Header("스킬")]
    public bool isSkillLanding;
    public int skillDamage;
    public float skillCount;
    public float skillInterval = 0.3f;

    [Header("강화")]
    public bool isNomalAttackFive;
    public int nomalAttackCount = 0;
    public bool isFirstLowHPEnemy;
    public bool isAttackSpeedPerMana;

    public int upgradeNum;

    // 일반 공격: 원거리 투사체 표창 가까운 적에게 던지기
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        nomalAttackCount += 1;

        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        // 만약 투사체 에셋이 적용된다면 강화공격이 이곳에 적용되어야할 듯
        if (isNomalAttackFive && nomalAttackCount == 5) { proj.GetComponent<Kunai>().SetInit(direction, attackDamage + skillDamage, projectileSpeed); }
        else proj.GetComponent<LeviAttack>().SetInit(direction, attackDamage, projectileSpeed);
    }

    protected override IEnumerator NormalAttackRoutine()
    {
        float currnetNormalFireInterval;

        while (true)
        {
            if (isAttackSpeedPerMana) currnetNormalFireInterval = normalFireInterval - currentMP / 600;
            else currnetNormalFireInterval = normalFireInterval;

            yield return new WaitForSeconds(currnetNormalFireInterval);
            if (!isGround) continue;

            Transform target = FindNearestEnemy();
            if (target != null)
            {
                FireNormalProjectile(target.position);
                yield return new WaitForSeconds(dobleAttackCoolTime);
                FireNormalProjectile(target.position);
            }
        }
    }

    // 스킬 : 점프 후 착지시 3초간 공격력 강화
    protected override IEnumerator FireSkill()
    {
        Debug.Log("돌진가즈아!");

        List<Transform> hitEnemies = new List<Transform>();

        gameObject.layer = LayerMask.NameToLayer("DoSkill");

        for (int i = 0; i < skillCount; i++)
        {
            Transform target = FindNearestEnemyExcluding(hitEnemies);
            if (target == null) break;

            hitEnemies.Add(target);

            // 돌진
            yield return StartCoroutine(DashToTarget(target));

            // 데미지 주기
            EnemyHP enemyHP = target.GetComponent<EnemyHP>();
            if (enemyHP != null)
            {
                enemyHP.TakeDamage(attackDamage + skillDamage);
            }

            yield return new WaitForSeconds(0.02f); // 약간의 딜레이
        }

        gameObject.layer = LayerMask.NameToLayer("Character");

        transform.up = Vector3.up;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private Transform FindNearestEnemyExcluding(List<Transform> excluded)
    {
        Transform nearest = null;
        float minDist = float.MaxValue;

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (excluded.Contains(obj.transform)) continue;

            float dist = Vector2.Distance(transform.position, obj.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = obj.transform;
            }
        }

        return nearest;
    }

    private IEnumerator DashToTarget(Transform target)
    {
        float dashSpeed = 25;
        float reachDist = 1f;

        while (target != null && Vector2.Distance(transform.position, target.position) > reachDist)
        {
            Vector2 dir = (target.position - transform.position).normalized;
            Vector2 move = (Vector2)transform.position + dir * dashSpeed * Time.fixedDeltaTime;
            rb.MovePosition(move); // 감속 없음

            transform.right = dir;

            yield return new WaitForFixedUpdate(); // FixedUpdate 기준
        }

        rb.linearVelocity = Vector2.zero;
    }

    // 착지했을 경우
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        ContactPoint2D contact = collision.contacts[0];
        if (Vector2.Dot(contact.normal, Vector2.up) < 0.9f) return;

        if (isUltimateActive) return;

        isGround = true;
        RiderManager.Instance.RiderCountUp();
        fixedJoint.enabled = true;
        fixedJoint.connectedBody = collision.rigidbody;
    }
}
