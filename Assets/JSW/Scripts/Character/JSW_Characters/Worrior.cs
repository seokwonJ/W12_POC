using System.Collections;
using UnityEngine;

public class Worrior : Character
{
    [Header("��ų")]
    public GameObject skillProjectile;
    public int skillCount = 3;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;

    [Header("��ȭ")]
    public float nomalAttackSize;
    public float nomalAttackLifetime;
    public bool isfallingCanAttack;
    public bool isShieldFlyer;
    public float shieldNum = 5;

    public int upgradeNum;

    private PlayerStatus _playerStatus;

    protected override void Start()
    {
        base.Start();
        _playerStatus = FindAnyObjectByType<PlayerStatus>();
    }

    // �Ϲ� ����: ������ ����ü �߻�
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<SwordAttack>().SetInit(direction, TotalAttackDamage(), projectileSpeed, nomalAttackLifetime, nomalAttackSize); // �� �޼��尡 ���ٸ� �׳� ���� �����ؼ� ���� ��
    }

    // ��ų: Ŀ�ٶ� ������ ����ü 3�� ���� �߻�
    protected override IEnumerator FireSkill()
    {

        if (isShieldFlyer) _playerStatus.defensePower -= shieldNum;

        // ��ų 3����
        for (int i = 0; i < skillCount; i++)
        {
            yield return new WaitForSeconds(skillFireDelay);
            FireSkillProjectiles();
            yield return new WaitForSeconds(skillInterval);
        }
    }

    protected override void FireSkillProjectiles()
    {
        GameObject proj = Instantiate(skillProjectile, firePoint.position, Quaternion.identity);
        var sword = proj.GetComponent<SwordAttack>();
        if (sword != null)
        {
            sword.speed = 20;
        }
        proj.transform.localScale *= 3; // Ŀ�ٶ� �� �ֵθ��� ����
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        ContactPoint2D contact = collision.contacts[0];
        if (Vector2.Dot(contact.normal, Vector2.up) < 0.9f) return;

        if (isSkillActive || isGround) return;
        isGround = true;

        if (isShieldFlyer) _playerStatus.defensePower += shieldNum;

        Managers.Status.RiderCount++;
        fixedJoint.enabled = true;
        fixedJoint.connectedBody = collision.rigidbody;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isfallingCanAttack && collision.tag == "Enemy" && !isGround)
        {
            //collision.GetComponent<EnemyHP>().TakeDamage(attackPower);
        }
    }
}