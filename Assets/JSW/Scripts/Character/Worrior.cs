using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Worrior : Character
{
    // �Ϲ� ����: ����ü �߻� ���� ���⸸ �ϰų�, ���� Ÿ�� �� ����
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<SwordAttack>().SetDirection(direction); // �� �޼��尡 ���ٸ� �׳� ���� �����ؼ� ���� ��
    }
    protected override IEnumerator ActivateUltimate()
    {
        if (isGround)
        {
            isGround = false;
            transform.SetParent(null); // ���� ������ ������ ��� �и�
            RiderManager.Instance.RiderCountDown();

            isUltimateActive = true;
            currentMP = 0;
            mpImage.fillAmount = currentMP / maxMP;

            // ����
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            // �ñر� 3����
            for (int i = 0; i < burstCount; i++)
            {
                yield return new WaitForSeconds(burstFireDelay);
                FireBurstProjectiles();
                yield return new WaitForSeconds(burstInterval);
            }

            isUltimateActive = false;
        }
    }

    // �ñر�: ��ȭ�� ���� ����
    protected override void FireBurstProjectiles()
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