using System.Collections;
using UnityEngine;

public class LightSoldier : Character
{
    [Header("��ų")]
    public GameObject skillProjectile;
    public int skillShotCount = 14;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public int skillSize = 2;

    [Header("��ȭ")]
    public float normalAttackSize;
    public float normalAttackLifetime;
    public bool isFires4NormalAttackProjectiles;
    public bool isGain1ManaPerHit;

    public int upgradeNum;

    protected override void Start()
    {
        base.Start();
    }

    // �Ϲ� ����: ������ ����ü �߻� 2�� �߻�
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position + Vector3.up, Quaternion.identity);
        GameObject proj2 = Instantiate(normalProjectile, firePoint.position + Vector3.down, Quaternion.identity);
        proj.GetComponent<LightSoldierAttack>().SetInit(direction, attackDamage, projectileSpeed, normalAttackLifetime, normalAttackSize, this, isGain1ManaPerHit);
        proj2.GetComponent<LightSoldierAttack>().SetInit(direction, attackDamage, projectileSpeed, normalAttackLifetime, normalAttackSize, this, isGain1ManaPerHit);

        if (isFires4NormalAttackProjectiles)
        {
            GameObject proj3 = Instantiate(normalProjectile, firePoint.position + Vector3.up * 2, Quaternion.identity);
            GameObject proj4 = Instantiate(normalProjectile, firePoint.position + Vector3.down * 2, Quaternion.identity);
            proj3.GetComponent<LightSoldierAttack>().SetInit(direction, attackDamage, projectileSpeed, normalAttackLifetime, normalAttackSize, this, isGain1ManaPerHit); // �� �޼��尡 ���ٸ� �׳� ���� �����ؼ� ���� ��
            proj4.GetComponent<LightSoldierAttack>().SetInit(direction, attackDamage, projectileSpeed, normalAttackLifetime, normalAttackSize, this, isGain1ManaPerHit); // �� �޼��尡 ���ٸ� �׳� ���� �����ؼ� ���� ��
        }
    }

    // ��ų: Ŀ�ٶ� ������ ����ü �ڿ��� ������ �߻�
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(skillInterval);

        Camera cam = Camera.main;

        // ī�޶� ����Ʈ�� ��ü ��� ~ �ϴ��� ���� �������� ��ȯ
        Vector3 topLeft = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.transform.position.z * -1f));
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.transform.position.z * -1f));

        for (int i = 0; i < skillShotCount; i++)
        {
            float randomY = Random.Range(bottomLeft.y, topLeft.y); // ���� ���� Y ����

            Vector3 worldPos = new Vector3(topLeft.x, randomY, 0); // ���� �����ڸ����� ���� y ��ġ

            GameObject proj = Instantiate(skillProjectile, worldPos, Quaternion.identity);
            var sword = proj.GetComponent<LightSoldierAttack>();

            if (sword != null)
            {
                sword.speed = 20;
                sword.SetInit(Vector3.right, attackDamage, 30, 10, normalAttackSize * skillSize, this, false);
            }

            rb.linearVelocity = new Vector2(0, 2f);
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

    public void GainMp()
    {
        currentMP += 1;
    }
}