using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class Tanker : Character
{
    [Header("��ų")]
    public bool isSkillLanding;
    public int skillDamage;
    public float skillknockbackPower;
    public float skillInterval = 0.3f;
    public float skillRange;
    public GameObject landingSkillEffect;

    [Header("����")]
    public float nomalAttackSize;
    public float nomalAttackLifetime;
    public float knockBackpower = 1;

    [Header("��ȭ")]
    public bool isFallingSpeedToSkillDamage;
    public bool isShieldFlyer;
    public bool isHitSkillPerGetMana;
    public bool isCloserMoreDamage;

    public int upgradeNum;

    private PlayerStatus _playerStatus;

    protected override void Start()
    {
        base.Start();
        _playerStatus = FindAnyObjectByType<PlayerStatus>();
    }

    // �Ϲ� ����: ������ ����ü �߻�
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<TankerAttack>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackLifetime, nomalAttackSize, knockBackpower); // �� �޼��尡 ���ٸ� �׳� ���� �����ؼ� ���� ��
    }

    // ��ų: Ŀ�ٶ� ������ ����ü 3�� ���� �߻�
    protected override IEnumerator FireSkill()
    {
        if (isShieldFlyer) _playerStatus.defensePower -= 5;
        isSkillLanding = true;

        yield return new WaitForSeconds(skillInterval);
    }

    // �������� ���
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        ContactPoint2D contact = collision.contacts[0];
        if (Vector2.Dot(contact.normal, Vector2.up) < 0.9f) return;


        if (isSkillActive) return;
        isGround = true;

        if (isShieldFlyer) _playerStatus.defensePower += 5;

        if (isSkillLanding)
        {
            isSkillLanding = false;
            LandingSkill(skillDamage);
        }
        Managers.Rider.RiderCountUp();
        fixedJoint.enabled = true;
        fixedJoint.connectedBody = collision.rigidbody;
    }


    // �������� ��� ������ ����ü ������� ������ �˹�
    void LandingSkill(int skillDamageNum)
    {
        GameObject landingSkillEffectObject = Instantiate(landingSkillEffect, transform.position, Quaternion.identity); 

        Debug.Log("���� ��ų");

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, skillRange);

        int hitEnemyCount = 0;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hitEnemyCount += 1;

                EnemyAI enemyAI = hit.GetComponent<EnemyAI>();
                EnemyHP enemyHP = hit.GetComponent<EnemyHP>();

                int totalDamage = skillDamageNum;
                if (isCloserMoreDamage) totalDamage += (int)(skillRange / Vector2.Distance(hit.transform.position, transform.position));

                if (isFallingSpeedToSkillDamage) enemyHP.TakeDamage(totalDamage + (int)rb.linearVelocity.magnitude);
                else enemyHP.TakeDamage(totalDamage);

                Vector3 knockbackDirection = hit.transform.position - transform.position;
                if (enemyAI != null) enemyAI.ApplyKnockback(knockbackDirection, skillknockbackPower);
                else
                {
                    Enemy2 enemy2 = hit.GetComponent<Enemy2>();
                    enemy2.ApplyKnockback(knockbackDirection, skillknockbackPower);
                }
            }
            if (hit.CompareTag("EnemyAttack"))
            {
                Destroy(hit.gameObject);
            }
        }

        if (isHitSkillPerGetMana) currentMP += 2 * hitEnemyCount;

        landingSkillEffectObject.transform.localScale = Vector3.one * skillRange * 2;
        Destroy(landingSkillEffectObject, 0.1f);
    }
}
