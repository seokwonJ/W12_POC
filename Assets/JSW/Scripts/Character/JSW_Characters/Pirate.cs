using System.Collections;
using UnityEngine;

public class Pirate : Character
{
    [Header("��ų")]
    public GameObject skillProjectile;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillSpeed = 10f;
    public int skillShotCount = 8;

    [Header("��ȭ")]
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

    // �Ϲ� ����: ������ �ѹ߾� �߻� ������ ��� ���� �˹�
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

        // ���⿡ ���� ĳ���� ��������Ʈ �¿� ����
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float nownomalAttackSize = nomalAttackSize;

        proj.GetComponent<PirateAttack>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackSize, isFirstHitDealsBonusDamage);
    }

    // ��ų: ���� �� ���߿� �����˵� ������ �߻�
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(1f);


        yield return new WaitForSeconds(skillFireDelay);
        yield return StartCoroutine(FireSkillCanon());

    }

    // �ñر� �߻� ����
    IEnumerator FireSkillCanon()
    {
        float minAngle = 60f;
        float maxAngle = 120f;


        for (int i = 0; i < skillShotCount; i++)
        {
            rb.linearVelocity = Vector3.zero;

            // �յ��� ���̽� ����
            float t = i / (float)(skillShotCount - 1);
            float baseAngle = Mathf.Lerp(minAngle, maxAngle, t);

            // �������� ��¦ ���Ʒ� ��鸲 �߰�
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
