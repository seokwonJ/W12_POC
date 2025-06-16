using System.Collections;
using UnityEngine;

public class Ninja : Character
{
    [Header("스킬")]
    public GameObject skillKunai;
    public bool isSkillLanding;
    public bool isSkilling;
    public int skillPower;
    public float skillAttackSpeed;
    public float skillPowerDuration;
    public float skillInterval = 0.3f;

    [Header("강화")]
    public bool isNomalAttackFive;
    public int nomalAttackCount = 0;
    public bool isFirstLowHPEnemy;
    public bool isAttackSpeedPerMana;
    public int upgradeNum;

    [Header("이펙트")]
    public GameObject skillLandingActive;


    // 일반 공격: 원거리 투사체 표창 가까운 적에게 던지기
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        if (isFirstLowHPEnemy)
        {
            targetPos = FindLowHPEnemy().position;
        }

        nomalAttackCount += 1;

        Vector2 direction = (targetPos - firePoint.position).normalized;

        // 방향에 따라 캐릭터 스프라이트 좌우 반전
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        GameObject proj;

        if (isSkilling)
        {
            proj = Instantiate(skillKunai, firePoint.position, Quaternion.identity);

            float totalSkillDamage = TotalSkillDamage();

            proj.GetComponent<Kunai>().SetInit(direction, totalSkillDamage, projectileSpeed * 2.5f);
        }
        else
        {
            proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

            float totalAttackDamage = TotalAttackDamage();

            if (isNomalAttackFive && nomalAttackCount == 5) { proj.GetComponent<Kunai>().SetInit(direction, totalAttackDamage, projectileSpeed); nomalAttackCount = 0; }
            else proj.GetComponent<Kunai>().SetInit(direction, attackDamage, projectileSpeed);
        }

        SoundManager.Instance.PlaySFX("NinjaAttack");
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

    // 스킬 : 점프 후 착지시 3초간 공격력 강화
    protected override IEnumerator FireSkill()
    {
        isSkillLanding = true;
        yield return new WaitForSeconds(skillInterval);
    }

    // 착지했을 경우
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision); // 부모 로직 먼저 실행

        // 조건 만족 안 하면 아무것도 안 하고 리턴 (부모에서 이미 isGround 처리됨)
        if (!isGround) return;

        // 추가 스킬 처리
        if (isSkillLanding)
        {
            isSkillLanding = false;
            StartCoroutine(PowerUp(skillPower));
        }
    }

    private GameObject activeParticle;

    IEnumerator PowerUp(int power)
    {
        SoundManager.Instance.PlaySFX("NinjaSkillActive");
        SoundManager.Instance.PlaySFX("NinjaSkillDuration");

        activeParticle = Instantiate(skillLandingActive,transform.position,Quaternion.identity,transform);
        isSkilling = true;
        normalFireInterval /= skillAttackSpeed;
        Debug.Log("공격속도 업!");

        yield return new WaitForSeconds(skillPowerDuration);

        Debug.Log("공격속도 돌아옴");
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

    public override void EndFieldAct() // 필드전투가 종료될 때 실행
    {
        base.EndFieldAct();

        if (isSkilling == true)
        {
            Debug.Log("공격속도 돌아옴");
            normalFireInterval *= skillAttackSpeed;
            isSkilling = false;
        }
        if (activeParticle != null)
        {
            Destroy(activeParticle);
        }

        isSkillLanding = false;
    }
}
