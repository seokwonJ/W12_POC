using System.Collections;
using UnityEngine;

public class Pirate : Character
{
    [Header("스킬")]
    public GameObject skillProjectile;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillSpeed = 10f;
    public int skillShotCount = 8;

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

    // 일반 공격: 대포알 한발씩 발사 대포알 경우 광역 넉백
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

        // 방향에 따라 캐릭터 스프라이트 좌우 반전
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float nownomalAttackSize = nomalAttackSize;

        proj.GetComponent<PirateAttack>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackSize, isFirstHitDealsBonusDamage);
    }

    // 스킬: 점프 후 공중에 대포알들 여러발 발사
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(1f);


        yield return new WaitForSeconds(skillFireDelay);
        yield return StartCoroutine(FireSkillCanon());

    }

    // 궁극기 발사 구현
    IEnumerator FireSkillCanon()
    {
        float minAngle = 60f;
        float maxAngle = 120f;


        for (int i = 0; i < skillShotCount; i++)
        {
            rb.linearVelocity = Vector3.zero;

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
            yield return new WaitForSeconds(0.1f);
        }
    }
}
