using System.Collections;
using UnityEngine;

public class Protecter : Character
{
    [Header("��ų")]
    public GameObject skillProjectile;
    public float skillFireDelay = 0.3f;
    public float skillSize = 1f;
    public float skillMultiple = 2f;
    public float skillDuration = 7f;
    public float currentSkillDuration = 0f;
    public int skillDurability = 100;

    [Header("��ȭ")]
    public float nomalAttackSize;
    public bool isShieldBreakExplosion;
    public GameObject shieldBreakExplosionEffect;
    public bool isShieldCancelsProjectiles;

    public int upgradeNum;
    public GameObject player;

    protected override void Start()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerMove>().gameObject;
    }

    // �Ϲ� ����: ����� ������ ���� ����ü ����
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        // ���⿡ ���� ĳ���� ��������Ʈ �¿� ����
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float nownomalAttackSize = nomalAttackSize;

        //proj.GetComponent<ProtecterAttack>().SetInit(direction, attackPower, projectileSpeed, nomalAttackSize, isShieldCancelsProjectiles);

    }

    // ��ų: ������ȣ�� ����
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(skillFireDelay);
        StartCoroutine(ActiveSkillProtect());
    }


    public IEnumerator ActiveSkillProtect()
    {
        GameObject protectSkillObject = Instantiate(skillProjectile, transform.position, Quaternion.identity);
        ProtecterSkill protectSkill = protectSkillObject.GetComponent<ProtecterSkill>();
        protectSkillObject.transform.localScale *= skillSize;
        protectSkill.Init(skillDurability + (int)(abilityPower * skillMultiple));
        currentSkillDuration = skillDuration;

        while (true)
        {
            currentSkillDuration -= Time.deltaTime;
            if (protectSkillObject == null)
            {
                if (isShieldBreakExplosion)
                {
                    ShieldBreakExplosion();
                }
                break;
            }
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

        if (isSkillActive || isGround) return;
        if (currentSkillDuration > 0) currentSkillDuration += 3;
        isGround = true;


        Managers.Status.RiderCount++;
        fixedJoint.enabled = true;
        fixedJoint.connectedBody = collision.rigidbody;
    }

    public void ShieldBreakExplosion()
    {
        // ����Ʈ �����ϰ�
        // �ֺ� �� ������ �ְ�\

        GameObject shieldBreakExplosionEffectObject = Instantiate(shieldBreakExplosionEffect, transform.position, Quaternion.identity);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, skillSize * 10);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                EnemyHP enemyHP = hit.GetComponent<EnemyHP>();

                //int totalDamage = abilityPower;
                print("���� ������ �޾ƶ�!!!");
                //enemyHP.TakeDamage(totalDamage);

            }
            if (hit.CompareTag("EnemyAttack"))
            {
                Destroy(hit.gameObject);
            }
        }

        Destroy(shieldBreakExplosionEffectObject, 0.1f);
    }
}
