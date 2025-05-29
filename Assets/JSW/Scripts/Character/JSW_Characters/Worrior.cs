using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Worrior : Character
{
    [Header("�ñر�")]
    public GameObject burstProjectile;
    public int burstCount = 3;
    public float burstInterval = 0.3f;
    public float burstFireDelay = 0.1f;

    public int upgradeNum;

    // �Ϲ� ����: ������ ����ü �߻�
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<SwordAttack>().SetInit(direction, attackDamage, projectileSpeed); // �� �޼��尡 ���ٸ� �׳� ���� �����ؼ� ���� ��
    }

    // ��ų: Ŀ�ٶ� ������ ����ü 3�� ���� �߻�
    protected override IEnumerator FireSkill()
    {
        // �ñر� 3����
        for (int i = 0; i < burstCount; i++)
        {
            yield return new WaitForSeconds(burstFireDelay);
            FireSkillProjectiles();
            yield return new WaitForSeconds(burstInterval);
        }
    }

    protected override void FireSkillProjectiles()
    {
        GameObject proj = Instantiate(burstProjectile, firePoint.position, Quaternion.identity);
        var sword = proj.GetComponent<SwordAttack>();
        if (sword != null)
        {
            sword.speed = 20;
        }
        proj.transform.localScale *= 3; // Ŀ�ٶ� �� �ֵθ��� ����
    }
}