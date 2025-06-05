using System.Collections;
using UnityEngine;

public class Ninja : Character
{
    [Header("��ų")]
    public GameObject skillKunai;
    public bool isSkillLanding;
    public bool isSkilling;
    public int skillPower;
    public float skillAttackSpeed;
    public float skillPowerDuration;
    public float skillInterval = 0.3f;

    [Header("��ȭ")]
    public bool isNomalAttackFive;
    public int nomalAttackCount = 0;
    public bool isFirstLowHPEnemy;
    public bool isAttackSpeedPerMana;
    public int upgradeNum;

    [Header("����Ʈ")]
    public GameObject skillLandingActive;


    // �Ϲ� ����: ���Ÿ� ����ü ǥâ ����� ������ ������
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        if (isFirstLowHPEnemy)
        {
            targetPos = FindLowHPEnemy().position;
        }

        nomalAttackCount += 1;

        Vector2 direction = (targetPos - firePoint.position).normalized;

        // ���⿡ ���� ĳ���� ��������Ʈ �¿� ����
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        GameObject proj;

        if (isSkilling)
        {
            proj = Instantiate(skillKunai, firePoint.position, Quaternion.identity);
            proj.GetComponent<Kunai>().SetInit(direction, attackDamage, projectileSpeed * 2.5f);
        }
        else
        {
            proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            // ���� ����ü ������ ����ȴٸ� ��ȭ������ �̰��� ����Ǿ���� ��
            if (isNomalAttackFive && nomalAttackCount == 5) { proj.GetComponent<Kunai>().SetInit(direction, attackDamage + skillPower, projectileSpeed); nomalAttackCount = 0; }
            else proj.GetComponent<Kunai>().SetInit(direction, attackDamage, projectileSpeed);
        }
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
                if (isSkilling)
                {
                    animator.Play("SKILLINGATTACK", -1, 0f);
                    FireNormalProjectile(target.position);
                }
                else
                {
                    animator.Play("ATTACK", -1, 0f);
                    FireNormalProjectile(target.position);
                }
            }
        }
    }

    // ��ų : ���� �� ������ 3�ʰ� ���ݷ� ��ȭ
    protected override IEnumerator FireSkill()
    {
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
        animator.SetBool("5_Fall", false);
        animator.Play("IDLE", -1, 0f);

        if (isSkillLanding)
        {
            isSkillLanding = false;
            StartCoroutine(PowerUp(skillPower));
        }
        Managers.Status.RiderCount++;
        fixedJoint.enabled = true;
        fixedJoint.connectedBody = collision.rigidbody;
        fallingTrail.SetActive(false);
    }
    IEnumerator PowerUp(int power)
    {
        GameObject activeParticle = Instantiate(skillLandingActive,transform.position,Quaternion.identity,transform);
        isSkilling = true;
        attackDamage += power;
        normalFireInterval /= skillAttackSpeed;
        Debug.Log("���ݷ� ��!");

        yield return new WaitForSeconds(skillPowerDuration);

        Debug.Log("���ݷ� ���ƿ�");
        attackDamage -= power;
        normalFireInterval *= skillAttackSpeed;
        isSkilling = false;
        Destroy(activeParticle);
    }

    protected Transform FindLowHPEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, enemyDetectRadius);
        float minHP = float.MaxValue;
        Transform nearest = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float hp = hit.GetComponent<EnemyHP>().enemyHP;
                if (hp < minHP)
                {
                    minHP = hp;
                    nearest = hit.transform;
                }
            }
        }
        return nearest;
    }
}
