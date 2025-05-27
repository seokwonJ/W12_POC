using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Archer : Character
{
    public bool stage3; // �̰� Archer ���� �ɼ��̴� ����

    // �Ϲ� ���� �������̵�
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<Arrow>().SetDirection(direction);

        if (stage3)
        {
            GameObject proj2 = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            proj2.GetComponent<Arrow>().SetDirection(direction + Vector2.up);

            GameObject proj3 = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            proj3.GetComponent<Arrow>().SetDirection(direction + Vector2.down);
        }
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

    // �ñر� ���� �������̵� (�ʿ� ��)
    protected override void FireBurstProjectiles()
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