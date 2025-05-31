using System.Collections;
using UnityEngine;

public class Pirate : Character
{
    [Header("��ų")]
    public GameObject skillProjectile;
    public int skillCount = 3;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillSize = 1f;
    public float skillDamage = 1f;

    [Header("��ȭ")]
    public float nomalAttackSize;
    public bool isAddAbilityPower;
    public bool isnomalAttackSizePerMana;
    public bool isCanTeleport;
    public int upgradeNum;
    public GameObject player;

    protected override void Start()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerMove>().gameObject;
    }

    // �Ϲ� ����: ����� ������ ���� ����
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos + Vector3.up - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float nownomalAttackSize = nomalAttackSize;
        if (isnomalAttackSizePerMana) nownomalAttackSize *= currentMP / 50;

        if (isAddAbilityPower) proj.GetComponent<MagicBall>().SetInit(direction, abilityPower + attackDamage, projectileSpeed, nownomalAttackSize);
        else
        {
            proj.GetComponent<PirateAttack>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackSize);
        }
    }

    // ��ų: ������ Ŀ�ٶ� ���� ����
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 4; i++)
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
        int shotCount = 4;

        for (int i = 0; i < shotCount; i++)
        {
            // �յ��� ���̽� ����
            float t = i / (float)(shotCount - 1);
            float baseAngle = Mathf.Lerp(minAngle, maxAngle, t);

            // �������� ��¦ ���Ʒ� ��鸲 �߰�
            float jitter = Random.Range(-15f, 15f);
            float finalAngle = baseAngle + jitter;

            Vector2 dir = Quaternion.Euler(0, 0, finalAngle) * Vector2.right;

            GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            PirateAttack mb = proj.GetComponent<PirateAttack>();
            mb.SetInit(dir.normalized, (int)(abilityPower), projectileSpeed - 5, skillSize);
            yield return new WaitForSeconds(0.05f);
        }
    }


}
