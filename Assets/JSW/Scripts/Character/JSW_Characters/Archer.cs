using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Archer : Character
{
    [Header("��ų")]
    public GameObject burstProjectile;
    public int burstCount = 3;
    public float burstInterval = 0.3f;
    public float burstFireDelay = 0.1f;
    public int skillProjectileCount = 10;
    public bool isUpgradeTripleShot; // �̰� Archer ���� �ɼ��̴� ����

    [Header("��ȭ")]
    public float knockbackPower;
    public float arrowSize;

    public int upgradeNum;

    // �Ϲ� ���� : ȭ�� �߻� 
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<Arrow>().SetInit(direction, attackDamage, projectileSpeed, knockbackPower, arrowSize);

        if (isUpgradeTripleShot)
        {
            GameObject proj2 = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            proj2.GetComponent<Arrow>().SetInit(Quaternion.Euler(0, 0, 10) * direction, attackDamage, projectileSpeed, knockbackPower, arrowSize);

            GameObject proj3 = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            proj3.GetComponent<Arrow>().SetInit(Quaternion.Euler(0, 0, -10) * direction, attackDamage, projectileSpeed, knockbackPower, arrowSize);
        }

        Destroy(proj, 3);
    }

    // ��ų : ��濡 ȭ�� ������ �߻�
    protected override IEnumerator FireSkill()
    {
        for (int i = 0; i < burstCount; i++)
        {
            yield return new WaitForSeconds(burstFireDelay);
            FireSkillProjectiles();
            yield return new WaitForSeconds(burstInterval);
        }
    }

    // �ñر� ���� �������̵� (�ʿ� ��)
    protected override void FireSkillProjectiles()
    {
        float angleStep = 360f / skillProjectileCount;
        Vector3 startPos = transform.position;

        for (int i = 0; i < skillProjectileCount; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject proj = Instantiate(burstProjectile, startPos, rotation);
            proj.GetComponent<Arrow>().SetInit(rotation * Vector2.right, attackDamage, projectileSpeed, knockbackPower, arrowSize);
        }
    }
}