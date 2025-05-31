using System.Collections;
using UnityEngine;

public class Protecter : Character
{
    [Header("��ų")]
    public GameObject skillProjectile;
    public int skillCount = 3;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillSize = 1f;
    public float skillDamage = 1f;
    public float skillDuration = 7f;
    public float currentSkillDuration = 1f;

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
            proj.GetComponent<ProtecterAttack>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackSize);
        }
    }

    // ��ų: ������ȣ�� ����
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(skillInterval);
        StartCoroutine(ActiveSkillProtect());
    }


    public IEnumerator ActiveSkillProtect()
    {
        GameObject protectSkillObject = Instantiate(skillProjectile, transform.position, Quaternion.identity);

        currentSkillDuration = skillDuration;

        while (true)
        {
            currentSkillDuration -= Time.deltaTime;

            protectSkillObject.transform.position = transform.position;
            if (currentSkillDuration < 0)
            {
                Destroy(protectSkillObject);
                break;
            }
            yield return null;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        ContactPoint2D contact = collision.contacts[0];
        if (Vector2.Dot(contact.normal, Vector2.up) < 0.9f) return;

        if (isUltimateActive || isGround) return;
        if (currentSkillDuration > 0) currentSkillDuration += 3;
        isGround = true;


        Managers.Rider.RiderCountUp();
        fixedJoint.enabled = true;
        fixedJoint.connectedBody = collision.rigidbody;
    }

}
