using System.Collections;
using UnityEngine;

public class Pirate : Character
{
    [Header("��ų")]
    public GameObject skillProjectile;
    public int skillCount = 3;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillSpeed = 10f;
    public int skillShotCount = 4;

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

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float nownomalAttackSize = nomalAttackSize;

        proj.GetComponent<PirateAttack>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackSize, isFirstHitDealsBonusDamage);
    }

    // ��ų: ���� �� ���߿� �����˵� ������ �߻�
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

    // �ñر� �߻� ����
    IEnumerator FireSkillCanon()
    {
        float minAngle = 60f;
        float maxAngle = 120f;


        for (int i = 0; i < skillShotCount; i++)
        {
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
            yield return new WaitForSeconds(0.05f);
        }
    }
}
