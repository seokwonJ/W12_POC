using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levi : Character
{
    [Header("������ ����")]
    public float dobleAttackCoolTime = 0.05f;


    [Header("��ų")]
    public bool isSkillLanding;
    public int skillDamage;
    public float skillCount;
    public float skillInterval = 0.3f;

    [Header("��ȭ")]
    public bool isNomalAttackFive;
    public int nomalAttackCount = 0;
    public bool isFirstLowHPEnemy;
    public bool isAttackSpeedPerMana;

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
        Debug.Log("���������!");

        List<Transform> hitEnemies = new List<Transform>();

        gameObject.layer = LayerMask.NameToLayer("DoSkill");

        for (int i = 0; i < skillCount; i++)
        {
            Transform target = FindNearestEnemyExcluding(hitEnemies);
            if (target == null) break;

            hitEnemies.Add(target);

            // ����
            yield return StartCoroutine(DashToTarget(target));

            // ������ �ֱ�
            EnemyHP enemyHP = target.GetComponent<EnemyHP>();
            if (enemyHP != null)
            {
                enemyHP.TakeDamage(attackDamage + skillDamage);
            }

            yield return new WaitForSeconds(0.02f); // �ణ�� ������
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
            rb.MovePosition(move); // ���� ����

            transform.right = dir;

            yield return new WaitForFixedUpdate(); // FixedUpdate ����
        }

        rb.linearVelocity = Vector2.zero;
    }

    // �������� ���
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
