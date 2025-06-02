using System.Collections;
using UnityEngine;

public class Pirate : Character
{
    [Header("스킬")]
    public GameObject skillProjectile;
    public int skillCount = 3;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillSpeed = 10f;
    public float skillDamage = 1f;
    public int skillShotCount = 4;


    [Header("강화")]
    public float nomalAttackSize;
    public bool isBackwardCannonShot;
    public bool isFirstHitDealsBonusDamage;

    public int upgradeNum;
    public GameObject player;

    protected override void Start()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerMove>().gameObject;
    }

    // 일반 공격: 가까운 적에게 관통 공격
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        if (isBackwardCannonShot)
        {
            Vector2 direction2 = -1 * (targetPos + Vector3.up - firePoint.position).normalized;

            GameObject proj2 = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

            float nownomalAttackSize2 = nomalAttackSize;

            proj2.GetComponent<PirateAttack>().SetInit(direction2, attackDamage, projectileSpeed, nomalAttackSize, isFirstHitDealsBonusDamage);
        }

        Vector2 direction = (targetPos + Vector3.up - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float nownomalAttackSize = nomalAttackSize;

        proj.GetComponent<PirateAttack>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackSize, isFirstHitDealsBonusDamage);
    }

    // 스킬: 느리고 커다란 관통 공격
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < skillCount; i++)
        {
            rb.linearVelocity = Vector3.zero;
            yield return new WaitForSeconds(skillFireDelay);
             StartCoroutine(FireSkillCanon());
            yield return new WaitForSeconds(skillInterval);
        }
    }

    // 궁극기 발사 구현
    IEnumerator FireSkillCanon()
    {
        float minAngle = 60f;
        float maxAngle = 120f;


        for (int i = 0; i < skillShotCount; i++)
        {
            // 균등한 베이스 각도
            float t = i / (float)(skillShotCount - 1);
            float baseAngle = Mathf.Lerp(minAngle, maxAngle, t);

            // 랜덤으로 살짝 위아래 흔들림 추가
            float jitter = Random.Range(-15f, 15f);
            float finalAngle = baseAngle + jitter;

            Vector2 dir = Quaternion.Euler(0, 0, finalAngle) * Vector2.right;

            GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            PirateAttack mb = proj.GetComponent<PirateAttack>();
            mb.SetInit(dir.normalized, (int)(abilityPower), skillSpeed, nomalAttackSize, isFirstHitDealsBonusDamage);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
