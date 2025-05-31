using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Levi : Character
{
    [Header("리바이 공격")]
    public float dobleAttackCoolTime = 0.05f;


    [Header("스킬")]
    public bool isSkillLanding;
    public int skillDamage;
    public float skillCount;
    public float skillInterval = 0.3f;
    public float skillDashSpeed;

    [Header("강화")]
    public bool isNomalAttackFive;
    public int nomalAttackCount = 0;
    public bool isFirstLowHPEnemy;
    public bool isAttackSpeedPerMana;

    public GameObject trail;
 
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
        trail.SetActive(true);
        List<Transform> hitEnemies = new List<Transform>();

        gameObject.layer = LayerMask.NameToLayer("DoSkill");

        Transform target;

        for (int i = 0; i < skillCount; i++)
        {
            target = FindFarestEnemyExcluding(hitEnemies);
            if (target == null) break;

            hitEnemies.Add(target);

            // 돌진
            yield return StartCoroutine(DashToTarget(target));

            // 중간에 죽었을 경우
            if (target == null) continue;

            // 데미지 주기
            EnemyHP enemyHP = target.GetComponent<EnemyHP>();
            if (enemyHP != null)
            {
                enemyHP.TakeDamage(attackDamage + skillDamage);
            }

            yield return new WaitForSeconds(0.02f); // 약간의 딜레이

        }

        transform.up = Vector3.up;

        rb.linearVelocity = new Vector2(-10, jumpForce);
        trail.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        gameObject.layer = LayerMask.NameToLayer("Character");
    }

    private Transform FindFarestEnemyExcluding(List<Transform> excluded)
    {
        Transform farest = null;
        float maxDist = 0;

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Transform enemyTransform = obj.transform;

            if (excluded.Contains(enemyTransform)) continue;

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

    private IEnumerator DashToTarget(Transform target)
    {
        float dashSpeed = skillDashSpeed;
        float reachDist = 1f;

        while (target != null && Vector2.Distance(transform.position, target.position) > reachDist)
        {
            if (target == null || target.gameObject == null)
            {
                break;
            }

            Vector2 dir = (target.position - transform.position).normalized;
            Vector2 move = (Vector2)transform.position + dir * dashSpeed * Time.fixedDeltaTime;
            rb.MovePosition(move); // 감속 없음
            
            if (dir.x >= 0) transform.right = Vector3.right;
            else transform.right = Vector3.left;

            yield return new WaitForFixedUpdate(); // FixedUpdate 기준
        }

        rb.linearVelocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isGround && collision.tag == "Enemy" && isUltimateActive)
        {
            collision.GetComponent<EnemyHP>().TakeDamage(attackDamage);
        }
    }
}
