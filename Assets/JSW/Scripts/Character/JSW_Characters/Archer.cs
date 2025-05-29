using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Archer : Character
{
    [Header("�ñر�")]
    public GameObject burstProjectile;
    public int burstCount = 3;
    public float burstInterval = 0.3f;
    public float burstFireDelay = 0.1f;


    public bool stage3; // �̰� Archer ���� �ɼ��̴� ����


    public int upgradeNum;


    // �Ϲ� ���� : ȭ�� �߻� 
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<Arrow>().SetDirection(direction);

        if (stage3)
        {
            GameObject proj2 = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            proj2.GetComponent<Arrow>().SetDirection(Quaternion.Euler(0, 0, 10) * direction);

            GameObject proj3 = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            proj3.GetComponent<Arrow>().SetDirection(Quaternion.Euler(0, 0, -10) * direction);
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
        int projectileCount = 10;
        float angleStep = 360f / projectileCount;
        Vector3 startPos = transform.position;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject proj = Instantiate(burstProjectile, startPos, rotation);
            proj.GetComponent<Arrow>().SetDirection(rotation * Vector2.right);
        }
    }
}