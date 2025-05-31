using System.Collections;
using UnityEngine;

public class LightSoldier : Character
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

        GameObject proj = Instantiate(normalProjectile, firePoint.position + Vector3.up, Quaternion.identity);
        GameObject proj2 = Instantiate(normalProjectile, firePoint.position + Vector3.down, Quaternion.identity);
        proj.GetComponent<LightSoldierAttack>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackLifetime, nomalAttackSize); // �� �޼��尡 ���ٸ� �׳� ���� �����ؼ� ���� ��
        proj2.GetComponent<LightSoldierAttack>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackLifetime, nomalAttackSize); // �� �޼��尡 ���ٸ� �׳� ���� �����ؼ� ���� ��
    }

    // ��ų: Ŀ�ٶ� ������ ����ü 3�� ���� �߻�
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(skillInterval);

        Camera cam = Camera.main;

        // ī�޶� ����Ʈ�� ��ü ��� ~ �ϴ��� ���� �������� ��ȯ
        Vector3 topLeft = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.transform.position.z * -1f));
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.transform.position.z * -1f));

        for (int i = 0; i < 14; i++)
        {
            float randomY = Random.Range(bottomLeft.y, topLeft.y); // ���� ���� Y ����

            Vector3 worldPos = new Vector3(topLeft.x, randomY, 0); // ���� �����ڸ����� ���� y ��ġ

            GameObject proj = Instantiate(skillProjectile, worldPos, Quaternion.identity);
            var sword = proj.GetComponent<LightSoldierAttack>();
            if (sword != null)
            {
                sword.speed = 20;
                sword.SetInit(Vector3.right, attackDamage, 30, 10, nomalAttackSize);
            }

            proj.transform.localScale *= 3;
            rb.linearVelocity = new Vector2(0, 2.2f);
            yield return new WaitForSeconds(0.2f);
            rb.linearVelocity = Vector3.zero;
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isfallingCanAttack && collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyHP>().TakeDamage(attackDamage);
        }
    }
}