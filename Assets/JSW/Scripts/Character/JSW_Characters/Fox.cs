using System.Collections;
using UnityEngine;

public class Fox : Character
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
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float nownomalAttackSize = nomalAttackSize;
        if (isnomalAttackSizePerMana) nownomalAttackSize *= currentMP / 50;

        if (isAddAbilityPower) proj.GetComponent<FoxAttack>().SetInit(direction, abilityPower + attackDamage, projectileSpeed, nownomalAttackSize, transform);
        else
        {
            proj.GetComponent<FoxAttack>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackSize, transform);
        }
    }

    // ��ų: ������ Ŀ�ٶ� ���� ����
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(skillFireDelay);
        FireSkillProjectiles();
    }

    // �ñر� �߻� ����
    protected override void FireSkillProjectiles()
    {
        int shotCount = 16;
        float angleStep = 360f / shotCount;

        for (int i = 0; i < shotCount; i++)
        {
            float angle = i * angleStep;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;

            GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            FoxAttack mb = proj.GetComponent<FoxAttack>();
            mb.SetInit(dir.normalized, (int)(abilityPower * skillDamage), projectileSpeed, skillSize, transform);
            mb.speed = 5;
        }
    }
}