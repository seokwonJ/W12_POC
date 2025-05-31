using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Levi : Character
{
    [Header("������ ����")]
    public float dobleAttackCoolTime = 0.05f;


    [Header("��ų")]
    public bool isSkillLanding;
    public int skillDamage;
    public float skillCount;
    public float skillInterval = 0.3f;
    public float skillDashSpeed;

    [Header("��ȭ")]
    public bool isNomalAttackFive;
    public int nomalAttackCount = 0;
    public bool isFirstLowHPEnemy;
    public bool isAttackSpeedPerMana;

    public GameObject trail;
 
    public int upgradeNum;

    // �Ϲ� ����: ���Ÿ� ����ü ǥâ ����� ������ ������
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        nomalAttackCount += 1;

        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        // ���� ����ü ������ ����ȴٸ� ��ȭ������ �̰��� ����Ǿ���� ��
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

    // ��ų : ���� �� ������ 3�ʰ� ���ݷ� ��ȭ
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

            // ����
            yield return StartCoroutine(DashToTarget(target));

            // �߰��� �׾��� ���
            if (target == null) continue;

            // ������ �ֱ�
            EnemyHP enemyHP = target.GetComponent<EnemyHP>();
            if (enemyHP != null)
            {
                enemyHP.TakeDamage(attackDamage + skillDamage);
            }

            yield return new WaitForSeconds(0.02f); // �ణ�� ������

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

            // ī�޶� ����Ʈ �ȿ� �ִ��� Ȯ��
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(enemyTransform.position);

            bool isVisible = viewportPos.z > 0 &&
                             viewportPos.x >= 0 && viewportPos.x <= 1 &&
                             viewportPos.y >= 0 && viewportPos.y <= 1;

            if (!isVisible) continue;

            // ����� �� ���
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
            rb.MovePosition(move); // ���� ����
            
            if (dir.x >= 0) transform.right = Vector3.right;
            else transform.right = Vector3.left;

            yield return new WaitForFixedUpdate(); // FixedUpdate ����
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
