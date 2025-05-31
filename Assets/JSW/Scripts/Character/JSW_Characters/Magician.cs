using System.Collections;
using UnityEngine;

public class Magician : Character
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

        if (isAddAbilityPower) proj.GetComponent<MagicBall>().SetInit(direction, abilityPower + attackDamage, projectileSpeed, nownomalAttackSize);
        else
        {
            proj.GetComponent<MagicBall>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackSize);
        }
    }

    // ��ų: ������ Ŀ�ٶ� ���� ����
    protected override IEnumerator FireSkill()
    {
        if (isCanTeleport) StartCoroutine(TeleportToPlayer());

        for (int i = 0; i < skillCount; i++)
        {
            yield return new WaitForSeconds(skillFireDelay);
            FireSkillProjectiles();
            yield return new WaitForSeconds(skillInterval);
        }
    }

    // �ñر� �߻� ����
    protected override void FireSkillProjectiles()
    {
        Transform target = FindNearestEnemy();
        if (target != null)
        {
            GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

            MagicBall mb = proj.GetComponent<MagicBall>();
            mb.SetInit((target.position - firePoint.position).normalized, (int)(abilityPower * skillDamage), projectileSpeed, skillSize);
            mb.speed = 5;
        }
    }

    IEnumerator TeleportToPlayer()
    {
        yield return new WaitForSeconds(5f);
        if (!isGround) transform.position = player.transform.position + Vector3.up;
    }
}