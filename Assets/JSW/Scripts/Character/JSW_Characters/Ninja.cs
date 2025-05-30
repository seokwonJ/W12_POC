using System.Collections;
using UnityEngine;

public class Ninja : Character
{
    public float skillInterval = 0.3f;
    [Header("닌자 스킬")]
    public bool isSkillLanding;
    public int skillPower;
    public float skillPowerDuration;


    [Header("강화")]
    public bool isNomalAttackFive;
    public int NomalAttackCount = 0;
    public bool isFirstLowHPEnemy;
    public bool isAttackSpeedPerMana; 

    public int upgradeNum;

    // 일반 공격: 원거리 투사체 표창 가까운 적에게 던지기
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        if (isFirstLowHPEnemy)
        {
            targetPos = FindLowHPEnemy().position;
        }


        NomalAttackCount += 1;

        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        if (isNomalAttackFive && NomalAttackCount == 5) { proj.GetComponent<Kunai>().SetInit(direction, attackDamage + skillPower, projectileSpeed);  }
        else proj.GetComponent<Kunai>().SetInit(direction, attackDamage, projectileSpeed);
    }

    protected override void Update()
    {
        base.Update();
    }

    // 스킬 : 점프 후 착지시 3초간 공격력 강화
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(skillInterval);

        if (isAttackSpeedPerMana)
        {
            normalFireInterval -= currentMP / 3000;
        }
    }

    // 착지했을 경우
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        ContactPoint2D contact = collision.contacts[0];
        if (Vector2.Dot(contact.normal, Vector2.up) < 0.9f) return;

        if (isUltimateActive) return;
        isGround = true;
        if (isSkillLanding)
        {
            isSkillLanding = false;
            StartCoroutine(PowerUp(skillPower));
        }
        RiderManager.Instance.RiderCountUp();
        fixedJoint.enabled = true;
        fixedJoint.connectedBody = collision.rigidbody;
    }

    IEnumerator PowerUp(float power)
    {
        // 공격력 += power
        Debug.Log("공격력 업!");
        yield return new WaitForSeconds(skillPowerDuration);
        Debug.Log("공격력 돌아옴");
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
